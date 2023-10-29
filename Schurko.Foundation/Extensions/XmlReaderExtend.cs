// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.XmlReaderExtend
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.Xml;
using System.Xml.XPath;


#nullable enable
namespace PNI.Extensions
{
  public static class XmlReaderExtend
  {
    public static XmlDocument AsXmlDocument(this XmlReader xmlResult, string rootName)
    {
      XmlDocument xmlDocument = new XmlDocument();
      XPathNavigator navigator = new XPathDocument(xmlResult).CreateNavigator();
      XmlElement element = xmlDocument.CreateElement(rootName);
      element.InnerXml = navigator.OuterXml;
      xmlDocument.AppendChild((XmlNode) element);
      return xmlDocument;
    }

    public static XmlDocument BuildXml(this XmlReader xmlResult) => xmlResult.AsXmlDocument("root");
  }
}
