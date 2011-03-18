using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using roundhouse.runners;
using StructureMap;

namespace visual.roundhouse.Models
{
    public static class XElementExtender
    {
        public static string GetAttributeValue(this XElement element, string attributeName)
        {
            return element.GetAttributeValue(attributeName, string.Empty);
        }

        public static string GetAttributeValue(this XElement element, string attributeName, string defaultValue)
        {
            XAttribute attr = element.Attribute(attributeName);
            return (attr != null) ? attr.Value : defaultValue;
        }
        public static string GetElementValue(this XElement element, string childElementName)
        {
            return element.GetElementValue(childElementName, string.Empty);
        }
        public static string GetElementValue(this XElement element, string childElementName, string defaultValue)
        {
            XElement child = element.Element(childElementName);
            return child != null ? child.Value : defaultValue;
        }
    }
    public class KickModel
    {
        public KickModel()
        {
            Connections = new List<Connection>();
            SqlFiles = new KickDirectorySet();
            Control = new KickControl();

        }
        public KickModel(FileInfo fileInfo) : this()
        {
            FileName = fileInfo.Name;
            XDocument doc = XDocument.Load(fileInfo.FullName);
            Load(doc.Root);
        }
        public KickModel(XElement modelElement)
            : this()
        {
            Load(modelElement);
        }
        public void Load(XElement modelElement)
        {
            Connections.Clear();
            Connections.AddRange(
                from element in modelElement.Elements("Connections").Elements("Connection") 
                select new Connection(element)
            );

            SqlFiles.Load(modelElement.Element("SqlFiles"));
            Control.Load(modelElement.Element("Control"));
        }
        public string FileName { get; set; }
        public List<Connection> Connections { get; private set; }
        public KickDirectorySet SqlFiles { get; private set; }
        public KickControl Control { get; private set; }
    }
    public class Connection
    {
        private List<ScriptInformation> _scriptInformation = null;
        public Connection()
        {
        }
        public Connection(XElement element)
        {
            Load(element);
        }
        public void Load(XElement element)
        {
            ServerName = element.GetAttributeValue("ServerName");
            Database = element.GetAttributeValue("Database");
            ConnectionString = element.GetAttributeValue("ConnectionString");
        }
        public string ServerName { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }
        public IEnumerable<ScriptInformation> ScriptInformation
        {
            get { return _scriptInformation?? (_scriptInformation = ReloadScriptInformation());}
        }
        private List<ScriptInformation> ReloadScriptInformation()
        {
            List<ScriptInformation> result  = new List<ScriptInformation>();
            IWalker walker = ObjectFactory.GetInstance<IWalker>();
            walker.TraverseFiles( result.Add );
            return result;
        }
    }
    public class KickDirectorySet
    {
        public void Load(XElement element)
        {
            Root = element.GetAttributeValue("Root");
            Up = element.GetElementValue("up");
            AfterUp = element.GetElementValue("afterup");
            Sprocs = element.GetElementValue("sprocs");
            Functions = element.GetElementValue("functions");
            Views = element.GetElementValue("views");
        }
        public string Root { get; set; }
        public string Up { get; set; }
        public string AfterUp { get; set; }
        public string Sprocs { get; set; }
        public string Functions { get; set; }
        public string Views { get; set; }
    }
    public class KickControl
    {
        public void Load(XElement element)
        {
            WithTransactions = bool.Parse(element.GetAttributeValue("WithTransactions"));
            CreationMode =
                (CreationModes) Enum.Parse(typeof (CreationModes), element.GetAttributeValue("CreationMode"), true);
        }

        public bool WithTransactions { get; set; }
        public CreationModes CreationMode { get; set; }
    }
    public enum CreationModes
    {
        UpdateOnly,
        CreateIfNotPresent,
        Recreate
    }
}
