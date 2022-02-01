using System;
using System.Collections.Generic;
using TestIEngineModel.Interface;
using TestIEngineModel.Testsite.Tests;

namespace TestIEngineModel.Testsite
{
    public class TestSite : ITestSite
    {
        private List<TestTypes> _testTypes;

        public TestSite(List<TestTypes> testTypes) // What tests we want to do in current testsite
        {
            _testTypes = testTypes;
        }

        public void RunTest(IEngine engine, double temp)
        {
            engine.Put(this);
            engine.RunSimulation(temp);
        }

        public void Update(IEngine engine)
        {
            foreach (var tests in _testTypes)
            {
                switch (tests)
                {
                    case TestTypes.Temperature:
                        TemperatureTest(engine);
                        break;
                    default:
                        throw new ArgumentException("Not supportable engine type");
                }
            }
        }

        public void TemperatureTest(IEngine engine)
        {
            switch (engine.EngineTypes)
            {
                case EngineTypes.InternalCombustionEngine:
                    EngineTemperatureTests.CriticalTempSimulationTime(engine as InternalCombustion);
                    break;
                case EngineTypes.RotorCombustionEngine:
                    //TemperatureTests.CriticalTempSimulationTime(engine as RotorCombustion);
                    throw new ArgumentException("RotorCombustionEngine not yet implemented");
                case EngineTypes.SteamEngine:
                    //TemperatureTests.CriticalTempSimulationTime(engine as Steam);
                    throw new ArgumentException("SteamEngine not yet implemented");
                default:
                    throw new ArgumentException("Not supportable engine type");
            }
        }
    }
}
