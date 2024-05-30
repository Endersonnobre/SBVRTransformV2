using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBVRTransformV2.Structs
{
    public struct AttrValues
    {
        public string Name;
        public string Type;
        public int ClassId;
    }

    public struct RelValues
    {
        public int Class1;
        public int Class2;
        public string Type;
        public string Cardinality;
    }

    public struct HerVallues
    {
        public int Parent;
        public int Child;
    }

    public struct EnumItemValue
    {
        public string Name;
        public int EnumId;
    }
}
