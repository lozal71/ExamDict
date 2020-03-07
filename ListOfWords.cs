using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LingvaDict
{
    public enum SetLanguage { Undefined, Russia, English, Deutsch, China };
    public enum SetActWordsList { Undefined, AddWord, RemoveWord, ChangeWord, ShowWords }
    public enum SetModeWrite { Undefined, Letters, Hieroglyph }
    public class ListOfWords
    {
        public delegate void DPartOfSpecch(ref Word word);
        SortedDictionary<Word, int> words;
        SetLanguage wordLanuage;
        SetLanguage userLanguage;
        SetModeWrite modeWrite;
        Dictionary<SetPartOfSpeech, DPartOfSpecch> dictPartofSpeech;

        MenuPool menuPool = new MenuPool();

        public ListOfWords(SetLanguage wordLanuage, SetLanguage userLanguage)
        {
            words = new SortedDictionary<Word, int>();
            this.wordLanuage = wordLanuage;
            this.userLanguage = userLanguage;
            if (this.wordLanuage == SetLanguage.China)
            {
                modeWrite = SetModeWrite.Hieroglyph;
            }
            else
            {
                modeWrite = SetModeWrite.Letters;
            }
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
        }

        public void ChangeWord()
        {
            WriteLine("редактировать запись \n");
        }
        public void ShowWordsList()
        {
            WriteLine("Вы ввели -->");
            foreach (Word w in words.Keys)
            {
                WriteLine("Запись {0}: {1} ", words[w], w);
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
            ShowWordsList();
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
            else
            {
                word.AuxiliaryVerb = "";
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
                WriteLine("Слово {} уже есть в списке", word.WriteLetter);
            }
        }
    }
}
