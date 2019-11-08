using System.Xml.Serialization;
using System.Collections.Generic;

// bindings created using https://xmltocsharp.azurewebsites.net/

namespace XsdCodegen
{
    [XmlRoot(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Annotation {
		[XmlElement(ElementName="documentation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Documentation Documentation { get; set; }
	}

	[XmlRoot(ElementName="documentation", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Documentation {
		[XmlAttribute(AttributeName="source")]
		public string Source { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName="import", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Import {
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Annotation Annotation { get; set; }
		[XmlAttribute(AttributeName="namespace")]
		public string Namespace { get; set; }
		[XmlAttribute(AttributeName="schemaLocation")]
		public string SchemaLocation { get; set; }
	}

	[XmlRoot(ElementName="anyAttribute", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class AnyAttribute {
		[XmlAttribute(AttributeName="namespace")]
		public string Namespace { get; set; }
		[XmlAttribute(AttributeName="processContents")]
		public string ProcessContents { get; set; }
	}

	[XmlRoot(ElementName="restriction", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Restriction {
		[XmlElement(ElementName="anyAttribute", Namespace="http://www.w3.org/2001/XMLSchema")]
		public AnyAttribute AnyAttribute { get; set; }
		[XmlAttribute(AttributeName="base")]
		public string Base { get; set; }
		[XmlElement(ElementName="enumeration", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Enumeration> Enumeration { get; set; }
		[XmlElement(ElementName="sequence", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Sequence Sequence { get; set; }
		[XmlElement(ElementName="attribute", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Attribute> Attribute { get; set; }
		[XmlElement(ElementName="group", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Group Group { get; set; }
		[XmlElement(ElementName="minLength", Namespace="http://www.w3.org/2001/XMLSchema")]
		public MinLength MinLength { get; set; }
	}

	[XmlRoot(ElementName="complexContent", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class ComplexContent {
		[XmlElement(ElementName="restriction", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Restriction Restriction { get; set; }
		[XmlElement(ElementName="extension", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Extension Extension { get; set; }
	}

	[XmlRoot(ElementName="complexType", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class ComplexType {
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Annotation Annotation { get; set; }
		[XmlElement(ElementName="complexContent", Namespace="http://www.w3.org/2001/XMLSchema")]
		public ComplexContent ComplexContent { get; set; }
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="abstract")]
		public string Abstract { get; set; }
		[XmlElement(ElementName="sequence", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Sequence Sequence { get; set; }
		[XmlElement(ElementName="attribute", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Attribute> Attribute { get; set; }
		[XmlElement(ElementName="anyAttribute", Namespace="http://www.w3.org/2001/XMLSchema")]
		public AnyAttribute AnyAttribute { get; set; }
		[XmlAttribute(AttributeName="mixed")]
		public string Mixed { get; set; }
	}

	[XmlRoot(ElementName="element", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Element {
		[XmlAttribute(AttributeName="ref")]
		public string Ref { get; set; }
		[XmlAttribute(AttributeName="minOccurs")]
		public string MinOccurs { get; set; }
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Annotation Annotation { get; set; }
		[XmlElement(ElementName="complexType", Namespace="http://www.w3.org/2001/XMLSchema")]
		public ComplexType ComplexType { get; set; }
		[XmlElement(ElementName="key", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Key> Key { get; set; }
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="id")]
		public string Id { get; set; }
		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName="maxOccurs")]
		public string MaxOccurs { get; set; }
		[XmlAttribute(AttributeName="abstract")]
		public string Abstract { get; set; }
		[XmlAttribute(AttributeName="substitutionGroup")]
		public string SubstitutionGroup { get; set; }
	}

	[XmlRoot(ElementName="sequence", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Sequence {
		[XmlElement(ElementName="element", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Element> Element { get; set; }
		[XmlElement(ElementName="group", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Group> Group { get; set; }
		[XmlElement(ElementName="sequence", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Sequence> Sequence_ { get; set; }
		[XmlElement(ElementName="choice", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Choice Choice { get; set; }
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Annotation Annotation { get; set; }
		[XmlAttribute(AttributeName="minOccurs")]
		public string MinOccurs { get; set; }
		[XmlElement(ElementName="any", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Any Any { get; set; }
		[XmlAttribute(AttributeName="maxOccurs")]
		public string MaxOccurs { get; set; }
	}

	[XmlRoot(ElementName="attribute", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Attribute {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName="default")]
		public string Default { get; set; }
		[XmlAttribute(AttributeName="use")]
		public string Use { get; set; }
		[XmlAttribute(AttributeName="ref")]
		public string Ref { get; set; }
		[XmlElement(ElementName="simpleType", Namespace="http://www.w3.org/2001/XMLSchema")]
		public SimpleType SimpleType { get; set; }
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Annotation Annotation { get; set; }
		[XmlAttribute(AttributeName="fixed")]
		public string Fixed { get; set; }
	}

	[XmlRoot(ElementName="extension", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Extension {
		[XmlElement(ElementName="sequence", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Sequence Sequence { get; set; }
		[XmlElement(ElementName="attribute", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Attribute> Attribute { get; set; }
		[XmlAttribute(AttributeName="base")]
		public string Base { get; set; }
		[XmlElement(ElementName="attributeGroup", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<AttributeGroup> AttributeGroup { get; set; }
		[XmlElement(ElementName="group", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Group Group { get; set; }
		[XmlElement(ElementName="choice", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Choice Choice { get; set; }
	}

	[XmlRoot(ElementName="choice", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Choice {
		[XmlElement(ElementName="element", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Element> Element { get; set; }
		[XmlElement(ElementName="group", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Group Group { get; set; }
		[XmlAttribute(AttributeName="minOccurs")]
		public string MinOccurs { get; set; }
		[XmlAttribute(AttributeName="maxOccurs")]
		public string MaxOccurs { get; set; }
		[XmlElement(ElementName="sequence", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Sequence Sequence { get; set; }
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Annotation Annotation { get; set; }
		[XmlElement(ElementName="any", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Any Any { get; set; }
	}

	[XmlRoot(ElementName="group", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Group {
		[XmlElement(ElementName="choice", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Choice Choice { get; set; }
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Annotation Annotation { get; set; }
		[XmlAttribute(AttributeName="ref")]
		public string Ref { get; set; }
		[XmlAttribute(AttributeName="minOccurs")]
		public string MinOccurs { get; set; }
		[XmlAttribute(AttributeName="maxOccurs")]
		public string MaxOccurs { get; set; }
		[XmlElement(ElementName="sequence", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Sequence Sequence { get; set; }
	}

	[XmlRoot(ElementName="enumeration", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Enumeration {
		[XmlAttribute(AttributeName="value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName="simpleType", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class SimpleType {
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Annotation Annotation { get; set; }
		[XmlElement(ElementName="restriction", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Restriction Restriction { get; set; }
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlElement(ElementName="union", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Union Union { get; set; }
		[XmlElement(ElementName="list", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List List { get; set; }
	}

	[XmlRoot(ElementName="list", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class List {
		[XmlAttribute(AttributeName="itemType")]
		public string ItemType { get; set; }
		[XmlElement(ElementName="simpleType", Namespace="http://www.w3.org/2001/XMLSchema")]
		public SimpleType SimpleType { get; set; }
	}

	[XmlRoot(ElementName="union", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Union {
		[XmlElement(ElementName="simpleType", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<SimpleType> SimpleType { get; set; }
		[XmlAttribute(AttributeName="memberTypes")]
		public string MemberTypes { get; set; }
	}

	[XmlRoot(ElementName="selector", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Selector {
		[XmlAttribute(AttributeName="xpath")]
		public string Xpath { get; set; }
	}

	[XmlRoot(ElementName="field", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Field {
		[XmlAttribute(AttributeName="xpath")]
		public string Xpath { get; set; }
	}

	[XmlRoot(ElementName="key", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Key {
		[XmlElement(ElementName="selector", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Selector Selector { get; set; }
		[XmlElement(ElementName="field", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Field Field { get; set; }
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName="attributeGroup", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class AttributeGroup {
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Annotation Annotation { get; set; }
		[XmlElement(ElementName="attribute", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Attribute> Attribute { get; set; }
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="ref")]
		public string Ref { get; set; }
	}

	[XmlRoot(ElementName="minLength", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class MinLength {
		[XmlAttribute(AttributeName="value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName="any", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Any {
		[XmlAttribute(AttributeName="processContents")]
		public string ProcessContents { get; set; }
		[XmlAttribute(AttributeName="minOccurs")]
		public string MinOccurs { get; set; }
		[XmlAttribute(AttributeName="maxOccurs")]
		public string MaxOccurs { get; set; }
		[XmlAttribute(AttributeName="namespace")]
		public string Namespace { get; set; }
	}

	[XmlRoot(ElementName="notation", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Notation {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="public")]
		public string Public { get; set; }
		[XmlAttribute(AttributeName="system")]
		public string System { get; set; }
	}

	[XmlRoot(ElementName="schema", Namespace="http://www.w3.org/2001/XMLSchema")]
	public class Schema {
		[XmlElement(ElementName="annotation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Annotation> Annotation { get; set; }
		[XmlElement(ElementName="import", Namespace="http://www.w3.org/2001/XMLSchema")]
		public Import Import { get; set; }
		[XmlElement(ElementName="complexType", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<ComplexType> ComplexType { get; set; }
		[XmlElement(ElementName="group", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Group> Group { get; set; }
		[XmlElement(ElementName="simpleType", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<SimpleType> SimpleType { get; set; }
		[XmlElement(ElementName="element", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Element> Element { get; set; }
		[XmlElement(ElementName="attributeGroup", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<AttributeGroup> AttributeGroup { get; set; }
		[XmlElement(ElementName="notation", Namespace="http://www.w3.org/2001/XMLSchema")]
		public List<Notation> Notation { get; set; }
		[XmlAttribute(AttributeName="xs", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Xs { get; set; }
		[XmlAttribute(AttributeName="elementFormDefault")]
		public string ElementFormDefault { get; set; }
		[XmlAttribute(AttributeName="lang", Namespace="http://www.w3.org/XML/1998/namespace")]
		public string Lang { get; set; }
		[XmlAttribute(AttributeName="targetNamespace")]
		public string TargetNamespace { get; set; }
		[XmlAttribute(AttributeName="version")]
		public string Version { get; set; }
	}
}
