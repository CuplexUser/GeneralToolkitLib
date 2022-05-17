using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
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

        [SecurityCritical, SecuritySafeCritical]
        public void Set(string key, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                //Remove password
                if (_encodedDataDictionary.ContainsKey(key))
                {
                    _encodedDataDictionary.Remove(key);
                }

                return;
            }

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

            passwordStorageItem.Data = (byte[])buffer.Clone();
            passwordStorageItem.PaddingLength = paddingLength;
        }

        [SecurityCritical, SecuritySafeCritical]
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

        [SecurityCritical, SecuritySafeCritical]
        public void PurgeMemory()
        {
            if (_encodedDataDictionary.Count > 0)
            {
                foreach (var key in _encodedDataDictionary.Keys)
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

    public enum MemoryProtectionScope
    {
        SameProcess
    }

    public static class ProtectedMemory
    {
        private static readonly MemoryStream _memoryStream = new MemoryStream();
        

        public static void Unprotect(byte[] buffer, MemoryProtectionScope sameProcess)
        {
            
        }

        public static void Protect(byte[] buffer, MemoryProtectionScope sameProcess)
        {
            _memoryStream.Write(buffer,0,buffer.Length);
        }

        public static long GetProtectedByteLength()
        {
            return _memoryStream.Length;
        }
    }
}