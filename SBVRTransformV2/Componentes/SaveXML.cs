using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBVRTransformV2.Componentes
{
    public class SaveXML
    {

        public void SaveFile(StringBuilder uml)
        {
            File.WriteAllText("Transformation.xml", uml.ToString());
        }
        

    }
}
