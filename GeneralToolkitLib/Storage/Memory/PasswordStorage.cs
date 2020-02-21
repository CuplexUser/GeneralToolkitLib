using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace GeneralToolkitLib.Storage.Memory
{
    public sealed class PasswordStorage : IDisposable
    {
        private readonly Dictionary<string, PasswordStorageItem> _encodedDataDictionary;
      
        public PasswordStorage()
        {
            _encodedDataDictionary = new Dictionary<string, PasswordStorageItem>();
        }

        [SecurityCritical]
        public void Set(string key, string password)
        {
            int encodingLength = Encoding.UTF8.GetByteCount(password);
            int paddingLength = 16 - encodingLength % 16;
            byte[] buffer = new byte[encodingLength + paddingLength];

            Encoding.UTF8.GetBytes(password, 0, password.Length, buffer, 0);
            ProtectedMemory.Protect(buffer, MemoryProtectionScope.SameProcess);

            PasswordStorageItem passwordStorageItem;
            if (_encodedDataDictionary.ContainsKey(key))
                passwordStorageItem = _encodedDataDictionary[key];
            else
            {
                passwordStorageItem = new PasswordStorageItem();
                _encodedDataDictionary.Add(key, passwordStorageItem);
            }

            passwordStorageItem.Data = (byte[]) buffer.Clone();
            passwordStorageItem.PaddingLength = paddingLength;
        }

        [SecurityCritical]
        public string Get(string key)
        {
            if (!_encodedDataDictionary.ContainsKey(key))
                return null;

            byte[] buffer = _encodedDataDictionary[key].Data;
            ProtectedMemory.Unprotect(buffer, MemoryProtectionScope.SameProcess);
            string password = Encoding.UTF8.GetString(buffer, 0, buffer.Length - this._encodedDataDictionary[key].PaddingLength);
            ProtectedMemory.Protect(buffer, MemoryProtectionScope.SameProcess);
            return password;
        }

        private class PasswordStorageItem
        {
            public byte[] Data;
            public int PaddingLength;
        }

        [SecurityCritical]
        public void PurgeMemory()
        {
            if (_encodedDataDictionary.Count>0)
            {
                foreach(var key in _encodedDataDictionary.Keys)
                {
                    byte[] buffer = _encodedDataDictionary[key].Data;
                    ProtectedMemory.Unprotect(buffer, MemoryProtectionScope.SameProcess);
                    Array.Clear(buffer, 0, buffer.Length);
                    _encodedDataDictionary[key].Data = null;
                    _encodedDataDictionary[key].PaddingLength = 0;
                }

                _encodedDataDictionary.Clear();
            }
        }

        public void Dispose()
        {
            _encodedDataDictionary.Clear();            
        }
    }
}