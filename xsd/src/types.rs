use std::iter::FromIterator;

#[derive(Debug, Clone, PartialEq)]
pub struct Element {
    pub name: String,
    pub body: ElementType,
}

impl Element {
    pub fn simple<P, Q>(name: P, type_name: Q) -> Self
    where
        P: Into<String>,
        Q: Into<String>,
    {
        Element {
            name: name.into(),
            body: ElementType::Simple {
                type_name: type_name.into(),
            },
        }
    }
}

#[derive(Debug, Clone, PartialEq)]
pub enum ElementType {
    Simple { type_name: String },
    Complex(ComplexType),
}

#[derive(Debug, Clone, PartialEq)]
pub enum ComplexType {
    Sequence(Sequence),
}

impl ComplexType {
    pub fn sequence<I>(items: I) -> Self
    where
        I: IntoIterator<Item = Element>,
    {
        ComplexType::Sequence(Sequence::from_iter(items))
    }
}

#[derive(Debug, Default, Clone, PartialEq)]
pub struct Sequence(pub Vec<Element>);

impl FromIterator<Element> for Sequence {
    fn from_iter<I: IntoIterator<Item = Element>>(items: I) -> Sequence {
        Sequence(Vec::from_iter(items))
    }
}

#[derive(Debug, Default, Clone, PartialEq)]
pub struct Schema {
    pub elements: Vec<Element>,
}
