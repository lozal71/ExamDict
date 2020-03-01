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
        Menu menuSelectPartOfSpeech;
        Menu menuSelectGender;
        public ListOfWords()
        {
            words = new SortedDictionary<Word, int>();
            menuSelectPartOfSpeech = CreateMenuPartOfSpeech();
            menuSelectGender = CreateMenuSelectGender();
        }

        public void Add(Word word)
        {
            Write("Введите буквенное написание слова -->");
            word.WriteLetter = ReadLine();
            word.PartOfSpeech = (SetPartOfSpeech)menuSelectPartOfSpeech.SelectOption("Какая часть речи?");
            if (word.PartOfSpeech != SetPartOfSpeech.Undefined)
            {
                switch (word.PartOfSpeech)
                {
                    case SetPartOfSpeech.Noun:
                        word.GenderNoun = (SetGender)menuSelectGender.
                            SelectOption("Выберите род существительного");
                        Write("Введите форму множественного числа -->");
                        word.PluralForm = ReadLine();
                        Write("Введите смысловое описание слова -->");
                        word.Description = ReadLine();
                        break;
                    case SetPartOfSpeech.Verb:
                        WriteLine("глагол\n");
                        break;
                }
            }
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
        static Menu CreateMenuPartOfSpeech()
        {
            Menu menu = new Menu(3);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t          Существительное - цифра 1");
            menu[2] = new MenuOption("\t                   Глагол - цифра 2");
            return menu;
        }
        static Menu CreateMenuSelectGender()
        {
            Menu menu = new Menu(4);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t                  Мужской - цифра 1");
            menu[2] = new MenuOption("\t                  Женский - цифра 2");
            menu[3] = new MenuOption("\t                  Средний - цифра 3");
            return menu;
        }
    }
}
