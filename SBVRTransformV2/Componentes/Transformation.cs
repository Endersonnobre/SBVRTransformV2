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
                    umlString.AppendLine(@"<packagedElement xmi:type = 'uml:Class' xmi:id = 'C" + c.Key + "' name = '" + c.Value + "'>");
                    foreach (var a in dics.Atributos.Where(x => x.Value.ClassId == c.Key))
                    {
                        umlString.AppendLine(@"<ownedAttribute xmi:id = 'A"+ a.Key +"' name = '"+ a.Value.Name +"' type = '"+ a.Value.Type +"' />");
                    }

                    if (dics.Herancas.Any(x => x.Value.Child == c.Key))
                    {
                        foreach (var h in dics.Herancas.Where(x => x.Value.Child == c.Key))
                        {
                            umlString.AppendLine(@"<generalization xmi:id = 'G" + h.Key +"' general = 'C"+ h.Value.Parent +"' />");
                        }     
                    }
                    umlString.AppendLine(@"</packagedElement>");  
                }

                foreach (var e in dics.Enumeracoes)
                {
                    umlString.AppendLine(@"<packagedElement xmi:type='uml:Enumeration' xmi:id='E"+ e.Key+"' name='"+ e.Value +"'>");
                    foreach (var i in dics.ItensEnumeracoes.Where(x => x.Value.EnumId == e.Key))
                    {
                        umlString.AppendLine(@"<ownedLiteral xmi:id='I"+ i.Key+"' name='"+ i.Value.Name+"'/>");
                    }
                    umlString.AppendLine(@"</packagedElement>");
                }

                int countRel = 1;
                int countCar = 1;
                int countEnds = 1;
                foreach (var r in dics.Relacionamento)
                {


                        umlString.AppendLine(@"<packagedElement xmi:type='uml:Association' xmi:id='Association_"+ countRel +"'>");

                        if(r.Value.Type == "Aggregation")
                        {
                            umlString.AppendLine(@"<ownedEnd xmi:id='R1" + countEnds + "' type='C" + r.Value.Class1 + "' aggregation='shared' association='Association_ " + countRel + "'>");
                        }
                        else
                        {
                            umlString.AppendLine(@"<ownedEnd xmi:id='R1" + countEnds + "' type='C" + r.Value.Class1 + "' association='Association_ " + countRel + "'>");
                        }   
                    
                      

                        if (int.TryParse(r.Value.Cardinality.Substring(0, 1), out int result))
                        {
                            umlString.AppendLine(@"<lowerValue xmi:type='uml:LiteralInteger' xmi:id='Lower_" + countCar + "' value='"+ 
                                r.Value.Cardinality.Substring(0, 1) + "'/>");
                        }
                        else
                        {
                            umlString.AppendLine(@"<lowerValue xmi:type='uml:LiteralUnlimitedNatural' xmi:id='Lower_"+ countCar + "' value='" +
                                r.Value.Cardinality.Substring(0, 1) + "'/>");
                        }

                        if (int.TryParse(r.Value.Cardinality.Substring(3, 1), out int result2))
                        {
                            umlString.AppendLine(@"<upperValue xmi:type='uml:LiteralInteger' xmi:id='Upper_" + countCar + "' value='" + 
                                r.Value.Cardinality.Substring(3, 1) + "'/>");
                        }
                        else
                        {
                            umlString.AppendLine(@"<upperValue xmi:type='uml:LiteralUnlimitedNatural' xmi:id='Upper_" + countCar + "' value='" +
                                r.Value.Cardinality.Substring(3, 1) + "'/>");
                        }
                        umlString.AppendLine(@"</ownedEnd>");

                        if (r.Value.Type == "Aggregation")
                        {
                            umlString.AppendLine(@"<ownedEnd xmi:id='R2" + r.Value.Class2 + "' type='C" + r.Value.Class2 + "' aggregation='shared' association='Association_ " + countRel + "'>");
                        }
                        else
                        {
                            umlString.AppendLine(@"<ownedEnd xmi:id='R2" + r.Value.Class2 + "' type='C" + r.Value.Class2 + "' association='Association_ " + countRel + "'>");
                        } 
                        umlString.AppendLine(@"</ownedEnd>");
                        umlString.AppendLine(@"</packagedElement>");
                        countRel++;
                        countCar++;
                        countEnds++;
                }
            }
            umlString.AppendLine(@"</uml:Model>");
            umlString.AppendLine(@"</xmi:XMI>");

            return umlString.Replace("'", "\"");

        }
    }
}
