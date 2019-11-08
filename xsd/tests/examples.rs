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
                Element::simple("to", "xs:string"),
                Element::simple("from", "xs:string"),
                Element::simple("heading", "xs:string"),
                Element::simple("body", "xs:string"),
            ])),
        }],
    };
    assert_eq!(got, expected);
}
