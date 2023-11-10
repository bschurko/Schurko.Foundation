using Schurko.Foundation.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Tests.Hash
{
    [TestClass]
    internal class HashPasswordTests
    {
        [TestMethod]
        public void Encrypt_and_Decrypt_Hash_Password_Test()
        {
            string password = "12345678";
            string hashedPassword = PasswordHasher.HashPassword(password);

            var isPasswordsEqual = PasswordHasher.VerifyHashedPassword(hashedPassword, password);

            Assert.IsTrue(isPasswordsEqual);
        }
    }
}
