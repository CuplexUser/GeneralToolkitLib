namespace GeneralToolkitLib.OTP
{
    public class GoogleAuthenticator
    {
        public GoogleAuthenticator(string label, string secret)
        {
            AuthenticatorLabel = label;
            Secret = secret;
        }

        public string AuthenticatorLabel { get; set; }
        public string Secret { get; set; }

        public string KeyUri
        {
            get { return $"otpauth://totp/{AuthenticatorLabel}?secret={Secret}"; }
        }
    }
}