using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DotnetBoot.Utilities
{
    public static class XmlHelper
    {
        public static T ReadXmlFile<T>(string path, out string error, string rootName = null)
        {
            try
            {
                error = null;
                using (StreamReader file = new StreamReader(path))
                {
                    XmlSerializer reader = rootName == null ? new XmlSerializer(typeof(T)) : CreateOverrider<T>(rootName);
                    var result = (T)reader.Deserialize(file);
                    file.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return default;
            }
        }
        public static bool ToXmlFile<T>(this T target, string path, out string error, string rootName = null)
        {
            try
            {
                error = null;
                using (FileStream file = File.Create(path))
                {
                    XmlSerializer writer = rootName == null ? new XmlSerializer(typeof(T)) : CreateOverrider<T>(rootName);
                    writer.Serialize(file, target);
                    file.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        private static XmlSerializer CreateOverrider<T>(string rootName)
        {
            try
            {
                XmlAttributes myXmlAttributes = new XmlAttributes();

                XmlRootAttribute myXmlRootAttribute = new XmlRootAttribute
                {
                    ElementName = rootName
                };

                myXmlAttributes.XmlRoot = myXmlRootAttribute;
                XmlAttributeOverrides myXmlAttributeOverrides = new XmlAttributeOverrides();

                myXmlAttributeOverrides.Add(typeof(T), myXmlAttributes);

                XmlSerializer myXmlSerializer = new XmlSerializer(typeof(T), myXmlAttributeOverrides);
                return myXmlSerializer;
            }
            catch
            {
                throw;
            }
        }
        public static XElement ToXElement(this object target)
        {
            if (target == null) return null;
            var t = target.GetType();
            var elementInfo = t.GetCustomAttribute<XmlRootAttribute>(false);
            XElement element = new XElement(elementInfo?.ElementName);
            foreach (var pro in t.GetProperties())
            {
                element.SetAttributeValue(pro.Name, pro.GetValue(target));
            }
            return element;
        }
        public static T ToObject<T>(this XElement element, out string message, string rootName = null)
        {
            try
            {
                message = null;
                if (element == null)
                {
                    return default;
                }
                XmlSerializer serializer = rootName == null ? new XmlSerializer(typeof(T)) : CreateOverrider<T>(rootName);
                var ret = (T)serializer.Deserialize(element.CreateReader());
                return ret;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return default;
            }

        }
    }
}
