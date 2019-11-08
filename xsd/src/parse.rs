use crate::{ParseError, Schema};
use std::io::Read;
use xml::reader::{EventReader, XmlEvent as Event};

pub fn parse_from_reader<R: Read>(reader: R) -> Result<Schema, ParseError> {
    let mut events = EventReader::new(reader);
    let mut pda = PushdownAutomata::empty();

    while !pda.finished() {
        let ev = events.next()?;
        pda.process_event(ev)?;
    }

    Ok(pda.schema)
}

#[derive(Debug)]
struct PushdownAutomata {
    // the schema we're building.
    schema: Schema,
    stack: Vec<Box<dyn State>>,
}

impl PushdownAutomata {
    fn empty() -> PushdownAutomata {
        PushdownAutomata {
            schema: Schema::default(),
            stack: vec![Box::new(ReadingSchema)],
        }
    }

    fn process_event(&mut self, event: Event) -> Result<(), ParseError> {
        let PushdownAutomata {
            ref mut schema,
            ref mut stack,
        } = self;

        let cont = stack
            .last_mut()
            .expect("We should always have a state")
            .process_event(event, schema);

        match cont {
            Continuation::Continue => {}
            Continuation::Pop => {
                let _ = stack.pop();
            }
            Continuation::Push(new_state) => stack.push(new_state),
            Continuation::Error(err) => return Err(err),
        }

        Ok(())
    }

    fn finished(&self) -> bool {
        self.stack.is_empty()
    }
}

trait State: std::fmt::Debug {
    fn process_event(&mut self, _event: Event, _schema: &mut Schema) -> Continuation;
}

/// Within the `<xs:schema>` tag.
#[derive(Debug, Copy, Clone)]
struct ReadingSchema;

impl State for ReadingSchema {
    fn process_event(&mut self, event: Event, _schema: &mut Schema) -> Continuation {
        match event {
            Event::EndElement { name } if name.local_name == "schema" => Continuation::Pop,
            Event::StartElement {
                name, attributes, ..
            } if name.local_name == "element" => ReadElement::from_attributes(attributes).into(),
            _ => Continuation::Continue,
        }
    }
}

#[derive(Debug, Clone, PartialEq)]
struct ReadElement {
    name: String,
}

impl ReadElement {
    fn from_attributes<I>(attrs: I) -> Result<ReadElement, ParseError> {
        unimplemented!()
    }
}

impl State for ReadElement {
    fn process_event(&mut self, event: Event, _schema: &mut Schema) -> Continuation {
        unimplemented!()
    }
}

#[derive(Debug)]
enum Continuation {
    Continue,
    Push(Box<dyn State>),
    Pop,
    Error(ParseError),
}

impl<S: State + 'static> From<Result<S, ParseError>> for Continuation {
    fn from(other: Result<S, ParseError>) -> Continuation {
        match other {
            Ok(state) => Continuation::Push(Box::new(state)),
            Err(err) => Continuation::Error(err),
        }
    }
}
