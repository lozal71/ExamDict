using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LingvaDict
{
    public enum SetModeJob { Exit, WordList, Translate, User }

    public delegate void DJob();
    class ModeOfJob
    {
        public static event dMenuOption SelectMenu; // событие выбора пункта меню
        public Dictionary<SetModeJob, DJob> DictJob { get;}

        Dictionary<SetLanguage, string> dictLingva;
        public ModeOfJob() 
        {
            DictJob = new Dictionary<SetModeJob, DJob>();
            DictJob[SetModeJob.Translate] = new DJob(JobTransalte);
            DictJob[SetModeJob.User] = new DJob(JobUser);
            DictJob[SetModeJob.WordList] = new DJob(JobWordList);
            DictJob[SetModeJob.Exit] = new DJob(JobNohting);

            dictLingva = new Dictionary<SetLanguage, string>();
            dictLingva[SetLanguage.China] = "китайский";
            dictLingva[SetLanguage.Deutsch] = "немецкий";
            dictLingva[SetLanguage.English] = "английский";
            dictLingva[SetLanguage.Russia] = "русский";
            dictLingva[SetLanguage.Undefined] = "язык не выбран";

        }
        void JobNohting()
        {
            Write("\n\tВыбран режим: ");
            WriteLine("выход из приложения\n");
        }
        void JobTransalte()
        {
            Write("\n\tВыбран режим: ");
            WriteLine("работа с переводом\n");
            ListOfWords wordsOut = new ListOfWords();
            ListOfWords wordsIn = new ListOfWords();
            Translate translate = new Translate();
            Word wordOut = new Word();
            Word wordIn = new Word();
            Word word = null;
            SetMenu continueJob;
            int idOut = 0;
            int idIn = 0;
            SelectMenu += MenuPool.CreateMenuSelectLanguage().SelectOption;
            translate.LingvaOut = (SetLanguage)SelectMenu?.Invoke("Выбор языка, с которого переводим: ");
            translate.LingvaIn = (SetLanguage)SelectMenu?.Invoke("Выбор языка, на который переводим: ");
            SelectMenu = null;
            //translate.LingvaOut = (SetLanguage)
            //   menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка, с которого переводим:");
            //translate.LingvaIn = (SetLanguage)
            //  menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка, на который переводим:");
            if (translate.LingvaOut != SetLanguage.Undefined && translate.LingvaIn != SetLanguage.Undefined)
            {
                wordsOut.WordLanuage = translate.LingvaOut;
                wordsOut.UserLanguage = translate.LingvaOut;
                wordsIn.WordLanuage = translate.LingvaIn;
                wordsIn.UserLanguage = translate.LingvaIn;
                wordsOut.ReadFromXML();
                wordsIn.ReadFromXML();
                translate.ReadFromXML();
                do
                {
                    Write("Введите слово, которое нужно перевести -->");
                    wordOut.WriteLetter = ReadLine();
                    if (wordsOut.IsInList(wordOut.WriteLetter, ref word))
                    {
                        idOut = wordsOut.GetID(word);
                        word = null;
                        Write("Введите слово-перевод -->");
                        wordIn.WriteLetter = ReadLine();
                        if (wordsIn.IsInList(wordIn.WriteLetter, ref word))
                        {
                            idIn = wordsIn.GetID(word);
                        }
                        else
                        {
                            WriteLine("Такого слова нет в списке");
                        }
                    }
                    else
                    {
                        WriteLine("Такого слова нет в списке");
                    }
                    if (idOut != 0 && idIn != 0)
                    {
                        translate.AddNewTranslate(idOut, idIn);
                        WriteLine(wordsOut.GetWord(idOut));
                        WriteLine(wordsIn.GetWord(idIn));
                        //translate.Show();
                    }
                    SelectMenu += MenuPool.CreateMenuContinueStop().SelectOption;
                    continueJob = (SetMenu)SelectMenu?.Invoke("Выберите дальнейшее действие:");
                    SelectMenu = null;
                    //continueJob = (SetMenu)menuPool[SetMenu.ContinueStop]().
                    //                            SelectOption("Выберите дальнейшее действие:");
                } while (continueJob != SetMenu.Undefined);
                translate.WriteToXML();
            }
        }
        void JobUser()
        {
            Write("\n\tВыбран режим: ");
            WriteLine("пользователь словаря\n");
            ListOfWords wordsOut = new ListOfWords();
            ListOfWords wordsIn = new ListOfWords();
            Translate translate = new Translate();
            Word wordOut = new Word();
            Word wordIn = new Word();
            Word word = null;
            int idOut = 0;
            List<int> listIdIn = null;
            SetMenu continueJob;
            SelectMenu += MenuPool.CreateMenuSelectLanguage().SelectOption;
            translate.LingvaOut = (SetLanguage)SelectMenu?.Invoke("Выбор языка, с которого переводим: ");
            translate.LingvaIn = (SetLanguage)SelectMenu?.Invoke("Выбор языка, на который переводим: ");
            SelectMenu = null;
            //translate.LingvaOut = (SetLanguage)
            //   menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка, с которого переводим:");
            //translate.LingvaIn = (SetLanguage)
            //  menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка, на который переводим:");
            if (translate.LingvaOut != SetLanguage.Undefined && translate.LingvaIn != SetLanguage.Undefined)
            {
                wordsOut.WordLanuage = translate.LingvaOut;
                wordsOut.UserLanguage = translate.LingvaOut;
                wordsIn.WordLanuage = translate.LingvaIn;
                wordsIn.UserLanguage = translate.LingvaIn;
                wordsOut.ReadFromXML();
                wordsIn.ReadFromXML();
                translate.ReadFromXML();
                do
                {
                    Write("Введите слово, которое нужно перевести -->");
                    wordOut.WriteLetter = ReadLine();
                    if (wordsOut.IsInList(wordOut.WriteLetter, ref word))
                    {
                        idOut = wordsOut.GetID(wordOut);
                        listIdIn = translate.GetListInID(idOut);
                        WriteLine(wordsOut.GetWord(idOut));
                        foreach (int id in listIdIn)
                        {
                            WriteLine(wordsIn.GetWord(id));
                        }
                    }
                    else
                    {
                        WriteLine("Такого слова нет в списке");
                    }
                    SelectMenu += MenuPool.CreateMenuContinueStop().SelectOption;
                    continueJob = (SetMenu)SelectMenu?.Invoke("Выберите дальнейшее действие:");
                    SelectMenu = null;
                    //continueJob = (SetMenu)menuPool[SetMenu.ContinueStop]().
                    //                            SelectOption("Выберите дальнейшее действие:");
                } while (continueJob != SetMenu.Undefined);
            }
        }
        void JobWordList()
        {
            Write("\n\tВыбран режим: ");
            WriteLine("работа со списком слов\n");

            SetLanguage wordsLingva;
            SetActWordsList actWordList = SetActWordsList.Undefined;
            do
            {
                SelectMenu += MenuPool.CreateMenuSelectLanguage().SelectOption;
                ListOfWords words = new ListOfWords();
                wordsLingva = (SetLanguage)SelectMenu?.Invoke("Выбор языка:");
                SelectMenu = null;
                //wordsLingva = (SetLanguage)
                //    menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка:");
                if (wordsLingva != SetLanguage.Undefined)
                {
                    words.WordLanuage = wordsLingva;
                    words.UserLanguage = wordsLingva;
                    words.ReadFromXML();
                    WriteLine("\n\tВыбран язык: {0}", dictLingva[words.WordLanuage]);
                    do
                    {
                        SelectMenu += MenuPool.CreateMenuWordsList().SelectOption;
                        actWordList = (SetActWordsList)
                            SelectMenu?.Invoke("Что вы хотите сделать со списком слов?");
                        SelectMenu = null;
                        //actWordList = (SetActWordsList)
                        //    menuPool[SetMenu.SelectActWordsList]().
                        //    SelectOption("Что вы хотите сделать со списком слов?");
                        words.dictActWordList[actWordList]();
                    } while (actWordList != SetActWordsList.Undefined);
                    words.WriteToXML();
                }
            } while (wordsLingva != SetLanguage.Undefined);
        }
    }
}
