namespace GeneralToolkitLib.OTP
{
    public class GoogleAuthenticator
    {
        public GoogleAuthenticator(string label, string secret)
        {
            this.AuthenticatorLabel = label;
            this.Secret = secret;
        }

        public string AuthenticatorLabel { get; set; }
        public string Secret { get; set; }

        public string KeyUri
        {
            get { return string.Format("otpauth://totp/{0}?secret={1}", AuthenticatorLabel, Secret); }
        }
    }
}