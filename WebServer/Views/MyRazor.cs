using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WebServer.Views
{
    internal class MyRazor : XmlDocument
    {
    }

    internal class Master : MyRazor
    {
        public XmlElement Head { get; }
        public XmlElement Body { get; }

        public Master(string path)
        {
            this.Load(path);
            foreach (XmlNode node in DocumentElement)
            {
                var e = node as XmlElement;
                if (e != null)
                {
                    switch (node.Name[0])
                    {
                        case 'h': Head = e; break;
                        case 'b': Body = e; break;
                    }
                }
            }
        }
        public Master()
            : this(Environment.CurrentDirectory + "/views/master.html")
        {

        }

        public void RenderBody(string path)
        {
            using (var sr = new StreamReader(path))
            {
                Body.InnerXml = sr.ReadToEnd();
            }
        }
    }
}
