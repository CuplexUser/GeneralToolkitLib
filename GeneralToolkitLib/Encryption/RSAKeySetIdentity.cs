using System;
using System.Runtime.Serialization;

namespace GeneralToolkitLib.Encryption
{
    [Serializable, DataContract]
    public sealed class RSAKeySetIdentity
    {
        [DataMember(Order = 1)] public readonly string RSA_PublicKey;
        [DataMember(Order = 2)] public readonly string RSA_PrivateKey;
        [DataMember(Order = 3)] public readonly string RSA_GUID;
        [DataMember(Order = 4)] private string _nickName;

        public RSAKeySetIdentity()
        {
        }

        public RSAKeySetIdentity(string privateKey, string publicKey)
        {
            this.RSA_PublicKey = publicKey;
            this.RSA_PrivateKey = privateKey;
            this.RSA_GUID = Guid.NewGuid().ToString().ToUpper();
        }

        public string NickName
        {
            get { return this._nickName; }
            set { this._nickName = value; }
        }
    }
}