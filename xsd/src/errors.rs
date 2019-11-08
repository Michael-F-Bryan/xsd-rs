#[derive(Debug, thiserror::Error)]
pub enum ParseError {
    #[error("Parsing XML failed")]
    Xml(
        #[source]
        #[from]
        xml::reader::Error,
    ),
}
