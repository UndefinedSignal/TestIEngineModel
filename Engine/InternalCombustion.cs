using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TestIEngineModel.Interface;

namespace TestIEngineModel
{
    [Serializable()]
    public class InternalCombustion : IEngine
    {
        private int index = 0;
        private int timeCounter = 0;

        private ITestSite _testSite = null;

        [System.Xml.Serialization.XmlElement("I")]
        public double I { get; set; }
        [System.Xml.Serialization.XmlArrayItem(typeof(int))]
        public int[] M { get; set; }
        [System.Xml.Serialization.XmlArrayItem(typeof(int))]
        public int[] V { get; set; }
        [System.Xml.Serialization.XmlElement("T")]
        public double T { get; set; }
        [System.Xml.Serialization.XmlElement("Hm")]
        public double Hm { get; set; }
        [System.Xml.Serialization.XmlElement("Hv")]
        public double Hv { get; set; }
        [System.Xml.Serialization.XmlElement("C")]
        public double C { get; set; }

        public double EngineTemperature { get; set; }
        public double AirTemperature { get; set; }

        public bool Workflow { get; set; } = false;

        public InternalCombustion() { }

        public InternalCombustion(double i, int[] m, int[] v, double t, double hm, double hv, double c)
        {
            I = i;
            M = m;
            V = v;
            T = t;
            Hm = hm;
            Hv = hv;
            C = c;
        }

        public EngineTypes EngineTypes { get; set; }

        public int SimulationTime => timeCounter;

        private double GetVc => C * (AirTemperature - EngineTemperature); // returns C*(Тсреды-Tдвигателя)
        private double GetVh => M[index] * Hm + Math.Pow(V[index], 2) * Hv; // returns M*Hм + V^2 * Hv

        bool IEngine.Workflow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void RunSimulation(double _airTemperature)
        {
            Workflow = !Workflow;
            timeCounter = 0;
            AirTemperature = _airTemperature;
            EngineTemperature = AirTemperature;

            double v = V[0];
            double m = M[0];
            double a = m/I;

            while(Workflow)
            {
                timeCounter++;
                v += a;

                if(index < M.Length - 2)
                {
                    index += v < M[index + 1] ? 0 : 1;
                }

                double up = v - V[index];
                double down = V[index+1] - V[index];
                double factor = M[index + 1] - M[index];
                m = up / down * factor + M[index];
                EngineTemperature += GetVc + GetVh;

                a = m / I;
                Notify();
            }

        }

        public void Put(ITestSite testsite)
        {
            _testSite = testsite;
        }

        public void Take()
        {
            _testSite = null;
        }

        public void Notify()
        {
            _testSite.Update(this);
        }
    }
}
