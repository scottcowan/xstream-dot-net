using System;
using System.Text;
using NUnit.Framework;

namespace Xstream.Core.Tests.Converters.Complex
{
    [TestFixture]
    public class TestStringBuilderConverter : BaseTest
    {
        [Test]
        public void TestSerialize()
        {
            StringBuilder sbuf	= new StringBuilder( TestRandomizer.GetString() );
            sbuf.Append( TestRandomizer.GetChar() );
            sbuf.Append( TestRandomizer.GetDouble() );

            string xml			= xstream.ToXml( sbuf );
            StringBuilder rev	= xstream.FromXml( xml ) as StringBuilder;

            Assert.IsNotNull( rev );
            Assert.AreEqual( sbuf.ToString(), rev.ToString() );
        }
    }
}