using System;
using Microsoft.Win32;

namespace GeneralToolkitLib.Storage.Registry
{
    public class RegistryDataTypeQWORD : RegistryDataTypes
    {
        private Int64 _data;
        public override string KeyName { get; set; }

        public override object Data
        {
            get { return _data; }
            set
            {
                if (value is Int64)
                    _data = (Int64)value;
            }
        }

        public override RegistryValueKind DataType => RegistryValueKind.QWord;
    }
}
