using System.Collections.Generic;
using Microsoft.Win32;

namespace GeneralToolkitLib.Storage.Registry
{
    public class RegistryDataTypeMultiString : RegistryDataTypes
    {
        private List<string> _multiStringData;

        public RegistryDataTypeMultiString()
        {
            this._multiStringData = new List<string>();
        }

        public override string KeyName { get; set; }

        public override object Data
        {
            get
            {
                if (this._multiStringData != null)
                    return this._multiStringData.ToArray();

                return null;
            }
            set
            {
                if (value is List<string>)
                    this._multiStringData = value as List<string>;
            }
        }

        public override RegistryValueKind DataType
        {
            get { return RegistryValueKind.MultiString; }
        }
    }
}