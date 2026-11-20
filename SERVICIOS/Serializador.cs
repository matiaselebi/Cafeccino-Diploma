using System.IO;
using System.Xml.Serialization;

namespace SERVICIOS
{
    // Serializacion en XML con la clase XmlSerializer de .NET (no hace falta instalar nada)
    public static class Serializador
    {
        public static void Serializar_389ME<T>(T datos_389ME, string ruta_389ME)
        {
            XmlSerializer serializador_389ME = new XmlSerializer(typeof(T));
            using (StreamWriter archivo_389ME = new StreamWriter(ruta_389ME))
                serializador_389ME.Serialize(archivo_389ME, datos_389ME);
        }

        public static T Deserializar_389ME<T>(string ruta_389ME)
        {
            XmlSerializer serializador_389ME = new XmlSerializer(typeof(T));
            using (StreamReader archivo_389ME = new StreamReader(ruta_389ME))
                return (T)serializador_389ME.Deserialize(archivo_389ME);
        }
    }
}
