#[macro_use]
extern crate pretty_assertions;

use xsd::{ComplexType, Element, ElementType, Schema};

#[test]
fn read_the_note_xsd() {
    let src = include_str!("data/note.xsd");

    let got = xsd::parse(src).unwrap();

    let expected = Schema {
        elements: vec![Element {
            name: String::from("note"),
            body: ElementType::Complex(ComplexType::sequence(vec![
                Element::simple("to", "string"),
                Element::simple("from", "string"),
                Element::simple("heading", "string"),
                Element::simple("body", "string"),
            ])),
        }],
    };
    assert_eq!(got, expected);
}
