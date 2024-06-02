using SBVRTransformV2.Dicionarios;
using SBVRTransformV2.Componentes;


Extraction extr = new Extraction();
Transformation trans = new Transformation();
SaveXML save = new SaveXML();
Dicionarios dics = new Dicionarios();
string relativePath = @"..\..\..\ExemploXMISBVR.xml";
string absolutePath = Path.GetFullPath(relativePath);


extr.ClassExtraction(absolutePath, dics);
save.SaveFile(trans.TransformationUML(dics));



