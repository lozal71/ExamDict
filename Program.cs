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
    public enum SetActWordsList { Undefined, AddWord, RemoveWord, ChangeWord, ShowWords}
    public enum SetModeJob { Undefined, WordList, Translate, User}

    class Program
    {
        public static MenuPool menuPool = new MenuPool();
        public delegate void DJob();
        static void Main(string[] args)
        {
            Dictionary<SetModeJob, DJob> dictJob = new Dictionary<SetModeJob, DJob>();
            dictJob[SetModeJob.Translate] = new DJob(JobTransalte); 
            dictJob[SetModeJob.User] = new DJob(JobUser); 
            dictJob[SetModeJob.WordList] = new DJob(JobWordList); ;
            dictJob[SetModeJob.Undefined] = new DJob(JobNohting); ;
            int modeOfUsing = 0;
            do
            {
                modeOfUsing = menuPool[SetMenu.ModeOfUsing]().SelectOption("Выбор режима работы:");
                Write("\n\tВыбран режим: ");
                dictJob[(SetModeJob)modeOfUsing]();
            } while (modeOfUsing != 0);
        }
        static void JobNohting()
        {
            WriteLine("выход из приложения\n");
        }
        static void JobTransalte()
        {
            WriteLine("работа с переводом\n");
        }
        static void JobUser()
        {
            WriteLine("пользователь словаря\n");
        }
        static void JobWordList()
        {
            WriteLine("работа со списком слов\n");
            SetLanguage wordLingva = SetLanguage.Undefined;
            SetLanguage userLingva = SetLanguage.Undefined;
            //SetModeWrite modeWrite = SetModeWrite.Undefined;
            SetActWordsList actWordList = SetActWordsList.Undefined;

            Dictionary<SetLanguage, string> dictLingva = new Dictionary<SetLanguage, string>();
            dictLingva[SetLanguage.China] = "китайский";
            dictLingva[SetLanguage.Deutsch] = "немецкий";
            dictLingva[SetLanguage.English] = "английский";
            dictLingva[SetLanguage.Russia] = "русский";
            dictLingva[SetLanguage.Undefined] = "язык не выбран";
            do
            {
                wordLingva = (SetLanguage)
                    menuPool[SetMenu.SelectLanguage]().SelectOption("Выбор языка:");
                userLingva = wordLingva;
                if (wordLingva != SetLanguage.Undefined)
                {
                    WriteLine("\n\tВыбран язык: {0}", dictLingva[wordLingva]);
                    ListOfWords words = new ListOfWords(wordLingva, userLingva);
                    Dictionary<SetActWordsList, DJob> dictActWordList =
                        new Dictionary<SetActWordsList, DJob>();
                    dictActWordList[SetActWordsList.AddWord] =
                        new DJob(words.GetNewWord);// "добавить запись \n";
                    dictActWordList[SetActWordsList.ChangeWord] =
                        new DJob(words.ChangeWord); //"редактировать запись \n";
                    dictActWordList[SetActWordsList.RemoveWord] =
                        new DJob(words.RemoveWord); //"удалить запись \n";
                    dictActWordList[SetActWordsList.ShowWords] =
                        new DJob(words.ShowWordsList);  //"показать записи \n";
                    dictActWordList[SetActWordsList.Undefined] =
                        new DJob(words.DoNothing);  //"отказ от выбора действия \n";
                    do
                    {
                        actWordList = (SetActWordsList)
                            menuPool[SetMenu.SelectActWordsList]().
                            SelectOption("Что вы хотите сделать со списком слов?");
                        dictActWordList[actWordList]();
                    } while (actWordList != SetActWordsList.Undefined);
                }
            } while (wordLingva != SetLanguage.Undefined);
        }
    }
}
