using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBVRTransformV2.Dicionarios;
using SBVRTransformV2.Structs;
using System.Xml;
using System.Xml.Linq;

namespace SBVRTransformV2.Componentes
{
    public class Extraction
    {
        XNamespace sbvr = XNamespace.Get(@"http://www.omg.org/spec/SBVR/20070901/SBVR.xml");
        XNamespace xmi = XNamespace.Get(@"http://schema.omg.org/spec/XMI/2.1");

        public void ClassExtraction(string path, Dicionarios.Dicionarios dics)
        {
            XDocument? xmlDoc = null;

            using (StreamReader oReader = new StreamReader(path, Encoding.GetEncoding("ISO-8859-1")))
            {
                xmlDoc = XDocument.Load(oReader);
                int countClasse = 1;
                int countHer = 1;
                int countAttr = 1;
                int countEmum = 1;
                int countEnumItem = 1;
                int countRel = 1;
                string[] classesRel = new string[2];

                var classes = xmlDoc.Descendants(sbvr + "ObjectType").Select(x => x.Attribute(xmi + "id").Value);

                foreach (var item in classes)
                {

                    dics.Classes.Add(countClasse ,item);
                    

                    var c = xmlDoc.Descendants(sbvr + "ObjectType").Where(x => x.Attribute(xmi + "id").Value == item);
                    var herda = c.Descendants(sbvr + "supertype");

                    if (herda.Any())
                    {
                        var Parent = c.Descendants(sbvr + "ObjectTypeReference")
                            .Select(x => x.Attribute("href").Value.Remove(0,1));
                        HerVallues her = new HerVallues();
                        her.Parent = dics.Classes.Where(x => x.Value.Equals(Parent.First())).First().Key;
                        her.Child = countClasse;

                        dics.Herancas.Add(countHer, her);
                        countHer++;
                    }

                    

                    var attr = c.Descendants(sbvr + "Attribute");

                    foreach (var atributos in attr)
                    {
                        AttrValues a = new AttrValues();
                        a.Name = atributos.Elements(sbvr + "name").First().Value;
                        a.Type = atributos.Elements(sbvr + "type").First().Value;
                        a.ClassId = countClasse;

                        dics.Atributos.Add(countAttr, a);
                        countAttr++;
                    }
                    countClasse++;
                }


                var enumerations = xmlDoc.Descendants(sbvr + "EnumerationType");

                foreach (var e in enumerations)
                {                      
                    dics.Enumeracoes.Add(countEmum, enumerations.Elements(sbvr + "name").First().Value);

                    var v = enumerations.Descendants(sbvr + "enumerationValue");
                    var n = v.Descendants(sbvr + "name");
                    foreach (var item in n)
                    {
                        EnumItemValue j = new EnumItemValue();
                        j.Name = item.Value;
                        j.EnumId = countEmum;
                        dics.ItensEnumeracoes.Add(countEnumItem, j);
                        countEnumItem++;
                    }
                    countEmum++;   
                }

                var associacao = xmlDoc.Descendants(sbvr + "AssociativeFactType");
                var agragacao = xmlDoc.Descendants(sbvr + "PartitiveFactType");

                if(associacao.Count() > 0)
                {
                    foreach (var item in associacao)
                    {

                        var d = associacao.Descendants(sbvr + "definition");
                        var r = associacao.Descendants(sbvr + "role");
                        var f = r.Descendants(sbvr + "FactTypeRole");
                        int index = 0;
                        foreach (var p in f)
                        {
                            classesRel[index] = p.Descendants(sbvr + "ObjectTypeReference").
                                Select(x => x.Attribute("href").Value.Remove(0, 1)).First();

                            index++;
                        }


                        RelValues rel = new RelValues();
                        rel.Type = "Associantion";
                        rel.Cardinality = dics.Cardinalidades.
                            Where(x => d.Elements(sbvr + "Text").First().Value.Contains(x.Key)).First().Value;
                        rel.Class1 = dics.Classes.Where(x => x.Value.Equals(classesRel[0])).First().Key;
                        rel.Class2 = dics.Classes.Where(x => x.Value.Equals(classesRel[1])).First().Key;
                        dics.Relacionamento.Add(countRel, rel);

                        countRel++;

                    }    
                }


                if(agragacao.Count() > 0)
                {
                    var d = agragacao.Descendants(sbvr + "definition");
                    var r = agragacao.Descendants(sbvr + "role");
                    var f = r.Descendants(sbvr + "FactTypeRole");
                    int index = 0;
                    foreach (var p in f)
                    {
                        classesRel[index] = p.Descendants(sbvr + "ObjectTypeReference").
                                Select(x => x.Attribute("href").Value.Remove(0, 1)).First();

                        index++;
                    }


                    RelValues rel = new RelValues();
                    rel.Type = "Aggregation";
                    rel.Cardinality = dics.Cardinalidades.
                        Where(x => d.Elements(sbvr + "Text").First().Value.Contains(x.Key)).First().Value;
                    rel.Class1 = dics.Classes.Where(x => x.Value.Equals(classesRel[0])).First().Key;
                    rel.Class2 = dics.Classes.Where(x => x.Value.Equals(classesRel[1])).First().Key;
                    dics.Relacionamento.Add(countRel, rel);

                    countRel++;
                }

            }
        }
    }
}
