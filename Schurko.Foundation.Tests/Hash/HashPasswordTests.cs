using Microsoft.AspNetCore.Identity;
using Schurko.Foundation.Hash;
using Schurko.Foundation.Identity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Tests.Hash
{
    [TestClass]
    public class HashPasswordTests
    {
        [TestMethod]
        public void Encrypt_and_Decrypt_Hash_Password_Test()
        {
            string password = "schurko";
            IPasswordHasher<AppUser> hashPasword = new Microsoft.AspNetCore.Identity.PasswordHasher<AppUser>();
            AppUser user = new AppUser();
            user.UserName = "bschurko";
            user.PasswordHash = "AQAAAAIAAYagAAAAEPwoFlexQTVB0q8k9JgMrW0L8YPEqjiv3VysWeZLhid5EHeS5DRIwEHdEVzGN21L4w==";
            string hashedPassword = hashPasword.HashPassword(user, password);

            var isPasswordsEqual = hashPasword.VerifyHashedPassword(user,hashedPassword, password);

            Assert.IsTrue(isPasswordsEqual == PasswordVerificationResult.Success); 
        }
    }
}
