using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingvaDict
{
    /// <summary>
    /// Набор меню
    /// </summary>
    public enum SetMenu 
    {   /// <summary>
        ///  Меню не определено
        /// </summary>
        Undefined,
        /// <summary>
        /// Режим работы пользователя
        /// </summary>
        ModeOfUsing, 
        /// <summary>
        /// Выбор языка
        /// </summary>
        SelectLanguage, 
        /// <summary>
        /// Выбор действий для работы со списком слов
        /// </summary>
        SelectActWordsList,
        /// <summary>
        /// Выбор части речи
        /// </summary>
        SelectPartOfSpeech, 
        /// <summary>
        /// Выбор рода существительного
        /// </summary>
        SelectGender, 
        /// <summary>
        /// Выбор переходности глагола
        /// </summary>
        SelectTransitive, 
        /// <summary>
        /// Выбор вида спряжения глагола
        /// </summary>
        SelectСonjugationType,
        /// <summary>
        /// Выбор действия - продолжить или остановиться
        /// </summary>
        ContinueStop
    };
    /// <summary>
    /// делегат вызова выбора опции меню
    /// </summary>
    /// <param name="str"> заголовок меню </param>
    /// <returns> возвращает целое число - выбор пользователя </returns>
    public delegate int dMenuOption(string str); 
    /// <summary>
    /// Класс содержит статические методы формирования различных меню
    /// </summary>
    public class MenuPool
    {
        /// <summary>
        /// Создание меню: продолжить-остановиться
        /// </summary>
        /// <returns> возвращает объект класса Menu </returns>
        public static Menu CreateMenuContinueStop()
        {
            Menu menu = new Menu(2);
            menu[0] = new MenuOption("\t     Возврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\tПродолжение работы со словарем - цифра 1");
            return menu;

        }
        /// <summary>
        /// Создание меню для выбора режима работы пользователя
        /// </summary>
        /// <returns> возвращает объект класса Menu </returns>
        public static Menu CreateMenuModeOfUsing()
        {
            Menu menu = new Menu(4);
            menu[0] = new MenuOption("\t      Выход из приложения - цифра 0 -->");
            menu[1] = new MenuOption("\t   Работа со списком слов - цифра 1");
            menu[2] = new MenuOption("\t       Работа с переводом - цифра 2");
            menu[3] = new MenuOption("\t     Пользователь словаря - цифра 3");
            return menu;
        }
        /// <summary>
        /// Создание меню для выбора языка
        /// </summary>
        /// <returns> возвращает объект класса Menu </returns>
        public static Menu CreateMenuSelectLanguage()
        {
            Menu menu = new Menu(5);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t             Русский язык - цифра 1");
            menu[2] = new MenuOption("\t          Английский язык - цифра 2");
            menu[3] = new MenuOption("\t            Немецкий язык - цифра 3");
            menu[4] = new MenuOption("\t           Китайский язык - цифра 4");
            return menu;
        }
        /// <summary>
        /// Создание меню для выбора действий работы со списком слов
        /// </summary>
        /// <returns> возвращает объект класса Menu </returns>
        public static Menu CreateMenuWordsList()
        {
            Menu menu = new Menu(5);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t           Добавить слово - цифра 1");
            menu[2] = new MenuOption("\t            Удалить слово - цифра 2");
            menu[3] = new MenuOption("\t      Редактировать слово - цифра 3");
            menu[4] = new MenuOption("\t           Показать слова - цифра 4");
            return menu;
        }
        /// <summary>
        /// Создание меню для выбора части речи
        /// </summary>
        /// <returns> возвращает объект класса Menu </returns>
        public static Menu CreateMenuPartOfSpeech()
        {
            Menu menu = new Menu(3);
            menu[0] = new MenuOption("\t   Неопределено - цифра 0 -->");
            menu[1] = new MenuOption("\tСуществительное - цифра 1");
            menu[2] = new MenuOption("\t         Глагол - цифра 2");
            return menu;
        }
        /// <summary>
        /// Создание меню для выбора рода имени существительного
        /// </summary>
        /// <returns> возвращает объект класса Menu </returns>
        public static Menu CreateMenuSelectGender()
        {
            Menu menu = new Menu(4);
            menu[0] = new MenuOption("\tНеопределено - цифра 0 -->");
            menu[1] = new MenuOption("\t     Мужской - цифра 1");
            menu[2] = new MenuOption("\t     Женский - цифра 2");
            menu[3] = new MenuOption("\t     Средний - цифра 3");
            return menu;
        }
        /// <summary>
        /// Создание меню для выбора переходности глагола
        /// </summary>
        /// <returns> возвращает объект класса Menu </returns>
        public static Menu CreateMenuSelectTransitive()
        {
            Menu menu = new Menu(3);
            menu[0] = new MenuOption("\tНеопределено - цифра 0 -->");
            menu[1] = new MenuOption("\t  Переходный - цифра 1");
            menu[2] = new MenuOption("\tНепереходный - цифра 2");
            return menu;
        }
        /// <summary>
        /// Создание меню для выбора вида спряжения глагола
        /// </summary>
        /// <returns> возвращает объект класса Menu </returns>
        public static Menu CreateMenuSelectСonjugationType()
        {
            Menu menu = new Menu(3);
            menu[0] = new MenuOption("\tНеопределено - цифра 0 -->");
            menu[1] = new MenuOption("\t      Слабое - цифра 1");
            menu[2] = new MenuOption("\t     Сильное - цифра 2");
            return menu;
        }
    }
}
