use crate::{ComplexType, Element, ParseError, Schema, Sequence};
use std::io::Read;
use xml::attribute::OwnedAttribute;
use xml::common::{Position, TextPosition};
use xml::name::OwnedName;
use xml::reader::{EventReader, XmlEvent as Event};

pub fn parse_from_reader<R: Read>(reader: R) -> Result<Schema, ParseError> {
    let mut events = EventReader::new(reader);

    find_schema(&mut events)
}

fn find_schema<R: Read>(reader: &mut EventReader<R>) -> Result<Schema, ParseError> {
    loop {
        match reader.next()? {
            Event::StartElement { ref name, .. } if name.local_name == "schema" => {
                return parsing_schema_tag(reader);
            }
            _ => {}
        }
    }
}

fn parsing_schema_tag<R: Read>(reader: &mut EventReader<R>) -> Result<Schema, ParseError> {
    let mut schema = Schema::default();

    loop {
        match reader.next()? {
            Event::StartElement {
                name, attributes, ..
            } => {
                expect_name(&name, "element", reader.position())?;
                schema
                    .elements
                    .push(parse_element_tag(reader, &attributes)?);
            }
            Event::EndElement { ref name } => {
                expect_name(&name, "schema", reader.position())?;
                return Ok(schema);
            }
            _ => {}
        }
    }
}

fn parse_element_tag<R: Read>(
    reader: &mut EventReader<R>,
    attributes: &[OwnedAttribute],
) -> Result<Element, ParseError> {
    let name = expect_attr(attributes, "name", reader.position())?.to_string();

    match find_attr(attributes, "type") {
        Some(type_name) => {
            reader.to_end("element")?;
            Ok(Element::simple(name, type_name))
        }
        None => {
            let complex = find_complex_type_tag(reader)?;
            reader.to_end("element")?;
            return Ok(Element::complex(name, complex));
        }
    }
}

fn find_complex_type_tag<R: Read>(reader: &mut EventReader<R>) -> Result<ComplexType, ParseError> {
    loop {
        match reader.next()? {
            Event::StartElement { ref name, .. } => {
                expect_name(&name, "complexType", reader.position())?;
                return parse_complex_type_tag(reader);
            }
            _ => {}
        }
    }
}

fn parse_complex_type_tag<R: Read>(reader: &mut EventReader<R>) -> Result<ComplexType, ParseError> {
    loop {
        match reader.next()? {
            Event::StartElement { ref name, .. } => {
                let complex = match name.local_name.as_str() {
                    "sequence" => ComplexType::from(parse_sequence(reader)?),
                    _ => unimplemented!(),
                };
                reader.to_end("complexType")?;
                return Ok(complex);
            }
            _ => {}
        }
    }
}

fn parse_sequence<R: Read>(reader: &mut EventReader<R>) -> Result<Sequence, ParseError> {
    let mut elements = Vec::new();

    loop {
        match reader.next()? {
            Event::StartElement {
                name, attributes, ..
            } => match name.local_name.as_str() {
                "element" => elements.push(parse_element_tag(reader, &attributes)?),
                _ => unimplemented!(),
            },
            Event::EndElement { ref name } if name.local_name == "sequence" => {
                return Ok(Sequence(elements))
            }
            _ => {}
        }
    }
}

fn expect_name(
    name: &OwnedName,
    expected: &'static str,
    position: TextPosition,
) -> Result<(), ParseError> {
    if name.local_name.as_str() == expected {
        Ok(())
    } else {
        Err(ParseError::ExpectedTag {
            tag: expected,
            position,
        })
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

trait ReaderExt {
    fn to_end(&mut self, tag_name: &'static str) -> Result<(), ParseError>;
}

impl<R: Read> ReaderExt for EventReader<R> {
    fn to_end(&mut self, tag_name: &'static str) -> Result<(), ParseError> {
        loop {
            match self.next()? {
                Event::EndElement { ref name } if name.local_name == tag_name => return Ok(()),
                _ => {}
            }
        }
    }
}
