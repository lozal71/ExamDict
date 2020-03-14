using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Xml;

namespace LingvaDict
{
    class Translate
    {
        Dictionary<int, List<int>> translate;
        public SetLanguage LingvaOut { get; set; }
        public SetLanguage LingvaIn { get; set; }

        public Translate()
        {
            translate = new Dictionary<int, List<int>>();
        }

        public void AddNewTranslate(int idOut, int idIn)
        {
            List<int> list = null;
            if (translate.ContainsKey(idOut))
            {
                translate.TryGetValue(idOut, out list);
                list.Add(idIn);
            }
            else
            {
                list = new List<int>();
                list.Add(idIn);
                translate.Add(idOut, list);
            }
        }
        public void Show()
        {
            foreach (int i in translate.Keys)
            {
                Write(i.ToString() + ",");
                foreach (int k in translate[i])
                {
                    Write(k.ToString() + ",");
                }
                WriteLine();
            }
        }
        public void WriteToXML()
        {
            string wordsFileName;
            XmlTextWriter writer = null;
            int count = 1;
            try
            {
                //XmlWriterSettings settings = new XmlWriterSettings();
                //settings.Indent = true;
                wordsFileName = LingvaOut.ToString() + "-" + LingvaIn.ToString() + ".xml";
                writer = new XmlTextWriter(wordsFileName, System.Text.Encoding.Unicode);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Translate");
                //writer.WriteStartElement("book");

                // Write the namespace declaration.
                //writer.WriteAttributeString("xmlns", "bk", null, "urn:samples");

                // Write the genre attribute.
                foreach (int i in translate.Keys)
                {
                    writer.WriteStartElement("idOut", i.ToString());
                //writer.WriteAttributeString("genre", "novel");
                    //writer.WriteAttributeString("1", "0");
                    foreach (int k in translate[i])
                    {
                        writer.WriteAttributeString(count.ToString(), k.ToString());
                        count++;
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                WriteLine("The {0} file is generated!", wordsFileName);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
