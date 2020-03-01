using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LingvaDict
{
    public enum SetLanguage { Undefined, Russia, English, Deutsch, China };
    public enum SetModeWrite { Undefined, Letters, Hieroglyph }
    enum SetActWordsList { Undefined, AddWord, RemoveWord, ChangeWord}
    class Program
    {
        static void Main(string[] args)
        {
            SetLanguage wordLingva = SetLanguage.Undefined;
            SetLanguage userLingva = SetLanguage.Undefined;
            SetModeWrite modeWrite = SetModeWrite.Undefined;
            Menu menuModeOfUsing = CreateMenuModeOfUsing();
            Menu menuSelectLanguage = CreateMenuSelectLanguage();
            Menu menuSelectActWordsList = CreateMenuWordsList();
            int modeOfUsing = 0;
            SetActWordsList actWordList = SetActWordsList.Undefined;
            do
            {
                modeOfUsing = menuModeOfUsing.SelectOption("Выбор режима работы:");
                if (modeOfUsing != 0)
                {
                    Write("\n\tВыбран режим: ");
                    switch (modeOfUsing)
                    {
                        case 1:
                            WriteLine("работа со списком слов\n");
                            ListOfWords words = new ListOfWords();
                            do
                            {
                                wordLingva = (SetLanguage)menuSelectLanguage.SelectOption("Выбор языка:");
                                userLingva = wordLingva;
                                if (wordLingva != SetLanguage.Undefined)
                                {
                                    if (wordLingva == SetLanguage.China)
                                    {
                                        modeWrite = SetModeWrite.Hieroglyph;
                                    }
                                    else
                                    {
                                        modeWrite = SetModeWrite.Letters;
                                    }
                                    Write("\n\tВыбран язык: ");
                                    switch (wordLingva)
                                    {
                                        case SetLanguage.Russia:
                                            WriteLine("русский\n");
                                            break;
                                        case SetLanguage.English:
                                            WriteLine("английский\n");
                                            break;
                                        case SetLanguage.Deutsch:
                                            WriteLine("немецкий\n");
                                            break;
                                        case SetLanguage.China:
                                            WriteLine("китайский\n");
                                            break;
                                    }
                                    do
                                    {
                                        actWordList = (SetActWordsList)menuSelectActWordsList.
                                                        SelectOption("Что вы хотите сделать со списком слов?");
                                        if (actWordList != SetActWordsList.Undefined)
                                        {
                                            Write("\n\tВыбрано действие: ");
                                            switch (actWordList)
                                            {
                                                case SetActWordsList.AddWord:
                                                    WriteLine("добавить запись \n");
                                                    Word word = new Word(wordLingva, userLingva, modeWrite);
                                                    words.Add(word);
                                                    break;
                                                case SetActWordsList.RemoveWord:
                                                    WriteLine("удалить запись \n");
                                                    break;
                                                case SetActWordsList.ChangeWord:
                                                    WriteLine("редактировать запись \n");
                                                    break;
                                            }
                                        }
                                    } while (actWordList != SetActWordsList.Undefined);
                                }
                            } while (wordLingva != SetLanguage.Undefined);
                                break;
                        case 2:
                            WriteLine("работа с переводом\n");
                            break;
                        case 3:
                            WriteLine("пользователь словаря\n");
                            break;
                    }
                }
            } while (modeOfUsing != 0);
        }

        static Menu CreateMenuModeOfUsing()
        {
            Menu menu = new Menu(4);
            menu[0] = new MenuOption("\t      Выход из приложения - цифра 0 -->");
            menu[1] = new MenuOption("\t   Работа со списком слов - цифра 1");
            menu[2] = new MenuOption("\t       Работа с переводом - цифра 2");
            menu[3] = new MenuOption("\t     Пользователь словаря - цифра 3");
            return menu;
        }
        static Menu CreateMenuSelectLanguage()
        {
            Menu menu = new Menu(5);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t             Русский язык - цифра 1");
            menu[2] = new MenuOption("\t          Английский язык - цифра 2");
            menu[3] = new MenuOption("\t            Немецкий язык - цифра 3");
            menu[4] = new MenuOption("\t           Китайский язык - цифра 4");
            return menu;
        }
        static Menu CreateMenuWordsList()
        {
            Menu menu = new Menu(5);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t           Добавить слово - цифра 1");
            menu[2] = new MenuOption("\t            Удалить слово - цифра 2");
            menu[3] = new MenuOption("\t      Редактировать слово - цифра 3");
            menu[4] = new MenuOption("\t           Показать слова - цифра 4");
            return menu;
        }
    }
}
