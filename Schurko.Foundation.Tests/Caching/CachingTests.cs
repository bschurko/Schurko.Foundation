using PNI.Caching;
using Schurko.Foundation.Tests.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Tests.Caching
{
    [TestClass]
    public class CachingTests
    {
        [TestMethod]
        public void Memory_Cache_Wrapper_Test()
        {
            var cache = MemoryCacheProvider.GetInstance("TestCacheName");
            var p = new Product() { Id=123, Name="Night Owl Camera", Description="Night Own Camera Solution, 6 Cameras and DVR"};
            cache.Add<Product>("NightOwl", p, null, new List<string>() { "Cam", "DVR", "NightOwl" });
            IEnumerable<KeyValuePair<string, object>> result = cache.GetByTag("NightOwl");
            Assert.IsTrue(result != null);
            var pp = result.Where(cc => cc.Key.Equals("NightOwl")).Select(cc => cc.Value);
            Assert.IsTrue(pp.Any());
            var product = (Product) pp.FirstOrDefault();
            
        }
    }
}
