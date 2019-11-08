use xml::common::TextPosition;

#[derive(Debug, thiserror::Error)]
pub enum ParseError {
    #[error("Parsing XML failed")]
    Xml(
        #[source]
        #[from]
        xml::reader::Error,
    ),
    #[error("Expected a \"{attribute}\" attribute at {position}")]
    ExpectedAttribute {
        attribute: &'static str,
        position: TextPosition,
    },
    #[error("Expected a \"<{tag}>\" tag at {position}")]
    ExpectedTag {
        tag: &'static str,
        position: TextPosition,
    },
}
