using System;
using System.Collections.Generic;
using TestIEngineModel.Testsite;

namespace TestIEngineModel
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new Serialization();
            var temperature = GetTemperatureInput();
            var TemperatureTestSite = new TestSite(new List<TestTypes> { TestTypes.Temperature });
            IEngine engine = serializer.Deserialize(EngineTypes.InternalCombustionEngine, @"..\..\..\EngineXML\InternalCombustionEngine1.xml");
            engine.Put(TemperatureTestSite);
            TemperatureTestSite.RunTest(engine, temperature);
        }

        static double GetTemperatureInput()
        {
            double _temperature = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("Type a engine ambient temperature");
                    _temperature = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{Environment.NewLine}{ex.Message}{Environment.NewLine}");
                    continue;
                }
                return _temperature;
            }
        }
    }
}
