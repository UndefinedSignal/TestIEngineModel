namespace TestIEngineModel.Interface
{
    public interface ITestSite
    {
        public void Update(IEngine engine);
        public void RunTest(IEngine engine, double temp);
    }
}
