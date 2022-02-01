using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TestIEngineModel.Interface;

namespace TestIEngineModel
{
    public class Serialization : ISerializer
    {
        public string Serialize(EngineTypes eT, IEngine obj)
        {
            XmlSerializer xsSubmit;
            switch (eT)
            {
                case EngineTypes.InternalCombustionEngine:
                    xsSubmit = new XmlSerializer(typeof(InternalCombustion));
                    break;
                default:
                    throw new ArgumentException("Unknown EngineType");
            }

            using (var strWriter = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(strWriter) { Formatting = Formatting.Indented })
                {
                    xsSubmit.Serialize(writer, obj);
                    return strWriter.ToString();
                }
            }
        }

        public IEngine Deserialize(EngineTypes eT, string xmlString)
        {
            using (TextReader reader = new StreamReader(xmlString))
            {
                IEngine engine;
                XmlSerializer serializer;
                switch(eT)
                {
                    case EngineTypes.InternalCombustionEngine:
                        serializer = new XmlSerializer(typeof(InternalCombustion));
                        engine = (InternalCombustion)serializer.Deserialize(reader);
                        break;
                    default:
                        throw new ArgumentException($"Deserialization for {eT} not yet implemented!");
                }
                engine.EngineTypes = eT;
                return engine;
            }
        }
    }
}
