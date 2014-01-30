/*
dotNetRDF is free and open source software licensed under the MIT License

-----------------------------------------------------------------------------

Copyright (c) 2009-2012 dotNetRDF Project (dotnetrdf-developer@lists.sf.net)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is furnished
to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using VDS.RDF.Parsing;
using VDS.RDF.Parsing.Tokens;
using VDS.RDF.Writing;

namespace VDS.RDF
{
    [TestFixture]
    public class MimeTypesTests
    {
        [SetUp]
        public void Setup()
        {
            IOManager.ResetDefinitions();
        }

        [TearDown]
        public void Teardown()
        {
            IOManager.ResetDefinitions();
        }

        [Test]
        public void MimeTypesGetDefinitionsAll()
        {
            int count = IOManager.Definitions.Count();
            Console.WriteLine(count + " Definitions registered");
#if PORTABLE
            Assert.AreEqual(16, count);
#else
            Assert.AreEqual(30, count);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeAny()
        {
            int count = IOManager.GetDefinitions(IOManager.Any).Count();
            Console.WriteLine(count + " Definitions registered");
#if PORTABLE
            Assert.AreEqual(16, count);
#else
            Assert.AreEqual(30, count);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeNotation3_1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/n3");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(Notation3Parser), d.RdfParserType);
            Assert.AreEqual(typeof(Notation3Writer), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedNotation3Parser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNotation3Writer), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeNotation3_2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/rdf+n3");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(Notation3Parser), d.RdfParserType);
            Assert.AreEqual(typeof(Notation3Writer), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedNotation3Parser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNotation3Writer), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtNotation3_1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".n3");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(Notation3Parser), d.RdfParserType);
            Assert.AreEqual(typeof(Notation3Writer), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtNotation3_2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("n3");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(Notation3Parser), d.RdfParserType);
            Assert.AreEqual(typeof(Notation3Writer), d.RdfWriterType);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtNotation3_3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".n3.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedNotation3Parser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNotation3Writer), d.RdfWriterType);
        }
#endif

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtNotation3_4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("n3.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedNotation3Parser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNotation3Writer), d.RdfWriterType);
        }
#endif

        [Test]
        public void MimeTypesGetDefinitionsByTypeTurtle1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/turtle");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeTurtle2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/x-turtle");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeTurtle3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/turtle");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtTurtle1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".ttl");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtTurtle2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("ttl");
            Assert.AreEqual(1, defs.Count());

#if !NO_COMPRESSION
            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);
#endif
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtTurtle3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".ttl.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
        }
#endif

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtTurtle4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("ttl.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
        }
#endif

        [Test]
        public void MimeTypesGetDefinitionsByTypeNTriples1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/rdf-triples");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(NTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(NTriplesWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedNTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNTriplesWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeNTriples2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/plain");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(NTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(NTriplesWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedNTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNTriplesWriter), d.RdfWriterType);
#endif
        }
       
        [Test]
        public void MimeTypesGetDefinitionsByTypeNTriples3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/ntriples");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(NTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(NTriplesWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedNTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNTriplesWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeNTriples4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/ntriples+turtle");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(NTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(NTriplesWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedNTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNTriplesWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeNTriples5()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/x-ntriples");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(NTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(NTriplesWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedNTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNTriplesWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtNTriples1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".nt");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(NTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(NTriplesWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtNTriples2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("nt");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(NTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(NTriplesWriter), d.RdfWriterType);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtNTriples3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".nt.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedNTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNTriplesWriter), d.RdfWriterType);
        }
#endif

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtNTriples4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("nt.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedNTriplesParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedNTriplesWriter), d.RdfWriterType);
        }
#endif

        [Test]
        public void MimeTypesGetDefinitionsByTypeRdfXml1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/rdf+xml");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(RdfXmlWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedRdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfXmlWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeRdfXml2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/xml");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(RdfXmlWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedRdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfXmlWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeRdfXml3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/xml");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(RdfXmlWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedRdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfXmlWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfXml1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".rdf");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(RdfXmlWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfXml2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("rdf");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(RdfXmlWriter), d.RdfWriterType);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtRdfXml3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".rdf.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfXmlWriter), d.RdfWriterType);
        }
#endif

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtRdfXml4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("rdf.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfXmlParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfXmlWriter), d.RdfWriterType);
        }
#endif

        [Test]
        public void MimeTypesGetDefinitionsByTypeRdfJson1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/json");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfJsonParser), d.RdfParserType);
            Assert.AreEqual(typeof(RdfJsonWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedRdfJsonParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfJsonWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeRdfJson2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/json");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfJsonParser), d.RdfParserType);
            Assert.AreEqual(typeof(RdfJsonWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedRdfJsonParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfJsonWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfJson1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".rj");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfJsonParser), d.RdfParserType);
            Assert.AreEqual(typeof(RdfJsonWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfJson2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("rj");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfJsonParser), d.RdfParserType);
            Assert.AreEqual(typeof(RdfJsonWriter), d.RdfWriterType);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtRdfJson3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".rj.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfJsonParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfJsonWriter), d.RdfWriterType);
        }
#endif

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtRdfJson4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("rj.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfJsonParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfJsonWriter), d.RdfWriterType);
        }
#endif

#if !NO_HTMLAGILITYPACK
        [Test]
        public void MimeTypesGetDefinitionsByTypeRdfA1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/xhtml+xml");
            Assert.AreEqual(2, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(HtmlWriter), d.RdfWriterType);

            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedRdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfAWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeRdfA2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/html");
            Assert.AreEqual(2, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(HtmlWriter), d.RdfWriterType);

            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedRdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfAWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".html");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(HtmlWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("html");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(HtmlWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".html.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfAWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("html.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfAWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA5()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".htm");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(HtmlWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA6()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("htm");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(HtmlWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA7()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".htm.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfAWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA8()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("htm.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfAWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA9()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".xhtml");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(HtmlWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA10()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("xhtml");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(RdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(HtmlWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA11()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".xhtml.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfAWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtRdfA12()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("xhtml.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedRdfAParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedRdfAWriter), d.RdfWriterType);
        }
#endif

        [Test]
        public void MimeTypesGetDefinitionsByTypeSparqlXml1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/sparql-results+xml");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlXmlParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlXmlWriter), d.SparqlResultsWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedSparqlXmlParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlXmlWriter), d.SparqlResultsWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlXml1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".srx");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlXmlParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlXmlWriter), d.SparqlResultsWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlXml2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("srx");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlXmlParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlXmlWriter), d.SparqlResultsWriterType);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlXml3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".srx.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedSparqlXmlParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlXmlWriter), d.SparqlResultsWriterType);
        }
#endif

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlXml4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("srx.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedSparqlXmlParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlXmlWriter), d.SparqlResultsWriterType);
        }
#endif

        [Test]
        public void MimeTypesGetDefinitionsByTypeSparqlJson1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("application/sparql-results+json");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlJsonParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlJsonWriter), d.SparqlResultsWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedSparqlJsonParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlJsonWriter), d.SparqlResultsWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlJson1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".srj");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlJsonParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlJsonWriter), d.SparqlResultsWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlJson2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("srj");
            Assert.AreEqual(1, defs.Count());

#if !NO_COMPRESSION
            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlJsonParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlJsonWriter), d.SparqlResultsWriterType);
#endif
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlJson3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".srj.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedSparqlJsonParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlJsonWriter), d.SparqlResultsWriterType);
        }
#endif

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlJson4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("srj.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedSparqlJsonParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlJsonWriter), d.SparqlResultsWriterType);
        }
#endif

        [Test]
        public void MimeTypesGetDefinitionsByTypeSparqlCsv1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/csv");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlCsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlCsvWriter), d.SparqlResultsWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedSparqlCsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlCsvWriter), d.SparqlResultsWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeSparqlCsv2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/comma-separated-values");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlCsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlCsvWriter), d.SparqlResultsWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedSparqlCsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlCsvWriter), d.SparqlResultsWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlCsv1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".csv");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlCsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlCsvWriter), d.SparqlResultsWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlCsv2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("csv");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlCsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlCsvWriter), d.SparqlResultsWriterType);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlCsv3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".csv.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedSparqlCsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlCsvWriter), d.SparqlResultsWriterType);
        }
#endif

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlCsv4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("csv.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedSparqlCsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlCsvWriter), d.SparqlResultsWriterType);
        }
#endif

        [Test]
        public void MimeTypesGetDefinitionsByTypeSparqlTsv1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/tab-separated-values");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlTsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlTsvWriter), d.SparqlResultsWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedSparqlTsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlTsvWriter), d.SparqlResultsWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlTsv1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".tsv");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlTsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlTsvWriter), d.SparqlResultsWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlTsv2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("tsv");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(SparqlTsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(SparqlTsvWriter), d.SparqlResultsWriterType);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlTsv3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".tsv.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedSparqlTsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlTsvWriter), d.SparqlResultsWriterType);
        }
#endif

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetDefinitionsByExtSparqlTsv4()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension("tsv.gz");
            Assert.AreEqual(1, defs.Count());

            //Check GZipped definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(GZippedSparqlTsvParser), d.SparqlResultsParserType);
            Assert.AreEqual(typeof(GZippedSparqlTsvWriter), d.SparqlResultsWriterType);
        }
#endif

        [Test]
        public void MimeTypesGetDefinitionsByTypeCaseSensitivity1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("TEXT/TURTLE");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeCaseSensitivity2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("TEXT/turtle");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeCaseSensitivity3()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("TeXt/TuRtLe");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtCaseSensitivity1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".TTL");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByExtCaseSensitivity2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitionsByFileExtension(".tTl");
            Assert.AreEqual(1, defs.Count());

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeExtraParams1()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/turtle; charset=utf-8");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetDefinitionsByTypeExtraParams2()
        {
            IEnumerable<MimeTypeDefinition> defs = IOManager.GetDefinitions("text/turtle; q=1.0");
#if PORTABLE
            Assert.AreEqual(1, defs.Count());
#else
            Assert.AreEqual(2, defs.Count());
#endif

            //Check normal definition
            MimeTypeDefinition d = defs.First();
            Assert.AreEqual(typeof(TurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(CompressingTurtleWriter), d.RdfWriterType);

#if !NO_COMPRESSION
            //Check GZipped definition
            d = defs.Last();
            Assert.AreEqual(typeof(GZippedTurtleParser), d.RdfParserType);
            Assert.AreEqual(typeof(GZippedTurtleWriter), d.RdfWriterType);
#endif
        }

        [Test]
        public void MimeTypesGetParserByTypeNTriples1()
        {
            IRdfReader parser = IOManager.GetParser("text/plain");
            Assert.IsInstanceOf<NTriplesParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeNTriples2()
        {
            IRdfReader parser = IOManager.GetParser("text/ntriples");
            Assert.IsInstanceOf<NTriplesParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeNTriples3()
        {
            IRdfReader parser = IOManager.GetParser("text/ntriples+turtle");
            Assert.IsInstanceOf<NTriplesParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeNTriples4()
        {
            IRdfReader parser = IOManager.GetParser("application/rdf-triples");
            Assert.IsInstanceOf<NTriplesParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeNTriples5()
        {
            IRdfReader parser = IOManager.GetParser("application/x-ntriples");
            Assert.IsInstanceOf<NTriplesParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeTurtle1()
        {
            IRdfReader parser = IOManager.GetParser("text/turtle");
            Assert.IsInstanceOf<TurtleParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeTurtle2()
        {
            IRdfReader parser = IOManager.GetParser("application/x-turtle");
            Assert.IsInstanceOf<TurtleParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeTurtle3()
        {
            IRdfReader parser = IOManager.GetParser("application/turtle");
            Assert.IsInstanceOf<TurtleParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeNotation3_1()
        {
            IRdfReader parser = IOManager.GetParser("text/n3");
            Assert.IsInstanceOf<Notation3Parser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeNotation3_2()
        {
            IRdfReader parser = IOManager.GetParser("text/rdf+n3");
            Assert.IsInstanceOf<Notation3Parser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeRdfXml1()
        {
            IRdfReader parser = IOManager.GetParser("text/xml");
            Assert.IsInstanceOf<RdfXmlParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeRdfXml2()
        {
            IRdfReader parser = IOManager.GetParser("application/rdf+xml");
            Assert.IsInstanceOf<RdfXmlParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeRdfXml3()
        {
            IRdfReader parser = IOManager.GetParser("application/xml");
            Assert.IsInstanceOf<RdfXmlParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeRdfJson1()
        {
            IRdfReader parser = IOManager.GetParser("text/json");
            Assert.IsInstanceOf<RdfJsonParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeRdfJson2()
        {
            IRdfReader parser = IOManager.GetParser("application/json");
            Assert.IsInstanceOf<RdfJsonParser>(parser);
        }

#if !NO_HTMLAGILITYPACK
        [Test]
        public void MimeTypesGetParserByTypeRdfA1()
        {
            IRdfReader parser = IOManager.GetParser("text/html");
            Assert.IsInstanceOf<RdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByTypeRdfA2()
        {
            IRdfReader parser = IOManager.GetParser("application/xhtml+xml");
            Assert.IsInstanceOf<RdfAParser>(parser);
        }
#endif

        [Test, ExpectedException(typeof(RdfParserSelectionException))]
        public void MimeTypesGetParserByTypeUnknown()
        {
            IRdfReader reader = IOManager.GetParser("application/unknown");
        }

        [Test]
        public void MimeTypesGetParserByExtNTriples1()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".nt");
            Assert.IsInstanceOf<NTriplesParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtNTriples2()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("nt");
            Assert.IsInstanceOf<NTriplesParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetParserByExtNTriples3()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".nt.gz");
            Assert.IsInstanceOf<GZippedNTriplesParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtNTriples4()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("nt.gz");
            Assert.IsInstanceOf<GZippedNTriplesParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetParserByExtTurtle1()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".ttl");
            Assert.IsInstanceOf<TurtleParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtTurtle2()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("ttl");
            Assert.IsInstanceOf<TurtleParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetParserByExtTurtle3()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".ttl.gz");
            Assert.IsInstanceOf<GZippedTurtleParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtTurtle4()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("ttl.gz");
            Assert.IsInstanceOf<GZippedTurtleParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetParserByExtNotation3_1()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".n3");
            Assert.IsInstanceOf<Notation3Parser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtNotation3_2()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("n3");
            Assert.IsInstanceOf<Notation3Parser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetParserByExtNotation3_3()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".n3.gz");
            Assert.IsInstanceOf<GZippedNotation3Parser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtNotation3_4()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("n3.gz");
            Assert.IsInstanceOf<GZippedNotation3Parser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetParserByExtRdfXml1()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".rdf");
            Assert.IsInstanceOf<RdfXmlParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfXml2()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("rdf");
            Assert.IsInstanceOf<RdfXmlParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetParserByExtRdfXml3()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".rdf.gz");
            Assert.IsInstanceOf<GZippedRdfXmlParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfXml4()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("rdf.gz");
            Assert.IsInstanceOf<GZippedRdfXmlParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetParserByExtRdfJson1()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".rj");
            Assert.IsInstanceOf<RdfJsonParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfJson2()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("rj");
            Assert.IsInstanceOf<RdfJsonParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetParserByExtRdfJson3()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".rj.gz");
            Assert.IsInstanceOf<GZippedRdfJsonParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfJson4()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("rj.gz");
            Assert.IsInstanceOf<GZippedRdfJsonParser>(parser);
        }
#endif

#if !NO_HTMLAGILITYPACK
        [Test]
        public void MimeTypesGetParserByExtRdfA1()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".html");
            Assert.IsInstanceOf<RdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA2()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("html");
            Assert.IsInstanceOf<RdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA3()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".html.gz");
            Assert.IsInstanceOf<GZippedRdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA4()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("html.gz");
            Assert.IsInstanceOf<GZippedRdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA5()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".htm");
            Assert.IsInstanceOf<RdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA6()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("htm");
            Assert.IsInstanceOf<RdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA7()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".htm.gz");
            Assert.IsInstanceOf<GZippedRdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA8()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("htm.gz");
            Assert.IsInstanceOf<GZippedRdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA9()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".xhtml");
            Assert.IsInstanceOf<RdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA10()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("xhtml");
            Assert.IsInstanceOf<RdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA11()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension(".xhtml.gz");
            Assert.IsInstanceOf<GZippedRdfAParser>(parser);
        }

        [Test]
        public void MimeTypesGetParserByExtRdfA12()
        {
            IRdfReader parser = IOManager.GetParserByFileExtension("xhtml.gz");
            Assert.IsInstanceOf<GZippedRdfAParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetWriterByTypeUnknown()
        {
            IRdfWriter writer = IOManager.GetWriter("application/unknown");
            Assert.IsInstanceOf<CompressingTurtleWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeAny()
        {
            IRdfWriter writer = IOManager.GetWriter(IOManager.Any);
            Assert.IsInstanceOf<NTriplesWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeNTriples1()
        {
            IRdfWriter writer = IOManager.GetWriter("text/plain");
            Assert.IsInstanceOf<NTriplesWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeNTriples2()
        {
            IRdfWriter writer = IOManager.GetWriter("text/ntriples");
            Assert.IsInstanceOf<NTriplesWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeNTriples3()
        {
            IRdfWriter writer = IOManager.GetWriter("text/ntriples+turtle");
            Assert.IsInstanceOf<NTriplesWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeNTriples4()
        {
            IRdfWriter writer = IOManager.GetWriter("application/rdf-triples");
            Assert.IsInstanceOf<NTriplesWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeNTriples5()
        {
            IRdfWriter writer = IOManager.GetWriter("application/x-ntriples");
            Assert.IsInstanceOf<NTriplesWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeTurtle1()
        {
            IRdfWriter writer = IOManager.GetWriter("text/turtle");
            Assert.IsInstanceOf<CompressingTurtleWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeTurtle2()
        {
            IRdfWriter writer = IOManager.GetWriter("application/x-turtle");
            Assert.IsInstanceOf<CompressingTurtleWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeTurtle3()
        {
            IRdfWriter writer = IOManager.GetWriter("application/turtle");
            Assert.IsInstanceOf<CompressingTurtleWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeNotation3_1()
        {
            IRdfWriter writer = IOManager.GetWriter("text/n3");
            Assert.IsInstanceOf<Notation3Writer>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeNotation3_2()
        {
            IRdfWriter writer = IOManager.GetWriter("text/rdf+n3");
            Assert.IsInstanceOf<Notation3Writer>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeRdfXml1()
        {
            IRdfWriter writer = IOManager.GetWriter("text/xml");
            Assert.IsInstanceOf<RdfXmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeRdfXml2()
        {
            IRdfWriter writer = IOManager.GetWriter("application/xml");
            Assert.IsInstanceOf<RdfXmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeRdfXml3()
        {
            IRdfWriter writer = IOManager.GetWriter("application/rdf+xml");
            Assert.IsInstanceOf<RdfXmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeRdfJson1()
        {
            IRdfWriter writer = IOManager.GetWriter("text/json");
            Assert.IsInstanceOf<RdfJsonWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeRdfJson2()
        {
            IRdfWriter writer = IOManager.GetWriter("application/json");
            Assert.IsInstanceOf<RdfJsonWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeRdfJson3()
        {
            IRdfWriter writer = IOManager.GetWriter("application/rdf+json");
            Assert.IsInstanceOf<RdfJsonWriter>(writer);
        }

#if !NO_HTMLAGILITYPACK
        [Test]
        public void MimeTypesGetWriterByTypeRdfA1()
        {
            IRdfWriter writer = IOManager.GetWriter("text/html");
            Assert.IsInstanceOf<HtmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetWriterByTypeRdfA2()
        {
            IRdfWriter writer = IOManager.GetWriter("application/xhtml+xml");
            Assert.IsInstanceOf<HtmlWriter>(writer);
        }
#endif

        [Test]
        public void MimeTypesGetWriterByExtNTriples1()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".nt");
            Assert.IsInstanceOf<NTriplesWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtNTriples2()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("nt");
            Assert.IsInstanceOf<NTriplesWriter>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetWriterByExtNTriples3()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".nt.gz");
            Assert.IsInstanceOf<GZippedNTriplesWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtNTriples4()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("nt.gz");
            Assert.IsInstanceOf<GZippedNTriplesWriter>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetWriterByExtTurtle1()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".ttl");
            Assert.IsInstanceOf<CompressingTurtleWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtTurtle2()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("ttl");
            Assert.IsInstanceOf<CompressingTurtleWriter>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetWriterByExtTurtle3()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".ttl.gz");
            Assert.IsInstanceOf<GZippedTurtleWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtTurtle4()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("ttl.gz");
            Assert.IsInstanceOf<GZippedTurtleWriter>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetWriterByExtNotation3_1()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".n3");
            Assert.IsInstanceOf<Notation3Writer>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtNotation3_2()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("n3");
            Assert.IsInstanceOf<Notation3Writer>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetWriterByExtNotation3_3()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".n3.gz");
            Assert.IsInstanceOf<GZippedNotation3Writer>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtNotation3_4()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("n3.gz");
            Assert.IsInstanceOf<GZippedNotation3Writer>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetWriterByExtRdfXml1()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".rdf");
            Assert.IsInstanceOf<RdfXmlWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfXml2()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("rdf");
            Assert.IsInstanceOf<RdfXmlWriter>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetWriterByExtRdfXml3()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".rdf.gz");
            Assert.IsInstanceOf<GZippedRdfXmlWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfXml4()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("rdf.gz");
            Assert.IsInstanceOf<GZippedRdfXmlWriter>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetWriterByExtRdfJson1()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".rj");
            Assert.IsInstanceOf<RdfJsonWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfJson2()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("rj");
            Assert.IsInstanceOf<RdfJsonWriter>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetWriterByExtRdfJson3()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".rj.gz");
            Assert.IsInstanceOf<GZippedRdfJsonWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfJson4()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("rj.gz");
            Assert.IsInstanceOf<GZippedRdfJsonWriter>(parser);
        }
#endif

#if !NO_HTMLAGILITYPACK
        [Test]
        public void MimeTypesGetWriterByExtRdfA1()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".html");
            Assert.IsInstanceOf<HtmlWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfA2()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("html");
            Assert.IsInstanceOf<HtmlWriter>(parser);
        }

        [Test]
#if !NO_COMPRESSION
        public void MimeTypesGetWriterByExtRdfA3()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".html.gz");
            Assert.IsInstanceOf<GZippedRdfAWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfA4()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("html.gz");
            Assert.IsInstanceOf<GZippedRdfAWriter>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetWriterByExtRdfA5()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".htm");
            Assert.IsInstanceOf<HtmlWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfA6()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("htm");
            Assert.IsInstanceOf<HtmlWriter>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetWriterByExtRdfA7()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".htm.gz");
            Assert.IsInstanceOf<GZippedRdfAWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfA8()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("htm.gz");
            Assert.IsInstanceOf<GZippedRdfAWriter>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetWriterByExtRdfA9()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".xhtml");
            Assert.IsInstanceOf<HtmlWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfA10()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("xhtml");
            Assert.IsInstanceOf<HtmlWriter>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetWriterByExtRdfA11()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension(".xhtml.gz");
            Assert.IsInstanceOf<GZippedRdfAWriter>(parser);
        }

        [Test]
        public void MimeTypesGetWriterByExtRdfA12()
        {
            IRdfWriter parser = IOManager.GetWriterByFileExtension("xhtml.gz");
            Assert.IsInstanceOf<GZippedRdfAWriter>(parser);
        }
#endif
#endif

        [Test, ExpectedException(typeof(RdfParserSelectionException))]
        public void MimeTypesGetSparqlParserByTypeUnknown()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParser("application/unknown");
        }

        [Test]
        public void MimeTypesGetSparqlParserByTypeSparqlXml1()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParser("application/sparql-results+xml");
            Assert.IsInstanceOf<SparqlXmlParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByTypeSparqlXml2()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParser("application/xml");
            Assert.IsInstanceOf<SparqlXmlParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByTypeSparqlJson1()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParser("application/sparql-results+json");
            Assert.IsInstanceOf<SparqlJsonParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByTypeSparqlJson2()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParser("application/json");
            Assert.IsInstanceOf<SparqlJsonParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByTypeSparqlCsv1()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParser("text/csv");
            Assert.IsInstanceOf<SparqlCsvParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByTypeSparqlCsv2()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParser("text/comma-separated-values");
            Assert.IsInstanceOf<SparqlCsvParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByTypeSparqlTsv1()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParser("text/tab-separated-values");
            Assert.IsInstanceOf<SparqlTsvParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlXml1()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension(".srx");
            Assert.IsInstanceOf<SparqlXmlParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlXml2()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension("srx");
            Assert.IsInstanceOf<SparqlXmlParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlXml3()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension(".srx.gz");
            Assert.IsInstanceOf<GZippedSparqlXmlParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlXml4()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension("srx.gz");
            Assert.IsInstanceOf<GZippedSparqlXmlParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlJson1()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension(".srj");
            Assert.IsInstanceOf<SparqlJsonParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlJson2()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension("srj");
            Assert.IsInstanceOf<SparqlJsonParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlJson3()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension(".srj.gz");
            Assert.IsInstanceOf<GZippedSparqlJsonParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlJson4()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension("srj.gz");
            Assert.IsInstanceOf<GZippedSparqlJsonParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlTsv1()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension(".tsv");
            Assert.IsInstanceOf<SparqlTsvParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlTsv2()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension("tsv");
            Assert.IsInstanceOf<SparqlTsvParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlTsv3()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension(".tsv.gz");
            Assert.IsInstanceOf<GZippedSparqlTsvParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlTsv4()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension("tsv.gz");
            Assert.IsInstanceOf<GZippedSparqlTsvParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlCsv1()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension(".csv");
            Assert.IsInstanceOf<SparqlCsvParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlCsv2()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension("csv");
            Assert.IsInstanceOf<SparqlCsvParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlCsv3()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension(".csv.gz");
            Assert.IsInstanceOf<GZippedSparqlCsvParser>(parser);
        }

        [Test]
        public void MimeTypesGetSparqlParserByExtSparqlCsv4()
        {
            ISparqlResultsReader parser = IOManager.GetSparqlParserByFileExtension("csv.gz");
            Assert.IsInstanceOf<GZippedSparqlCsvParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetSparqlWriterByTypeUnknown()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriter("application/unknown");
            Assert.IsInstanceOf<SparqlXmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByTypeAny()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriter(IOManager.Any);
            Assert.IsInstanceOf<SparqlXmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByTypeSparqlXml1()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriter("application/sparql-results+xml");
            Assert.IsInstanceOf<SparqlXmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByTypeSparqlXml2()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriter("application/xml");
            Assert.IsInstanceOf<SparqlXmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByTypeSparqlJson1()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriter("application/sparql-results+json");
            Assert.IsInstanceOf<SparqlJsonWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByTypeSparqlJson2()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriter("application/json");
            Assert.IsInstanceOf<SparqlJsonWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByTypeSparqlCsv1()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriter("text/csv");
            Assert.IsInstanceOf<SparqlCsvWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByTypeSparqlCsv2()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriter("text/comma-separated-values");
            Assert.IsInstanceOf<SparqlCsvWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByTypeSparqlTsv1()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriter("text/tab-separated-values");
            Assert.IsInstanceOf<SparqlTsvWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlXml1()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension(".srx");
            Assert.IsInstanceOf<SparqlXmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlXml2()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension("srx");
            Assert.IsInstanceOf<SparqlXmlWriter>(writer);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlXml3()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension(".srx.gz");
            Assert.IsInstanceOf<GZippedSparqlXmlWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlXml4()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension("srx.gz");
            Assert.IsInstanceOf<GZippedSparqlXmlWriter>(writer);
        }
#endif

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlJson1()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension(".srj");
            Assert.IsInstanceOf<SparqlJsonWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlJson2()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension("srj");
            Assert.IsInstanceOf<SparqlJsonWriter>(writer);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlJson3()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension(".srj.gz");
            Assert.IsInstanceOf<GZippedSparqlJsonWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlJson4()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension("srj.gz");
            Assert.IsInstanceOf<GZippedSparqlJsonWriter>(writer);
        }
#endif

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlTsv1()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension(".tsv");
            Assert.IsInstanceOf<SparqlTsvWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlTsv2()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension("tsv");
            Assert.IsInstanceOf<SparqlTsvWriter>(writer);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlTsv3()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension(".tsv.gz");
            Assert.IsInstanceOf<GZippedSparqlTsvWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlTsv4()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension("tsv.gz");
            Assert.IsInstanceOf<GZippedSparqlTsvWriter>(writer);
        }
#endif

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlCsv1()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension(".csv");
            Assert.IsInstanceOf<SparqlCsvWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlCsv2()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension("csv");
            Assert.IsInstanceOf<SparqlCsvWriter>(writer);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlCsv3()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension(".csv.gz");
            Assert.IsInstanceOf<GZippedSparqlCsvWriter>(writer);
        }

        [Test]
        public void MimeTypesGetSparqlWriterByExtSparqlCsv4()
        {
            ISparqlResultsWriter writer = IOManager.GetSparqlWriterByFileExtension("csv.gz");
            Assert.IsInstanceOf<GZippedSparqlCsvWriter>(writer);
        }
#endif

        [Test, ExpectedException(typeof(RdfParserSelectionException))]
        public void MimeTypesGetStoreParserByTypeUnknown()
        {
            IStoreReader parser = IOManager.GetStoreParser("application/unknown");
        }

        [Test]
        public void MimeTypesGetStoreParserByTypeNQuads1()
        {
            IStoreReader parser = IOManager.GetStoreParser("text/x-nquads");
            Assert.IsInstanceOf<NQuadsParser>(parser);
        }

        [Test]
        public void MimeTypesGetStoreParserByTypeTriG1()
        {
            IStoreReader parser = IOManager.GetStoreParser("application/x-trig");
            Assert.IsInstanceOf<TriGParser>(parser);
        }

        [Test]
        public void MimeTypesGetStoreParserByTypeTriX1()
        {
            IStoreReader parser = IOManager.GetStoreParser("application/trix");
            Assert.IsInstanceOf<TriXParser>(parser);
        }

        [Test]
        public void MimeTypesGetStoreParserByExtNQuads1()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension(".nq");
            Assert.IsInstanceOf<NQuadsParser>(parser);
        }

        [Test]
        public void MimeTypesGetStoreParserByExtNQuads2()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension("nq");
            Assert.IsInstanceOf<NQuadsParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetStoreParserByExtNQuads3()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension(".nq.gz");
            Assert.IsInstanceOf<GZippedNQuadsParser>(parser);
        }

        [Test]
        public void MimeTypesGetStoreParserByExtNQuads4()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension("nq.gz");
            Assert.IsInstanceOf<GZippedNQuadsParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetStoreParserByExtTriG1()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension(".trig");
            Assert.IsInstanceOf<TriGParser>(parser);
        }

        [Test]
        public void MimeTypesGetStoreParserByExtTriG2()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension("trig");
            Assert.IsInstanceOf<TriGParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetStoreParserByExtTriG3()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension(".trig.gz");
            Assert.IsInstanceOf<GZippedTriGParser>(parser);
        }

        [Test]
        public void MimeTypesGetStoreParserByExtTriG4()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension("trig.gz");
            Assert.IsInstanceOf<GZippedTriGParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetStoreParserByExtTriX1()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension(".xml");
            Assert.IsInstanceOf<TriXParser>(parser);
        }

        [Test]
        public void MimeTypesGetStoreParserByExtTriX2()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension("xml");
            Assert.IsInstanceOf<TriXParser>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetStoreParserByExtTriX3()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension(".xml.gz");
            Assert.IsInstanceOf<GZippedTriXParser>(parser);
        }

        [Test]
        public void MimeTypesGetStoreParserByExtTriX4()
        {
            IStoreReader parser = IOManager.GetStoreParserByFileExtension("xml.gz");
            Assert.IsInstanceOf<GZippedTriXParser>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetStoreWriterByTypeUnknown()
        {
            IStoreWriter writer = IOManager.GetStoreWriter("application/unknown");
            Assert.IsInstanceOf<NQuadsWriter>(writer);
        }

        [Test]
        public void MimeTypesGetStoreWriterByTypeNQuads1()
        {
            IStoreWriter parser = IOManager.GetStoreWriter("text/x-nquads");
            Assert.IsInstanceOf<NQuadsWriter>(parser);
        }

        [Test]
        public void MimeTypesGetStoreWriterByTypeTriG1()
        {
            IStoreWriter parser = IOManager.GetStoreWriter("application/x-trig");
            Assert.IsInstanceOf<TriGWriter>(parser);
        }

        [Test]
        public void MimeTypesGetStoreWriterByTypeTriX1()
        {
            IStoreWriter parser = IOManager.GetStoreWriter("application/trix");
            Assert.IsInstanceOf<TriXWriter>(parser);
        }

        [Test]
        public void MimeTypesGetStoreWriterByExtNQuads1()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension(".nq");
            Assert.IsInstanceOf<NQuadsWriter>(parser);
        }

        [Test]
        public void MimeTypesGetStoreWriterByExtNQuads2()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension("nq");
            Assert.IsInstanceOf<NQuadsWriter>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetStoreWriterByExtNQuads3()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension(".nq.gz");
            Assert.IsInstanceOf<GZippedNQuadsWriter>(parser);
        }

        [Test]
        public void MimeTypesGetStoreWriterByExtNQuads4()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension("nq.gz");
            Assert.IsInstanceOf<GZippedNQuadsWriter>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetStoreWriterByExtTriG1()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension(".trig");
            Assert.IsInstanceOf<TriGWriter>(parser);
        }

        [Test]
        public void MimeTypesGetStoreWriterByExtTriG2()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension("trig");
            Assert.IsInstanceOf<TriGWriter>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetStoreWriterByExtTriG3()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension(".trig.gz");
            Assert.IsInstanceOf<GZippedTriGWriter>(parser);
        }

        [Test]
        public void MimeTypesGetStoreWriterByExtTriG4()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension("trig.gz");
            Assert.IsInstanceOf<GZippedTriGWriter>(parser);
        }
#endif

        [Test]
        public void MimeTypesGetStoreWriterByExtTriX1()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension(".xml");
            Assert.IsInstanceOf<TriXWriter>(parser);
        }

        [Test]
        public void MimeTypesGetStoreWriterByExtTriX2()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension("xml");
            Assert.IsInstanceOf<TriXWriter>(parser);
        }

#if !NO_COMPRESSION
        [Test]
        public void MimeTypesGetStoreWriterByExtTriX3()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension(".xml.gz");
            Assert.IsInstanceOf<GZippedTriXWriter>(parser);
        }

        [Test]
        public void MimeTypesGetStoreWriterByExtTriX4()
        {
            IStoreWriter parser = IOManager.GetStoreWriterByFileExtension("xml.gz");
            Assert.IsInstanceOf<GZippedTriXWriter>(parser);
        }
#endif

        [Test]
        public void MimeTypesApplyWriterOptions1()
        {
            int compressionLevel = Options.DefaultCompressionLevel;
            try
            {
                Options.DefaultCompressionLevel = WriterCompressionLevel.High;
                IRdfWriter writer = IOManager.GetWriter("application/turtle");
                Assert.IsInstanceOf<CompressingTurtleWriter>(writer);
                Assert.AreEqual(WriterCompressionLevel.High, ((ICompressingWriter)writer).CompressionLevel);
            }
            finally
            {
                Options.DefaultCompressionLevel = compressionLevel;
            }
        }

        [Test]
        public void MimeTypesApplyWriterOptions2()
        {
            int compressionLevel = Options.DefaultCompressionLevel;
            try
            {
                Options.DefaultCompressionLevel = WriterCompressionLevel.High;
                IRdfWriter writer = IOManager.GetWriterByFileExtension(".ttl");
                Assert.IsInstanceOf<CompressingTurtleWriter>(writer);
                Assert.AreEqual(WriterCompressionLevel.High, ((ICompressingWriter)writer).CompressionLevel);
            }
            finally
            {
                Options.DefaultCompressionLevel = compressionLevel;
            }
        }

        [Test]
        public void MimeTypesApplyWriterOptions3()
        {
            int compressionLevel = Options.DefaultCompressionLevel;
            try
            {
                Options.DefaultCompressionLevel = WriterCompressionLevel.High;
                IStoreWriter writer = IOManager.GetStoreWriter("application/x-trig");
                Assert.IsInstanceOf<TriGWriter>(writer);
                Assert.AreEqual(WriterCompressionLevel.High, ((ICompressingWriter)writer).CompressionLevel);
            }
            finally
            {
                Options.DefaultCompressionLevel = compressionLevel;
            }
        }

        [Test]
        public void MimeTypesApplyWriterOptions4()
        {
            int compressionLevel = Options.DefaultCompressionLevel;
            try
            {
                Options.DefaultCompressionLevel = WriterCompressionLevel.High;
                IStoreWriter writer = IOManager.GetStoreWriterByFileExtension(".trig");
                Assert.IsInstanceOf<TriGWriter>(writer);
                Assert.AreEqual(WriterCompressionLevel.High, ((ICompressingWriter)writer).CompressionLevel);
            }
            finally
            {
                Options.DefaultCompressionLevel = compressionLevel;
            }
        }

        [Test]
        public void MimeTypesApplyParserOptions1()
        {
            TokenQueueMode queueMode = Options.DefaultTokenQueueMode;
            try
            {
                Options.DefaultTokenQueueMode = TokenQueueMode.AsynchronousBufferDuringParsing;
                IRdfReader parser = IOManager.GetParser("application/turtle");
                Assert.IsInstanceOf<TurtleParser>(parser);
                Assert.AreEqual(TokenQueueMode.AsynchronousBufferDuringParsing, ((ITokenisingParser)parser).TokenQueueMode);
            }
            finally
            {
                Options.DefaultTokenQueueMode = queueMode;
            }
        }

        [Test]
        public void MimeTypesApplyParserOptions2()
        {
            TokenQueueMode queueMode = Options.DefaultTokenQueueMode;
            try
            {
                Options.DefaultTokenQueueMode = TokenQueueMode.AsynchronousBufferDuringParsing;
                IRdfReader parser = IOManager.GetParserByFileExtension(".ttl");
                Assert.IsInstanceOf<TurtleParser>(parser);
                Assert.AreEqual(TokenQueueMode.AsynchronousBufferDuringParsing, ((ITokenisingParser)parser).TokenQueueMode);
            }
            finally
            {
                Options.DefaultTokenQueueMode = queueMode;
            }
        }

        [Test]
        public void MimeTypesApplyParserOptions3()
        {
            TokenQueueMode queueMode = Options.DefaultTokenQueueMode;
            try
            {
                Options.DefaultTokenQueueMode = TokenQueueMode.AsynchronousBufferDuringParsing;
                IStoreReader parser = IOManager.GetStoreParser("text/x-nquads");
                Assert.IsInstanceOf<NQuadsParser>(parser);
                Assert.AreEqual(TokenQueueMode.AsynchronousBufferDuringParsing, ((ITokenisingParser)parser).TokenQueueMode);
            }
            finally
            {
                Options.DefaultTokenQueueMode = queueMode;
            }
        }

        [Test]
        public void MimeTypesApplyParserOptions4()
        {
            TokenQueueMode queueMode = Options.DefaultTokenQueueMode;
            try
            {
                Options.DefaultTokenQueueMode = TokenQueueMode.AsynchronousBufferDuringParsing;
                IStoreReader parser = IOManager.GetStoreParserByFileExtension(".nq");
                Assert.IsInstanceOf<NQuadsParser>(parser);
                Assert.AreEqual(TokenQueueMode.AsynchronousBufferDuringParsing, ((ITokenisingParser)parser).TokenQueueMode);
            }
            finally
            {
                Options.DefaultTokenQueueMode = queueMode;
            }
        }

        [Test]
        public void MimeTypesContentNegotiation1()
        {
            String[] types = new String[] { "application/turtle" , "application/rdf+xml", "text/plain" };
            MimeTypeDefinition def = IOManager.GetDefinitions(types).FirstOrDefault();
            Assert.IsNotNull(def);
            Assert.AreEqual(typeof(TurtleParser), def.RdfParserType);
        }

        [Test]
        public void MimeTypesContentNegotiation2()
        {
            String[] types = new String[] { "application/rdf+xml", "application/turtle", "text/plain" };
            MimeTypeDefinition def = IOManager.GetDefinitions(types).FirstOrDefault();
            Assert.IsNotNull(def);
            Assert.AreEqual(typeof(RdfXmlParser), def.RdfParserType);
        }

        [Test]
        public void MimeTypesContentNegotiation3()
        {
            String[] types = new String[] { "text/plain", "application/rdf+xml", "application/turtle" };
            MimeTypeDefinition def = IOManager.GetDefinitions(types).FirstOrDefault();
            Assert.IsNotNull(def);
            Assert.AreEqual(typeof(NTriplesParser), def.RdfParserType);
        }

        [Test]
        public void MimeTypesContentNegotiation4()
        {
            MimeTypeDefinition def = IOManager.GetDefinitions(IOManager.Any).FirstOrDefault();
            Assert.IsNotNull(def);
        }

        [Test]
        public void MimeTypesContentNegotiation5()
        {
            String[] types = new String[] { "application/turtle; q=0.8", "application/rdf+xml", "text/plain; q=0.9" };
            MimeTypeDefinition def = IOManager.GetDefinitions(types).FirstOrDefault();
            Assert.IsNotNull(def);
            Assert.AreEqual(typeof(RdfXmlParser), def.RdfParserType);
        }

        private void PrintSelectors(IEnumerable<MimeTypeSelector> selectors)
        {
            foreach (MimeTypeSelector selector in selectors)
            {
                Console.WriteLine(selector.ToString());
            }
        }

        [Test]
        public void MimeTypesSelectors1()
        {
            String[] types = new String[] { "audio/*; q=0.2", "audio/basic" };
            List<MimeTypeSelector> selectors = MimeTypeSelector.CreateSelectors(types).ToList();
            this.PrintSelectors(selectors);

            Assert.IsFalse(selectors[0].IsRange);
            Assert.AreEqual("audio/basic", selectors[0].Type);
            Assert.IsTrue(selectors[1].IsRange);
            Assert.AreEqual("audio/*", selectors[1].Type);
            Assert.AreEqual(0.2d, selectors[1].Quality);
        }

        [Test]
        public void MimeTypesSelectors2()
        {
            String[] types = new String[] { "text/plain; q=0.5", "text/turtle" };
            List<MimeTypeSelector> selectors = MimeTypeSelector.CreateSelectors(types).ToList();
            this.PrintSelectors(selectors);

            Assert.AreEqual("text/turtle", selectors[0].Type);
            Assert.AreEqual("text/plain", selectors[1].Type);
            Assert.AreEqual(0.5d, selectors[1].Quality);
        }

        [Test]
        public void MimeTypesSelectors3()
        {
            String[] types = new String[] { "text/plain", "text/turtle" };
            List<MimeTypeSelector> selectors = MimeTypeSelector.CreateSelectors(types).ToList();
            this.PrintSelectors(selectors);

            Assert.AreEqual("text/plain", selectors[0].Type);
            Assert.AreEqual("text/turtle", selectors[1].Type);
        }

        [Test]
        public void MimeTypesSelectors4()
        {
            String[] types = new String[] { "text/*", "text/html", "*/*" };
            List<MimeTypeSelector> selectors = MimeTypeSelector.CreateSelectors(types).ToList();
            this.PrintSelectors(selectors);

            Assert.AreEqual("text/html", selectors[0].Type);
            Assert.AreEqual("text/*", selectors[1].Type);
            Assert.AreEqual(IOManager.Any, selectors[2].Type);
        }

        [Test]
        public void MimeTypesSelectors5()
        {
            String[] types = new String[] { "text/plain; q=0.5", "text/turtle; q=0.5" };
            List<MimeTypeSelector> selectors = MimeTypeSelector.CreateSelectors(types).ToList();
            this.PrintSelectors(selectors);

            Assert.AreEqual("text/plain", selectors[0].Type);
            Assert.AreEqual(0.5d, selectors[0].Quality);
            Assert.AreEqual("text/turtle", selectors[1].Type);
            Assert.AreEqual(0.5d, selectors[1].Quality);
        }

        [Test]
        public void MimeTypesSelectors6()
        {
            String[] types = new String[] { "text/turtle; q=0.5", "text/plain; q=0.5" };
            List<MimeTypeSelector> selectors = MimeTypeSelector.CreateSelectors(types).ToList();
            this.PrintSelectors(selectors);

            Assert.AreEqual("text/turtle", selectors[0].Type);
            Assert.AreEqual(0.5d, selectors[0].Quality);
            Assert.AreEqual("text/plain", selectors[1].Type);
            Assert.AreEqual(0.5d, selectors[1].Quality);
        }

        [Test]
        public void MimeTypesGetDefinitionByUpperCaseExt()
        {
            foreach (MimeTypeDefinition def in IOManager.Definitions)
            {
                if (!def.HasFileExtensions) continue;
                String ext = def.CanonicalFileExtension.ToUpper();
                MimeTypeDefinition def2 = IOManager.GetDefinitionsByFileExtension(ext).FirstOrDefault();
                Assert.IsNotNull(def2);
                Assert.AreEqual(def.SyntaxName, def2.SyntaxName);
                Assert.AreEqual(def.CanonicalMimeType, def2.CanonicalMimeType);
                Assert.AreEqual(def.HasFileExtensions, def2.HasFileExtensions);
                Assert.AreEqual(def.CanonicalFileExtension, def2.CanonicalFileExtension);
                Assert.AreEqual(def.CanParseRdf, def2.CanParseRdf);
                Assert.AreEqual(def.CanParseRdfDatasets, def2.CanParseRdfDatasets);
                Assert.AreEqual(def.CanParseSparqlResults, def2.CanParseSparqlResults);
                Assert.AreEqual(def.CanWriteRdf, def2.CanWriteRdf);
                Assert.AreEqual(def.CanWriteRdfDatasets, def2.CanWriteRdfDatasets);
                Assert.AreEqual(def.CanWriteSparqlResults, def2.CanWriteSparqlResults);
                Assert.AreEqual(def.RdfParserType, def2.RdfParserType);
                Assert.AreEqual(def.RdfDatasetParserType, def2.RdfDatasetParserType);
                Assert.AreEqual(def.SparqlResultsParserType, def2.SparqlResultsParserType);
                Assert.AreEqual(def.RdfWriterType, def2.RdfWriterType);
                Assert.AreEqual(def.RdfDatasetWriterType, def2.RdfDatasetWriterType);
                Assert.AreEqual(def.SparqlResultsWriterType, def2.SparqlResultsWriterType);
            }
        }
    }
}