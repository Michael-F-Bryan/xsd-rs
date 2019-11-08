using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XsdCodegen.Test
{
    public class ParserTest
    {
        [Fact]
        public void SmokeTest()
        {
            var xsd = Helpers.XsdSchema;

            var got = Parser.Parse(xsd);

            Assert.NotNull(got);
        }

        [Fact]
        public void ParseSimpleFile()
        {
            var xsd = Helpers.Simple;

            var got = Parser.Parse(xsd);

            Assert.NotNull(got);
            var expectedNames = new[] { "lastname", "age", "dateborn" };
            Assert.Equal(expectedNames, got.Element.Select(elem => elem.Name));
        }
    }
}
