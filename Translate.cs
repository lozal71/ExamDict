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
        public static event dMenuOption SelectMenu; // событие выбора пункта меню

        Dictionary<int, List<int>> translateDict;
        public ListOfWords wordsOut;
        public ListOfWords wordsIn;
        public SetLanguage LingvaOut { get; set; }
        public SetLanguage LingvaIn { get; set; }

        public Translate()
        {
            translateDict = new Dictionary<int, List<int>>();
            wordsOut = new ListOfWords();
            wordsIn = new ListOfWords();
            ListFilling();
        }

        public bool ListFilling()
        {
            //Word wordOut = new Word();
            //Word wordIn = new Word();
            SelectMenu += MenuPool.CreateMenuSelectLanguage().SelectOption;
            LingvaOut = (SetLanguage)SelectMenu?.Invoke("Выбор языка, с которого переводим: ");
            LingvaIn = (SetLanguage)SelectMenu?.Invoke("Выбор языка, на который переводим: ");
            SelectMenu = null;
            if (LingvaOut != SetLanguage.Undefined && LingvaIn != SetLanguage.Undefined)
            {
                wordsOut.WordLanuage = LingvaOut;
                wordsOut.UserLanguage = LingvaOut;
                wordsIn.WordLanuage = LingvaIn;
                wordsIn.UserLanguage = LingvaIn;
                wordsOut.ReadFromXML();
                wordsIn.ReadFromXML();
                ReadFromXML();
                return true;
            }
            else
            {
                return false;
            }
        }
            public void AddNewTranslate(int idOut, int idIn)
        {
            List<int> list = null;
            if (translateDict.ContainsKey(idOut))
            {
                translateDict.TryGetValue(idOut, out list);
                list.Add(idIn);
            }
            else
            {
                list = new List<int>();
                list.Add(idIn);
                translateDict.Add(idOut, list);
            }
        }

        public List<int> GetListInID(int outID)
        {
            List<int> list = null;
            foreach (int i in translateDict.Keys)
            {
                if (i == outID)
                {
                    list = translateDict[i];
                    break;
                }
            }
            return list;
        }
        public void Show()
        {
            foreach (int i in translateDict.Keys)
            {
                Write(i.ToString() + ",");
                foreach (int k in translateDict[i])
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
                foreach (int i in translateDict.Keys)
                {
                    writer.WriteStartElement("idOut_"+i.ToString());
                    listSerializer.Serialize(writer, translateDict[i]);
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
                    translateDict.Add(wordID, list);
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
