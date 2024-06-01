using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBVRTransformV2.Dicionarios;

namespace SBVRTransformV2.Componentes
{
    public class Transformation
    {

        public StringBuilder TransformationUML(Dicionarios.Dicionarios dics)
        {
            StringBuilder umlString = new StringBuilder();
            umlString.AppendLine(@"<xmi:XMI xmlns:xmi='http://www.omg.org/spec/XMI/2011070' xmlns:uml='http://www.omg.org/spec/UML/20110701'>");
            umlString.AppendLine(@"<uml:Model xmi:id='Model_1' name='TransformationModel'>");
            if (dics != null)
            {
                foreach (var c in dics.Classes)
                {
                    umlString.AppendLine(@"<packagedElement xmi:type = 'uml:Class' xmi:id = '" + c.Key + "' name = '" + c.Value + "'>");
                    foreach (var a in dics.Atributos.Where(x => x.Value.ClassId == c.Key))
                    {
                        umlString.AppendLine(@"<ownedAttribute xmi:id = '"+ a.Key +"' name = '"+ a.Value.Name +"' type = '"+ a.Value.Type +"' />");
                    }

                    if (dics.Herancas.Any(x => x.Value.Child == c.Key))
                    {
                        foreach (var h in dics.Herancas.Where(x => x.Value.Child == c.Key))
                        {
                            umlString.AppendLine(@"<generalization xmi:id = '" + h.Key +"' general = '"+ h.Value.Parent +"' />");
                        }     
                    }
                    umlString.AppendLine(@"</packagedElement>");  
                }

                foreach (var e in dics.Enumeracoes)
                {
                    umlString.AppendLine(@"<packagedElement xmi:type='uml:Enumeration' xmi:id='"+ e.Key+"' name='"+ e.Value +"'>");
                    foreach (var i in dics.ItensEnumeracoes.Where(x => x.Value.EnumId == e.Key))
                    {
                        umlString.AppendLine(@"<ownedLiteral xmi:id='"+ i.Key+"' name='"+ i.Value.Name+"'/>");
                    }
                    umlString.AppendLine(@"</packagedElement>");
                }

                int countRel = 1;
                foreach (var r in dics.Relacionamento)
                {
                    
                        umlString.AppendLine(@"<packagedElement xmi:type='uml:Association' xmi:id='Association_"+ countRel +"'>");
                        countRel++;
                        umlString.AppendLine(@"<memberEnd xmi:idref='AssociationEnd_1'/>");
                        umlString.AppendLine(@"<memberEnd xmi:idref='AssociationEnd_2'/>");
                        umlString.AppendLine(@"<ownedEnd xmi:id='AssociationEnd_1' type='Class_1' name='"+ dics.Classes.
                            Where(x => x.Key == r.Value.Class1).First().Value +"'>");
                        umlString.AppendLine(@"<type xmi:idref='Class_2'/>");

                        if (r.Value.Type.Equals("Aggregation"))
                        {
                            umlString.AppendLine(@"<aggregation xmi:type='uml: AggregationKind' value='shared'/>");
                        }

                        if (int.TryParse(r.Value.Cardinality.Substring(0, 1), out int result))
                        {
                            umlString.AppendLine(@"<lowerValue xmi:type='uml:LiteralInteger' xmi:id='Lower_1' value='"+ 
                                r.Value.Cardinality.Substring(0, 1) + "'/>");
                        }
                        else
                        {
                            umlString.AppendLine(@"<lowerValue xmi:type='uml:LiteralUnlimitedNatural' xmi:id='Lower_1' value='" +
                                r.Value.Cardinality.Substring(0, 1) + "'/>");
                        }

                        if (int.TryParse(r.Value.Cardinality.Substring(3, 1), out int result2))
                        {
                            umlString.AppendLine(@"<lowerValue xmi:type='uml:LiteralInteger' xmi:id='Upper_1' value='" + 
                                r.Value.Cardinality.Substring(3, 1) + "'/>");
                        }
                        else
                        {
                            umlString.AppendLine(@"<lowerValue xmi:type='uml:LiteralUnlimitedNatural' xmi:id='Upper_1' value='" +
                                r.Value.Cardinality.Substring(3, 1) + "'/>");
                        }
                    umlString.AppendLine(@"</ownedEnd>");
                    umlString.AppendLine(@"<ownedEnd xmi:id='AssociationEnd_2' type='Class_2' name='" + dics.Classes.
                            Where(x => x.Key == r.Value.Class2).First().Value + "'>");
                    umlString.AppendLine(@"<type xmi:idref='Class_1'/>");
                    umlString.AppendLine(@"</ownedEnd>");
                    umlString.AppendLine(@"</packagedElement>");

                }
            }
            umlString.AppendLine(@"</uml:Model>");
            umlString.AppendLine(@"</xmi:XMI>");

            return umlString.Replace("'", "\"");

        }
    }
}
