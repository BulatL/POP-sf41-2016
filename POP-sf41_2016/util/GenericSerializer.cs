using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_sf41_2016.util
{
    public class GenericSerializer
    {
        public static void Serializer<T>(string fileName, List<T> listToSerilize) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                using (var sw = new StreamWriter($@"../../Data/{fileName}"))
                {
                    serializer.Serialize(sw, listToSerilize);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<T> Deserializer<T>(string fileName) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                using (var sr = new StreamReader($@"../../Data/{fileName}"))
                {
                    return (List <T>)serializer.Deserialize(sr);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
