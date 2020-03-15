using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using static System.Convert;

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

        public List<int> GetListInID(int outID)
        {
            List<int> list = null;
            foreach (int i in translate.Keys)
            {
                if (i == outID)
                {
                    list = translate[i];
                    break;
                }
            }
            return list;
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
            try
            {
                wordsFileName = LingvaOut.ToString() + "-" + LingvaIn.ToString() + ".xml";
                writer = new XmlTextWriter(wordsFileName, Encoding.Unicode);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Translate");
                XmlSerializer listSerializer = new XmlSerializer(typeof(List<int>));
                foreach (int i in translate.Keys)
                {
                    writer.WriteStartElement("idOut_"+i.ToString());
                    listSerializer.Serialize(writer, translate[i]);
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
        public void ReadFromXML()
        {
            int wordID;
            string wordsFileName;
            wordsFileName = LingvaOut.ToString() + "-" + LingvaIn.ToString() + ".xml";
            XmlTextReader reader = null;
            List<int> list = new List<int>();
            XmlSerializer listSerializer = new XmlSerializer(typeof(List<int>));
            try
            {
                reader = new XmlTextReader(wordsFileName);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                reader.ReadStartElement();
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    wordID = ToInt32(reader.Name.Split('_')[1]);
                    reader.ReadStartElement();
                    list = (List<int>)listSerializer.Deserialize(reader);
                    translate.Add(wordID, list);
                    list = null;
                    reader.ReadEndElement();
                    reader.MoveToContent();
                }
                reader.ReadEndElement();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();

            }
        }
    }
}
