using SBVRTransformV2.Dicionarios;
using SBVRTransformV2.Componentes;


Extraction extr = new Extraction();
Transformation trans = new Transformation();
SaveXML save = new SaveXML();
Dicionarios dics = new Dicionarios();
string path = AppDomain.CurrentDomain.BaseDirectory;

extr.ClassExtraction(path + @"\XML\ExemploXMISBVR.xml", dics);
save.SaveFile(trans.TransformationUML(dics));



