using System;

namespace TestIEngineModel.Testsite.Tests
{
    public static class EngineTemperatureTests
    {
        private static double AbsoluteError { get; set; } = 10e-1;

        static public void CriticalTempSimulationTime(InternalCombustion engine, int MaxTestTime = 10000)
        {
            double eps = engine.T - engine.EngineTemperature;
            engine.Workflow = eps > AbsoluteError && engine.SimulationTime < MaxTestTime;
            Console.WriteLine($"At the: {engine.SimulationTime} We got: {eps}>{AbsoluteError}");

            if (!engine.Workflow)
            {
                Console.WriteLine("Simulation results:");
                Console.WriteLine($"Sim\\Max time: {engine.SimulationTime}\\{MaxTestTime} ");
                if (engine.SimulationTime < MaxTestTime)
                {
                    Console.WriteLine($"The {engine.EngineTypes} didn't pass the test.");
                }else
                {
                    Console.WriteLine($"The {engine.EngineTypes} passed the test.");
                }
            }
        }
    }
}
