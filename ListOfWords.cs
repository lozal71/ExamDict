using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LingvaDict
{
    public class ListOfWords
    {
        SortedDictionary<Word, int> words;
        SetLanguage wordLanuage;
        SetLanguage userLanguage;
        SetModeWrite modeWrite;

        public static MenuPool menuPool = new MenuPool();

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
        }

        public void Add(Word word)
        {
            WriteLine("Вы ввели -->");
            if (!words.ContainsKey(word))
            {
                words.Add(word, words.Count + 1);
            }
            foreach (Word w in words.Keys)
            {
                WriteLine("Запись {0}: {1} ", words[w], w);
            }
        }

        public void DoNothing()
        {

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
            WriteLine("показать список слов \n");
        }
        public void GetNewWord()
        {
            Word tempWord = new Word();
            WriteLine("добавить запись \n");
            Write("Введите буквенное написание слова -->");
            tempWord.WriteLetter = ReadLine();
            tempWord.PartOfSpeech = (SetPartOfSpeech)
                menuPool[SetMenu.menuSelectPartOfSpeec]().SelectOption("Какая часть речи?");
            if (tempWord.PartOfSpeech != SetPartOfSpeech.Undefined)
            {
                switch (tempWord.PartOfSpeech)
                {
                    case SetPartOfSpeech.Noun:
                        tempWord.GenderNoun = (SetGender)
                            menuPool[SetMenu.menuSelectGender]().SelectOption("Выберите род существительного");
                        Write("Введите форму множественного числа -->");
                        tempWord.PluralForm = ReadLine();
                        Write("Введите смысловое описание слова -->");
                        tempWord.Description = ReadLine();
                        break;
                    case SetPartOfSpeech.Verb:
                        WriteLine("глагол\n");
                        break;
                }
            }
            WriteLine("Вы ввели -->");
            if (!words.ContainsKey(tempWord))
            {
                words.Add(tempWord, words.Count + 1);
            }
            foreach (Word w in words.Keys)
            {
                WriteLine("Запись {0}: {1} ", words[w], w);
            }
        }
    }
}
