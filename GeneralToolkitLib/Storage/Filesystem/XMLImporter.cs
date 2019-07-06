using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Xml;
using Serilog;

namespace GeneralToolkitLib.Storage.FileSystem
{
    public class XMLImporter
    {
        private List<XMLDataElement> _dataElementList = null;

        public bool LoadXMLFile(string path)
        {
            try
            {
                FileStream xmlstream = File.OpenRead(path);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlstream);

                _dataElementList = new List<XMLDataElement>();
                var root = xmlDoc.DocumentElement;

                foreach (XmlNode xmlNode in root.ChildNodes)
                {
                    XMLDataElement dataElement = new XMLDataElement(xmlNode.Name);
                    dataElement.ElementValue = xmlNode.InnerText;
                    foreach (XmlAttribute xmlAttribute in xmlNode.Attributes)
                    {
                        dataElement.ElementProperties[xmlAttribute.Name] = xmlAttribute.Value;
                    }
                    _dataElementList.Add(dataElement);
                }


                xmlstream.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex,"LoadXMLFile()");
            }

            return true;
        }


        public List<XMLDataElement> XMLDocumentNodes
        {
            get { return _dataElementList; }
        }
    }


    [Bindable(BindableSupport.Yes)]
    public class XMLDataElement
    {
        public XMLDataElement()
        {
            ElementProperties = new NameValueCollection();
        }

        public XMLDataElement(string name)
        {
            ElementProperties = new NameValueCollection();
            ElementType = name;
        }

        public string ElementType { get; set; }
        public string ElementValue { get; set; }
        public NameValueCollection ElementProperties { get; set; }
    }
}