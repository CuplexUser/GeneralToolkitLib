using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using GeneralToolkitLib.ConfigHelper;

namespace GeneralToolkitLib.Utility.DataConverters
{
    public static class IniConfigFileSectionSerializer
    {
        public static List<IniConfigFileSection> Serialize(object graph)
        {
            if (graph == null)
                return null;

            Attribute[] attrs = Attribute.GetCustomAttributes(graph.GetType());
            bool dataContractSerializable = attrs.OfType<DataContractAttribute>().Any();

            if (!dataContractSerializable)
                return null;

            List<IniConfigFileSection> configItemList = new List<IniConfigFileSection>();
            Type objType = graph.GetType();
            IniConfigFileSection configFileSection = new IniConfigFileSection();

            foreach (PropertyInfo propertyInfo in objType.GetProperties())
            {
                if (propertyInfo.CanRead && (propertyInfo.PropertyType.BaseType == typeof (ValueType)) || propertyInfo.GetMethod.ReturnType == typeof (string))
                {
                    object propertyValue = propertyInfo.GetValue(graph);
                    if (propertyValue == null)
                        configFileSection.ConfigItems.Add(propertyInfo.Name, "");
                    else
                        configFileSection.ConfigItems.Add(propertyInfo.Name, propertyValue.ToString());
                }
                else if (propertyInfo.PropertyType.BaseType == typeof (Enum))
                {
                    object propertyValue = propertyInfo.GetValue(graph);
                    configFileSection.ConfigItems.Add(propertyInfo.Name, propertyValue.ToString());
                }
                else if (propertyInfo.PropertyType == typeof (FontFamily))
                {
                    FontFamily fontFamily = propertyInfo.GetValue(graph) as FontFamily;
                    configFileSection.ConfigItems.Add(propertyInfo.Name, fontFamily.Name);
                }
                else
                {
                    object propertyValue = propertyInfo.GetValue(graph);
                    var subItems = Serialize(propertyValue);
                    if (subItems != null)
                        configItemList.AddRange(subItems);
                }
            }
            configItemList.Add(configFileSection);

            return configItemList;
        }
    }
}