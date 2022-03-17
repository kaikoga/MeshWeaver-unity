using System.Linq;
using NUnit.Framework;
using Silksprite.MeshBuilder.Extensions;

namespace Silksprite.MeshBuilder.Models
{
    public class ChannelTest
    {
        [Test]
        public void TestZipChannels()
        {
            var a = new Mux<string>(new[]
            {
                new MuxLayer<string>("(0-19)", 0),
                new MuxLayer<string>("(20-29)", 20),
                new MuxLayer<string>("(30-49)", 30),
                new MuxLayer<string>("(50-)", 50)
            });
            var b = new Mux<string>(new[]
            {
                new MuxLayer<string>("[0-9]", 0),
                new MuxLayer<string>("[10-29]", 10),
                new MuxLayer<string>("[30-]", 30)
            });
            var r = a.ZipMux(b, (m, n, i) => $"{m}{n}{i}").ToArray();
            Assert.AreEqual(5, r.Length);
            Assert.AreEqual("(0-19)[0-9]0", r[0].Value);
            Assert.AreEqual("(0-19)[10-29]10", r[1].Value);
            Assert.AreEqual("(20-29)[10-29]20", r[2].Value);
            Assert.AreEqual("(30-49)[30-]30", r[3].Value);
            Assert.AreEqual("(50-)[30-]50", r[4].Value);
            Assert.AreEqual(0, r[0].MinIndex);
            Assert.AreEqual(10, r[1].MinIndex);
            Assert.AreEqual(20, r[2].MinIndex);
            Assert.AreEqual(30, r[3].MinIndex);
            Assert.AreEqual(50, r[4].MinIndex);
        }
    }
}