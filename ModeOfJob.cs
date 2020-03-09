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
        public ModeOfJob() 
        {
            DictJob = new Dictionary<SetModeJob, DJob>();
            DictJob[SetModeJob.Translate] = new DJob(JobTransalte);
            DictJob[SetModeJob.User] = new DJob(JobUser);
            DictJob[SetModeJob.WordList] = new DJob(JobWordList);
            DictJob[SetModeJob.Undefined] = new DJob(JobNohting);

        }
        void JobNohting()
        {
            WriteLine("выход из приложения\n");
        }
        void JobTransalte()
        {
            WriteLine("работа с переводом\n");
        }
        void JobUser()
        {
            WriteLine("пользователь словаря\n");
        }
        void JobWordList()
        {
            WriteLine("работа со списком слов\n");
            ListOfWords words = new ListOfWords();
     
            words.ReadFromXML();

            SetActWordsList actWordList = SetActWordsList.Undefined;

            Dictionary<SetLanguage, string> dictLingva = new Dictionary<SetLanguage, string>();
            dictLingva[SetLanguage.China] = "китайский";
            dictLingva[SetLanguage.Deutsch] = "немецкий";
            dictLingva[SetLanguage.English] = "английский";
            dictLingva[SetLanguage.Russia] = "русский";
            dictLingva[SetLanguage.Undefined] = "язык не выбран";
            do
            {
                words.WordLanuage = (SetLanguage)
                    menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка:");
                words.UserLanguage = words.WordLanuage;
                if (words.WordLanuage != SetLanguage.Undefined)
                {
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
            } while (words.WordLanuage != SetLanguage.Undefined);
            words.WriteToXML();
            //words.ReadFromXML();
        }
    }
}
