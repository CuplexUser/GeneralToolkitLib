using System;
using Microsoft.Win32;

namespace GeneralToolkitLib.Storage.Registry
{
    public class RegistryDataTypeDWORD : RegistryDataTypes
    {
        private Int32 _data;
        public override string KeyName { get; set; }

        public override object Data
        {
            get { return _data; }
            set
            {
                if (value is Int32)
                    _data = (Int32) value;
            }
        }

        public override RegistryValueKind DataType => RegistryValueKind.DWord;
    }
}