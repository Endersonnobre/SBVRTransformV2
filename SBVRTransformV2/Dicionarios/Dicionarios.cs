using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBVRTransformV2.Structs;

namespace SBVRTransformV2.Dicionarios
{
  
    public class Dicionarios
    {
        public IDictionary<int, string> Classes = new Dictionary<int, string>();
        public IDictionary<int, AttrValues> Atributos = new Dictionary<int, AttrValues>();
        public IDictionary<int, RelValues> Relacionamento = new Dictionary<int, RelValues>();
        public IDictionary<int, HerVallues> Herancas = new Dictionary<int, HerVallues>();
        public IDictionary<int, string> Enumeracoes = new Dictionary<int, string>();
        public IDictionary<int, EnumItemValue> ItensEnumeracoes = new Dictionary<int, EnumItemValue>();


        public readonly IDictionary<string, string> Cardinalidades = new Dictionary<string, string>()
        {
            { "pelo menos um", "1..*" },
            { "pelo menos uma", "1..*" },
            { "pelo menos", "n..*" } ,
            { "no máximo um", "0..1" },
            { "no máximo uma", "0..1" },
            { "no máximo", "0..n" },
            {"exatamente um ", "1..1" },
            {"exatamente uma ", "1..1" },
            {"exatamente", "n..n" }
        };
    }
}
