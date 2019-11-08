#[derive(Debug, Clone, PartialEq)]
pub enum Element {
    Simple { name: String, type_name: String },
    Complex(ComplexType),
}

#[derive(Debug, Clone, PartialEq)]
pub struct ComplexType;

#[derive(Debug, Clone, PartialEq)]
pub struct Sequence(Vec<Element>);

#[derive(Debug, Clone, PartialEq)]
pub struct Schema {
    pub elements: Vec<Element>,
}
