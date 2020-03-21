using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Xml;
using static System.Convert;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace LingvaDict
{
    /// <summary>
    /// набор языков
    /// </summary>
    public enum SetLanguage 
    { 
        /// <summary>
        /// язык не определен
        /// </summary>
        Undefined, 
        /// <summary>
        /// русский
        /// </summary>
        Russia, 
        /// <summary>
        /// английский
        /// </summary>
        English, 
        /// <summary>
        /// немецкий
        /// </summary>
        Deutsch, 
        /// <summary>
        /// китайский
        /// </summary>
        China 
    };
    /// <summary>
    /// набор действий для списка слов
    /// </summary>
    public enum SetActWordsList 
    { 
        /// <summary>
        /// действие не определено
        /// </summary>
        Undefined, 
        /// <summary>
        /// добавить слово
        /// </summary>
        AddWord, 
        /// <summary>
        /// удалить слово
        /// </summary>
        RemoveWord, 
        /// <summary>
        /// изменить слово
        /// </summary>
        ChangeWord, 
        /// <summary>
        /// показать слова
        /// </summary>
        ShowWords 
    }
    /// <summary>
    /// набор видов написания слова
    /// </summary>
    public enum SetModeWrite 
    { 
        /// <summary>
        /// вид написания слова не определен
        /// </summary>
        Undefined, 
        /// <summary>
        /// вид написания - буквенный
        /// </summary>
        Letters, 
        /// <summary>
        /// вид написания - иероглифический
        /// </summary>
        Hieroglyph 
    }

    /// <summary>
    /// Класс содержит свойства и методы для работы со списком слов
    /// </summary>
    [XmlRoot("List")]
    public class ListOfWords : IEnumerable<Word>, IXmlSerializable
    {
        /// <summary>
        /// событие выбора пункта меню
        /// </summary>
        public static event dMenuOption SelectMenu; 

        SortedDictionary<Word, int> words;
        /// <summary>
        /// делегат для работы с частью речи
        /// </summary>
        /// <param name="word"> ссылка на слово </param>
        public delegate void DPartOfSpeech(ref Word word);

        Dictionary<SetPartOfSpeech, DPartOfSpeech> dictPartofSpeech;

        /// <summary>
        /// словарь действий со списком : ключ - название действия, значение - делегат-действие
        /// </summary>
        public Dictionary<SetActWordsList, DJob> dictActWordList;

        /// <summary>
        /// свойство определяет язык слов в списке
        /// </summary>
        public SetLanguage WordLanuage { get; set; }
        /// <summary>
        /// свойство определяет язык пользователя
        /// </summary>
        public SetLanguage UserLanguage { get; set; }
        /// <summary>
        /// свйоство определяет способ написания слова
        /// </summary>
        public SetModeWrite ModeWrite
        {
            get
            {
                if (this.WordLanuage == SetLanguage.China)
                {
                    return SetModeWrite.Hieroglyph;
                }
                else
                {
                    return SetModeWrite.Letters;
                }
            }
        }
        /// <summary>
        /// Конструктор, в котором инициализируется:
        /// 1. словарь частей речи.
        /// в котором ключ - название части речи, значение - делегат-метод заполнения
        /// параметров части речи
        /// 2. словарь действий со списком
        /// </summary>
        public ListOfWords()
        {
            words = new SortedDictionary<Word, int>();
            dictPartofSpeech = new Dictionary<SetPartOfSpeech, DPartOfSpeech>();
            dictPartofSpeech[SetPartOfSpeech.Noun] = new DPartOfSpeech(SetNewNoun);
            dictPartofSpeech[SetPartOfSpeech.Verb] = new DPartOfSpeech(SetNewVerb);
            dictPartofSpeech[SetPartOfSpeech.Undefined] = new DPartOfSpeech(SetUndefinedWord);

            dictActWordList = new Dictionary<SetActWordsList, DJob>();
            dictActWordList[SetActWordsList.AddWord] = new DJob(GetNewWord);
            dictActWordList[SetActWordsList.ChangeWord] = new DJob(ChangeWord);
            dictActWordList[SetActWordsList.RemoveWord] = new DJob(RemoveWord);
            dictActWordList[SetActWordsList.ShowWords] = new DJob(ShowWordsList);
            dictActWordList[SetActWordsList.Undefined] = new DJob(DoNothing);

        }
        /// <summary>
        /// метод для обозначения, что со списком ничего не надо делать
        /// </summary>
        public void DoNothing()
        {
            WriteLine("ничего не делать со списком слов \n");
        }
        /// <summary>
        /// метод проверяет, есть ли буквенное написание в списке слов
        /// </summary>
        /// <param name="wordLetter"> буквенное написание </param>
        /// <param name="word"> ссылка на объект класса Word, 
        /// в котором есть искомое буквенное написание</param>
        /// <returns> возвращает true, если буквенное написание найдено </returns>
        public bool IsInList(string wordLetter, ref Word word)
        {
            foreach (Word w in words.Keys)
            {
                if (w.WriteLetter.Equals(wordLetter))
                {
                    word = w;
                    return true;
                }
            }
            return false;
        }
        public void RemoveWord()
        {
            //WriteLine("удалить запись \n");
            Word deleteWord = new Word();
            Word word = null;
            Write("Введите слово, которое нужно удалить -->");
            deleteWord.WriteLetter = ReadLine();
            if (IsInList(deleteWord.WriteLetter,ref word))
            {
                word.DeleteMarker = true;
            }
        }

        public void ChangeWord()
        {
            //WriteLine("редактировать запись \n");
            Word changeWord = new Word();
            Word word = null;
            Write("Введите слово, которое нужно изменить -->");
            changeWord.WriteLetter = ReadLine();
            if (IsInList(changeWord.WriteLetter, ref word))
            {
                Write("Введите буквенное написание слова -->");
                word.WriteLetter = ReadLine();
                SelectMenu += MenuPool.CreateMenuPartOfSpeech().SelectOption;
                word.PartOfSpeech = (SetPartOfSpeech)SelectMenu?.Invoke("Какая часть речи?");
                SelectMenu = null;
                dictPartofSpeech[word.PartOfSpeech](ref word);
                Write("Введите смысловое описание слова -->");
                word.Description = ReadLine();
            }
        }
        public int GetID(Word w)
        {
            int id;
           if (!w.DeleteMarker)
            {
                words.TryGetValue(w, out id);
                return id;
            }
            else
            {
                return 0;
            }

        }
        public void ShowWordsList()
        {
            WriteLine("Вы ввели -->");
            int k = 0;
            try
            {
                foreach (Word w in words.Keys)
                {
                    if (!w.DeleteMarker)
                    {
                        words.TryGetValue(w, out k);
                        WriteLine("Запись {0}: {1} ", k, w);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
        public Word GetWord(int wordID)
        {
            Word word = null;
            foreach (Word w in words.Keys)
            {
                if (words[w] == wordID)
                {
                    word = w;
                    break;
                }
            }
            return word;
        }
        public void GetNewWord()
        {
            Word word = new Word();
            WriteLine("добавить запись \n");
            Write("Введите буквенное написание слова -->");
            word.WriteLetter = ReadLine();
            SelectMenu += MenuPool.CreateMenuPartOfSpeech().SelectOption;
            word.PartOfSpeech = (SetPartOfSpeech)SelectMenu?.Invoke("Какая часть речи?");
            SelectMenu = null;
            dictPartofSpeech[word.PartOfSpeech](ref word);
            Write("Введите смысловое описание слова -->");
            word.Description = ReadLine();
            AddNewWord(word);
        }
        void SetUndefinedWord(ref Word word)
        {
            WriteLine("часть речи неопределена\n");
        }
        void SetNewNoun(ref Word word)
        {
            WriteLine("существительное\n");
            SelectMenu += MenuPool.CreateMenuSelectGender().SelectOption;
            word.GenderNoun = (SetGender)SelectMenu?.Invoke("Выберите род существительного");
            SelectMenu = null;
            Write("Введите форму множественного числа -->");
            word.PluralForm = ReadLine();
        }
        void SetNewVerb(ref Word word)
        {
            WriteLine("глагол\n");
            SelectMenu += MenuPool.CreateMenuSelectTransitive().SelectOption;
            word.Transitive = (SetTransitiveForm)SelectMenu?.Invoke("Этот глагол переходный/непереходный?");
            SelectMenu = null;
            SelectMenu += MenuPool.CreateMenuSelectСonjugationType().SelectOption;
            word.ConjugationType = (SetConjugationType)
                SelectMenu?.Invoke("Выберите спряжение (сильное/слабое):");
            SelectMenu = null;
            if (word.ConjugationType == SetConjugationType.Strong)
            {
                Write("Введите глагол, с которым спрягается -->");
                word.AuxiliaryVerb = ReadLine();
            }
        }
        void AddNewWord(Word word)
        {
            if (!words.ContainsKey(word))
            {
                words.Add(word, words.Count + 1);
            }
            else
            {
                WriteLine("Слово {0} уже есть в списке", word.WriteLetter.ToString());
            }
        }

        IEnumerator<Word> IEnumerable<Word>.GetEnumerator()
        {
            //throw new NotImplementedException();
            foreach (Word w in words.Keys)
            {
                yield return w;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //throw new NotImplementedException();
            foreach (Word w in words.Keys)
            {
                yield return w;
            }
        }
        public void WriteToXML()
        {
            string wordsFileName;
            XmlTextWriter writer = null;
            try
            {
                wordsFileName = WordLanuage.ToString() + ".xml";
                writer = new XmlTextWriter(wordsFileName, Encoding.Unicode);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Words");
                WriteXml(writer);
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
            XmlTextReader reader = null;
            string wordsFileName;
            wordsFileName = WordLanuage.ToString() +".xml";
            try
            {
                reader = new XmlTextReader(wordsFileName);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                ReadXml(reader);
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

        XmlSchema IXmlSerializable.GetSchema()
        {
            //throw new NotImplementedException();
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            int wordID;
            XmlSerializer wordSerializer = new XmlSerializer(typeof(Word));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            reader.Read();
            reader.Read();
            if (wasEmpty)
                return;
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                wordID = ToInt32(reader.Name.Split('_')[1]);
                reader.Read();
                Word word = (Word)wordSerializer.Deserialize(reader);
                words.Add(word, wordID);
                reader.ReadEndElement();
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer wordsSerializer = new XmlSerializer(typeof(Word));
            foreach (Word w in words.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("word_" + words[w].ToString());
                wordsSerializer.Serialize(writer, w);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
    }
}
