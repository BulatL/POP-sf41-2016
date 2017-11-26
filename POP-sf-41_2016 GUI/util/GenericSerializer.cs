using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_sf41_2016.util
{
    public class GenericSerializer
    {
        public static void Serializer<T>(string fileName, ObservableCollection<T> listToSerilize) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
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

        public static ObservableCollection<T> Deserializer<T>(string fileName) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
                using (var sr = new StreamReader($@"../../Data/{fileName}"))
                {
                    return (ObservableCollection<T>)serializer.Deserialize(sr);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
