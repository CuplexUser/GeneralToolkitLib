using GeneralToolkitLib.OTP;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTests
{
    [TestClass]
    public class OTPTest
    {
        [TestMethod]
        public void TimeAuthenticatorTest()
        {
            var authenticator = new TimeAuthenticator();
            string secret = Authenticator.GenerateKey();
            string code = authenticator.GetCode(secret);

            Assert.IsTrue(authenticator.CheckCode(secret,code),"OTP Time Authentication failed");
        }
    }
}
