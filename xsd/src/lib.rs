mod errors;
mod parse;
mod types;

pub use errors::ParseError;
pub use parse::parse_from_reader;
pub use types::*;

pub fn parse(src: &str) -> Result<Schema, ParseError> {
    parse_from_reader(src.as_bytes())
}
