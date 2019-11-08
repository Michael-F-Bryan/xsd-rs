use crate::{Element, ParseError, Schema};
use std::io::Read;
use xml::attribute::OwnedAttribute;
use xml::common::{Position, TextPosition};
use xml::reader::{EventReader, XmlEvent as Event};

pub fn parse_from_reader<R: Read>(reader: R) -> Result<Schema, ParseError> {
    let mut events = EventReader::new(reader);
    let mut pda = PushdownAutomata::empty();

    while !pda.finished() {
        let ev = events.next()?;
        dbg!(&pda.stack);
        pda.process_event(ev, events.position())?;
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

    fn process_event(&mut self, event: Event, pos: TextPosition) -> Result<(), ParseError> {
        let PushdownAutomata {
            ref mut schema,
            ref mut stack,
        } = self;

        let cont = stack
            .last_mut()
            .expect("We should always have a state")
            .process_event(event, pos);

        match cont {
            Continuation::Continue => {}
            Continuation::Pop => {
                stack
                    .pop()
                    .expect("we should always have a state")
                    .on_pop(schema);
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
    fn process_event(&mut self, _event: Event, _pos: TextPosition) -> Continuation;

    fn on_pop(&mut self, _schema: &mut Schema) {}
}

/// Within the `<xs:schema>` tag.
#[derive(Debug, Copy, Clone)]
struct ReadingSchema;

impl ReadingSchema {
    fn on_start_element(self, attributes: &[OwnedAttribute], pos: TextPosition) -> Continuation {
        let name = match expect_attr(&attributes, "name", pos) {
            Ok(a) => a.to_string(),
            Err(e) => return Continuation::Error(e),
        };

        match find_attr(&attributes, "type") {
            Some(type_name) => Continuation::Push(Box::new(ReadSimpleElement {
                name,
                type_name: type_name.to_string(),
            })),
            None => Continuation::Push(Box::new(ExpectingComplexElement { name })),
        }
    }
}

impl State for ReadingSchema {
    fn process_event(&mut self, event: Event, pos: TextPosition) -> Continuation {
        match event {
            Event::EndElement { name } if name.local_name == "schema" => Continuation::Pop,
            Event::StartElement {
                name, attributes, ..
            } if name.local_name == "element" => self.on_start_element(&attributes, pos),
            _ => Continuation::Continue,
        }
    }
}

fn expect_attr<'attr>(
    attributes: &'attr [OwnedAttribute],
    attribute: &'static str,
    position: TextPosition,
) -> Result<&'attr str, ParseError> {
    find_attr(attributes, attribute).ok_or_else(|| ParseError::ExpectedAttribute {
        attribute,
        position,
    })
}

fn find_attr<'attr>(
    attributes: &'attr [OwnedAttribute],
    attribute: &'static str,
) -> Option<&'attr str> {
    attributes
        .iter()
        .find(|attr| attr.name.local_name == attribute)
        .map(|attr| attr.value.as_str())
}

#[derive(Debug, Clone, PartialEq)]
struct ReadSimpleElement {
    name: String,
    type_name: String,
}

impl State for ReadSimpleElement {
    fn process_event(&mut self, event: Event, _pos: TextPosition) -> Continuation {
        match event {
            Event::EndElement { name } if name.local_name == "element" => Continuation::Pop,
            _ => Continuation::Continue,
        }
    }

    fn on_pop(&mut self, schema: &mut Schema) {
        schema
            .elements
            .push(Element::simple(&self.name, &self.type_name));
    }
}

/// We're inside an `<element>` tag and are looking for a `<complexType>`.
#[derive(Debug, Clone, PartialEq)]
struct ExpectingComplexElement {
    name: String,
}

impl State for ExpectingComplexElement {
    fn process_event(&mut self, event: Event, pos: TextPosition) -> Continuation {
        match event {
            Event::EndElement { name } if name.local_name == "element" => Continuation::Pop,
            Event::StartElement { name, .. } => {
                if name.local_name == "complexType" {
                    Continuation::Push(Box::new(ReadingComplexElement {
                        name: name.local_name,
                    }))
                } else {
                    Continuation::Error(ParseError::ExpectedTag {
                        tag: "complexType",
                        position: pos,
                    })
                }
            }
            _ => Continuation::Continue,
        }
    }
}

/// We're insite a `<complexType>` and trying to figure out what type of
/// [`ComplexType`] it is.
#[derive(Debug, Clone, PartialEq)]
struct ReadingComplexElement {
    name: String,
}

impl State for ReadingComplexElement {
    fn process_event(&mut self, event: Event, pos: TextPosition) -> Continuation {
        match event {
            Event::StartElement { name, .. } => match name.local_name.as_str() {
                "sequence" => unimplemented!(),
                _ => Continuation::Error(ParseError::ExpectedTag {
                    tag: "sequence",
                    position: pos,
                }),
            },
            Event::EndElement { name } if name.local_name == "element" => unimplemented!(),
            _ => Continuation::Continue,
        }
    }
}

#[derive(Debug, Clone, PartialEq)]
struct ReadingSequence {
    element_name: String,
    items: Vec<Element>,
}

impl State for ReadingSequence {
    fn process_event(&mut self, event: Event, pos: TextPosition) -> Continuation {
        match event {
            Event::StartElement { .. } => unimplemented!(),
            _ => Continuation::Continue,
        }
    }
}

#[derive(Debug)]
enum Continuation {
    Continue,
    Push(Box<dyn State>),
    Pop,
    Error(ParseError),
}
