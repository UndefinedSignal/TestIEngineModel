using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIEngineModel.Interface
{
    public interface ISerializer
    {
        public string Serialize(EngineTypes eT, IEngine obj);
        public IEngine Deserialize(EngineTypes eT, string xmlString);
    }
}
