using Microsoft.Win32;

namespace GeneralToolkitLib.Storage.Registry
{
    public class RegistryDataTypeString : RegistryDataTypes
    {
        private string _data;
        public override string KeyName { get; set; }

        public override object Data
        {
            get { return _data; }
            set { _data = value as string; }
        }

        public override RegistryValueKind DataType => RegistryValueKind.String;
    }
}