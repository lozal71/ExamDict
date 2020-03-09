using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Xml;
using static System.Convert;

namespace LingvaDict
{
    public enum SetLanguage { Undefined, Russia, English, Deutsch, China };
    public enum SetActWordsList { Undefined, AddWord, RemoveWord, ChangeWord, ShowWords }
    public enum SetModeWrite { Undefined, Letters, Hieroglyph }
    public class ListOfWords : IEnumerable<Word>
    {
        SortedDictionary<Word, int> words;
        public delegate void DPartOfSpecch(ref Word word);
        Dictionary<SetPartOfSpeech, DPartOfSpecch> dictPartofSpeech;

        MenuPool menuPool = new MenuPool();

        public SetLanguage WordLanuage { get; set; }
        public SetLanguage UserLanguage { get; set; }
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

        //Word IEnumerator<Word>.Current => throw new NotImplementedException();

        //object IEnumerator.Current => throw new NotImplementedException();

        public ListOfWords()
        {
            words = new SortedDictionary<Word, int>();
            dictPartofSpeech = new Dictionary<SetPartOfSpeech, DPartOfSpecch>();
            dictPartofSpeech[SetPartOfSpeech.Noun] = new DPartOfSpecch(SetNewNoun);
            dictPartofSpeech[SetPartOfSpeech.Verb] = new DPartOfSpecch(SetNewVerb);
            dictPartofSpeech[SetPartOfSpeech.Undefined] = new DPartOfSpecch(SetUndefinedWord);
        }

        public void DoNothing()
        {
            WriteLine("ничего не делать со списком слов \n");
        }
        public void RemoveWord()
        {
            WriteLine("удалить запись \n");
            Word deleteWord = new Word();
            Write("Введите слово, которое нужно удалить -->");
            deleteWord.WriteLetter = ReadLine();
            foreach (Word w in words.Keys)
            {
                if (w.WriteLetter.Equals(deleteWord.WriteLetter))
                {
                    WriteLine("Найдено слово:");
                    WriteLine(w);
                    w.DeleteMarker = true;
                    break;
                }
            }
        }

        public void ChangeWord()
        {
            //WriteLine("редактировать запись \n");
            Word changeWord = new Word();
            Write("Введите слово, которое нужно изменить -->");
            changeWord.WriteLetter = ReadLine();
            foreach (Word w in words.Keys)
            {
                if (w.WriteLetter.Equals(changeWord.WriteLetter))
                {
                    WriteLine("Найдено слово:");
                    WriteLine(w);
                    changeWord = w;
                    break;
                }
            }
            Write("Введите буквенное написание слова -->");
            changeWord.WriteLetter = ReadLine();
            changeWord.PartOfSpeech = (SetPartOfSpeech)
                menuPool[SetMenu.SelectPartOfSpeech]().SelectOption("Какая часть речи?");
            dictPartofSpeech[changeWord.PartOfSpeech](ref changeWord);
            Write("Введите смысловое описание слова -->");
            changeWord.Description = ReadLine();
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
        public void GetNewWord()
        {
            Word word = new Word();
            WriteLine("добавить запись \n");
            Write("Введите буквенное написание слова -->");
            word.WriteLetter = ReadLine();
            word.PartOfSpeech = (SetPartOfSpeech)
                menuPool[SetMenu.SelectPartOfSpeech]().SelectOption("Какая часть речи?");
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
            word.GenderNoun = (SetGender)
                menuPool[SetMenu.SelectGender]().SelectOption("Выберите род существительного");
            Write("Введите форму множественного числа -->");
            word.PluralForm = ReadLine();
        }
        void SetNewVerb(ref Word word)
        {
            WriteLine("глагол\n");
            word.Transitive = (SetTransitiveForm)
                menuPool[SetMenu.SelectTransitive]().SelectOption("Этот глагол переходный/непереходный?");
            word.ConjugationType = (SetConjugationType)
                menuPool[SetMenu.SelectСonjugationType]().
                SelectOption("Выберите спряжение (сильное/слабое):");
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
                writer = new XmlTextWriter(wordsFileName, System.Text.Encoding.Unicode);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Words");
                foreach (Word w in words.Keys)
                {
                    writer.WriteStartElement("Word_" + words[w].ToString());
                    writer.WriteElementString("WriteLetter", w.WriteLetter);
                    writer.WriteElementString("Pronounce", w.Pronounce);
                    writer.WriteElementString("PartOfSpeach", ToInt32(w.PartOfSpeech).ToString());
                    writer.WriteElementString("AuxiliaryVerb", w.AuxiliaryVerb);
                    writer.WriteElementString("ConjugationType", ToInt32(w.ConjugationType).ToString());
                    writer.WriteElementString("GenderNoun", ToInt32(w.GenderNoun).ToString());
                    writer.WriteElementString("PluralForm", w.PluralForm);
                    writer.WriteElementString("Transitive", ToInt32(w.Transitive).ToString());
                    writer.WriteElementString("Description", w.Description);
                    writer.WriteElementString("DeleteMarker", w.DeleteMarker.ToString());
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
            XmlTextReader reader = null;
            int wordID;
            string wordsFileName;
            wordsFileName = WordLanuage.ToString() +".xml";
            try
            {
                reader = new XmlTextReader(wordsFileName);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name.Contains("Word_")) 
                    {
                        Word word = new Word();
                        wordID = ToInt32(reader.Name.Split('_')[1]);
                        reader.Read();
                        reader.Read();
                        word.WriteLetter = reader.Value;
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        word.Pronounce = reader.Value;
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        word.PartOfSpeech = (SetPartOfSpeech)ToInt32(reader.Value);
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        word.AuxiliaryVerb = reader.Value;
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        word.ConjugationType = (SetConjugationType)ToInt32(reader.Value);
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        word.GenderNoun =(SetGender)ToInt32(reader.Value);
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        word.PluralForm = reader.Value;
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        word.Transitive =(SetTransitiveForm)ToInt32(reader.Value);
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        word.Description = reader.Value;
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        word.DeleteMarker = ToBoolean(reader.Value);
                        if (!words.ContainsKey(word))
                        {
                            words.Add(word, words.Count + 1);
                        }
                    }
                }
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
