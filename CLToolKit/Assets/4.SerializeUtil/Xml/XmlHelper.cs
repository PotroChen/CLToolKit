using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace Chenlin.SerializeUtil
{
    /// <summary>
    /// 将数据类在指定路径path转化为xml文件
    /// </summary>
    /*注意：
     *1.被转化类中不能包含Dictionary
     *2.若要将子类数据转化为基类，则需要在被转化类内添加标识[XmlInclude(typeof(SubClass))]
     */
    //TODO:添加BinaryFormatter和SoapFormatter，参考博客：http://www.cnblogs.com/jeffwongishandsome/archive/2009/05/01/1438895.html
    public static class XmlHelper
    {
        public static string Serialize<T>(this T value)
        {
            var result = string.Empty;
            if (value == null)
            {
                return result;
            }
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    serializer.Serialize(writer, value);
                    result = stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while XML serialize", ex);
            }

            return result;
        }

        public static T Deserialize<T>(this string value)
        {
            T retObj = default(T);
            if (string.IsNullOrEmpty(value) == true)
            {
                return retObj;
            }
            try
            {
                using (var rdr = new StringReader(value))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    retObj = (T)serializer.Deserialize(rdr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while XML Deserialize", ex);
            }

            return retObj;
        }

        public static void SerializeToFile<T>(T target, string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    xs.Serialize(stream, target);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while XML Deserialize", ex);
            }
        }

        public static void DeSerializeFromFile<T>(out T result, string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    result = (T)xs.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while XML Deserialize", ex);
            }

        }
    }
}