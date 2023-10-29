// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.TransformExtension
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;


#nullable enable
namespace PNI.Extensions
{
  public static class TransformExtension
  {
    public static string Transform(this XmlDocument obj, string xslUrl)
    {
      XmlDocument xslDoc = new XmlDocument();
      xslDoc.Load(xslUrl);
      return TransformExtension.PerformTransform(obj, (XsltArgumentList) null, xslDoc, (XmlWriterSettings) null);
    }

    public static string Transform(this XmlDocument obj, XsltArgumentList args, XmlDocument xslDoc) => TransformExtension.PerformTransform(obj, args, xslDoc, (XmlWriterSettings) null);

    private static string PerformTransform(
      XmlDocument xmlToTransform,
      XsltArgumentList args,
      XmlDocument xslDoc,
      XmlWriterSettings writerSettings)
    {
      XslCompiledTransform compiledTransform = new XslCompiledTransform();
      compiledTransform.Load((IXPathNavigable) xslDoc);
      XmlWriterSettings settings = writerSettings;
      if (settings == null)
      {
        settings = new XmlWriterSettings();
        settings.OmitXmlDeclaration = true;
        settings.ConformanceLevel = ConformanceLevel.Fragment;
      }
      StringBuilder output = new StringBuilder();
      XmlWriter results = XmlWriter.Create(output, settings);
      compiledTransform.Transform((IXPathNavigable) xmlToTransform, args, results);
      return output.ToString();
    }
  }
}
