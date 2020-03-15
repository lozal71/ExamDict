using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LingvaDict
{
    public enum SetModeJob { Undefined, WordList, Translate, User }

    class ModeOfJob
    {
        MenuPool menuPool = new MenuPool();

        public delegate void DJob();
        public Dictionary<SetModeJob, DJob> DictJob { get;}
        Dictionary<SetLanguage, string> dictLingva;
        public ModeOfJob() 
        {
            DictJob = new Dictionary<SetModeJob, DJob>();
            DictJob[SetModeJob.Translate] = new DJob(JobTransalte);
            DictJob[SetModeJob.User] = new DJob(JobUser);
            DictJob[SetModeJob.WordList] = new DJob(JobWordList);
            DictJob[SetModeJob.Undefined] = new DJob(JobNohting);

            dictLingva = new Dictionary<SetLanguage, string>();
            dictLingva[SetLanguage.China] = "китайский";
            dictLingva[SetLanguage.Deutsch] = "немецкий";
            dictLingva[SetLanguage.English] = "английский";
            dictLingva[SetLanguage.Russia] = "русский";
            dictLingva[SetLanguage.Undefined] = "язык не выбран";

        }
        void JobNohting()
        {
            WriteLine("выход из приложения\n");
        }
        void JobTransalte()
        {
            WriteLine("работа с переводом\n");
            ListOfWords wordsOut = new ListOfWords();
            ListOfWords wordsIn = new ListOfWords();
            Translate translate = new Translate();
            Word wordOut = new Word();
            Word wordIn = new Word();
            Word word = null;
            int idOut = 0;
            int idIn = 0;
            string continueJob = "0";
            translate.LingvaOut = (SetLanguage)
               menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка, с которого переводим:");
            translate.LingvaIn = (SetLanguage)
              menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка, на который переводим:");
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
                    Write("Продолжение работы: цифра - 1, выход: цифра 0 -->");
                    continueJob = ReadLine();
                } while (continueJob == "1");
                translate.WriteToXML();
            }
        }
        void JobUser()
        {
            WriteLine("пользователь словаря\n");
            ListOfWords wordsOut = new ListOfWords();
            ListOfWords wordsIn = new ListOfWords();
            Translate translate = new Translate();
            Word wordOut = new Word();
            Word wordIn = new Word();
            Word word = null;
            int idOut = 0;
            List<int> listIdIn = null;
            string continueJob = "0";
            translate.LingvaOut = (SetLanguage)
               menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка, с которого переводим:");
            translate.LingvaIn = (SetLanguage)
              menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка, на который переводим:");
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
                    Write("Продолжение работы: цифра - 1, выход: цифра 0 -->");
                    continueJob = ReadLine();
                } while (continueJob == "1");
            }
        }
        void JobWordList()
        {
            WriteLine("работа со списком слов\n");
            ListOfWords words = new ListOfWords();

            SetLanguage wordsLingva;
            SetActWordsList actWordList = SetActWordsList.Undefined;

            do
            {
                wordsLingva = (SetLanguage)
                    menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка:");
                if (wordsLingva != SetLanguage.Undefined)
                {
                    words.WordLanuage = wordsLingva;
                    words.UserLanguage = wordsLingva;
                    words.ReadFromXML();
                    WriteLine("\n\tВыбран язык: {0}", dictLingva[words.WordLanuage]);
                    Dictionary<SetActWordsList, DJob> dictActWordList =
                                                new Dictionary<SetActWordsList, DJob>();
                    dictActWordList[SetActWordsList.AddWord] =
                                                new DJob(words.GetNewWord);
                    dictActWordList[SetActWordsList.ChangeWord] =
                                                new DJob(words.ChangeWord);
                    dictActWordList[SetActWordsList.RemoveWord] =
                                                new DJob(words.RemoveWord);
                    dictActWordList[SetActWordsList.ShowWords] =
                                                new DJob(words.ShowWordsList);
                    dictActWordList[SetActWordsList.Undefined] =
                                                new DJob(words.DoNothing);
                    do
                    {
                        actWordList = (SetActWordsList)
                            menuPool[SetMenu.SelectActWordsList]().
                            SelectOption("Что вы хотите сделать со списком слов?");
                        dictActWordList[actWordList]();
                    } while (actWordList != SetActWordsList.Undefined);
                }
                words.WriteToXML();
            } while (wordsLingva != SetLanguage.Undefined);
            //words.WrileXML();
        }
    }
}
