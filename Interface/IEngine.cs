using TestIEngineModel.Interface;

namespace TestIEngineModel
{
    public interface IEngine
    {
        bool Workflow { get; set; }

        public EngineTypes EngineTypes { get; set; }
        public void RunSimulation(double AirTemperature);

        void Put(ITestSite testsite);

        void Take();

        void Notify();
    }
}
