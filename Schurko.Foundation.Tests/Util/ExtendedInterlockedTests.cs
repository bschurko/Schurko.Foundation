using Schurko.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Tests.Util
{
    [TestClass]
    public class ExtendedInterlockedTests
    {
        [TestMethod]
        public void ExtendedInterlockedTest()
        {
            int i = 0;

            ExtendedInterlocked.Increment(ref i, 0, 1);
            Console.WriteLine(i);

            ExtendedInterlocked.Decrement(ref i, 0, 1);
            Console.WriteLine(i);

            Interlocked.Increment(ref i);
            Interlocked.Decrement(ref i);
        }
    }
}
