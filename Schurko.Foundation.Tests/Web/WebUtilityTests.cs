using Schurko.Foundation.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Tests.Web
{
    [TestClass]
    public class WebUtilityTests
    {
        [TestMethod]
        public void Encoded_and_Decode_URL_Test()
        {
            string url = "https://www.netflix.com/watch/81428?trackId=26397";

            string encodedUrl = WebUtility.UrlEncode(url);

            string decodedUrl = WebUtility.UrlDecode(encodedUrl);

            Assert.IsTrue(url.Equals(decodedUrl));
        }
    }
}
