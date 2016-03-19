using System.Diagnostics;
using IctBaden.WebInfo.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IctBaden.WebInfo.Test.Browser
{
    [TestClass]
    public class UserAgentParserTests
    {
        [TestMethod]
        public void ParseMozilla5()
        {
            //var test = "Opera/7.11 (Windows NT 5.1; U)";
            const string userAgentString = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:28.0) Gecko/20100101 Firefox/28.0";

            var sw = new Stopwatch();
            sw.Restart();

            var info = UserAgent.Parse(userAgentString);

            sw.Stop();
            Debug.WriteLine($"{sw.ElapsedMilliseconds}ms");

            Assert.IsNotNull(info);
            Assert.AreEqual("Win7", info.Platform);
            Assert.AreEqual("Firefox", info.Browser);
            Assert.AreEqual("28.0", info.Version);
            Assert.AreEqual("Desktop", info.DeviceType);
            Assert.AreEqual(false, info.IsMobile);
            Assert.AreEqual(false, info.IsTablet);
            Assert.AreEqual(false, info.IsCrawler);
        }

        [TestMethod]
        public void ParseOpera7()
        {
            const string userAgentString = "Opera/7.11 (Windows NT 5.1; U)";

            var info = UserAgent.Parse(userAgentString);

            Assert.IsNotNull(info);
            Assert.AreEqual("WinXP", info.Platform);
            Assert.AreEqual("Opera", info.Browser);
            Assert.AreEqual("7.11", info.Version);
            Assert.AreEqual("Desktop", info.DeviceType);
            Assert.AreEqual(false, info.IsMobile);
            Assert.AreEqual(false, info.IsTablet);
            Assert.AreEqual(false, info.IsCrawler);
        }

        [TestMethod]
        public void ParseChrome()
        {
            const string userAgentString = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36";

            var info = UserAgent.Parse(userAgentString);

            Assert.IsNotNull(info);
            Assert.AreEqual("Win10", info.Platform);
            Assert.AreEqual("Chrome", info.Browser);
            Assert.AreEqual("49.0", info.Version);
            Assert.AreEqual("Desktop", info.DeviceType);
            Assert.AreEqual(false, info.IsMobile);
            Assert.AreEqual(false, info.IsTablet);
            Assert.AreEqual(false, info.IsCrawler);
        }

        [TestMethod]
        public void ParseInternetExplorer()
        {
            const string userAgentString = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322)";

            var info = UserAgent.Parse(userAgentString);

            Assert.IsNotNull(info);
            Assert.AreEqual("WinXP", info.Platform);
            Assert.AreEqual("IE", info.Browser);
            Assert.AreEqual("6.0", info.Version);
            Assert.AreEqual("Desktop", info.DeviceType);
            Assert.AreEqual(false, info.IsMobile);
            Assert.AreEqual(false, info.IsTablet);
            Assert.AreEqual(false, info.IsCrawler);
        }

        [TestMethod]
        public void ParseFirefoxMac()
        {
            const string userAgentString = "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.5; en-US; rv:1.9b4) Gecko/2008030317 Firefox/3.0b4";

            var info = UserAgent.Parse(userAgentString);

            Assert.IsNotNull(info);
            Assert.AreEqual("MacOSX", info.Platform);
            Assert.AreEqual("Firefox", info.Browser);
            Assert.AreEqual("3.0", info.Version);
            Assert.AreEqual("Desktop", info.DeviceType);
            Assert.AreEqual(false, info.IsMobile);
            Assert.AreEqual(false, info.IsTablet);
            Assert.AreEqual(false, info.IsCrawler);
        }

        [TestMethod]
        public void ParseSafariMac()
        {
            const string userAgentString = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/537.13+ (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2";

            var info = UserAgent.Parse(userAgentString);

            Assert.IsNotNull(info);
            Assert.AreEqual("MacOSX", info.Platform);
            Assert.AreEqual("Safari", info.Browser);
            Assert.AreEqual("5.1", info.Version);
            Assert.AreEqual("Desktop", info.DeviceType);
            Assert.AreEqual(false, info.IsMobile);
            Assert.AreEqual(false, info.IsTablet);
            Assert.AreEqual(false, info.IsCrawler);
        }



    }
}
