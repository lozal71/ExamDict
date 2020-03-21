using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingvaDict
{
    public enum SetMenu 
    { 
        Undefined, ModeOfUsing, SelectLanguage, SelectActWordsList,
        SelectPartOfSpeech, SelectGender, SelectTransitive, SelectСonjugationType,
        ContinueStop
    };
    //public delegate Menu DCreateMenu();

    public delegate int dMenuOption(string str, int k = 0); // делегат вызова выбора опции меню

    public class MenuPool
    {
        //Dictionary<SetMenu, DCreateMenu> menuPool;
        //public MenuPool()
        //{
        //    menuPool = new Dictionary<SetMenu, DCreateMenu>();
        //    menuPool.Add(SetMenu.ModeOfUsing, new DCreateMenu(CreateMenuModeOfUsing));
        //    menuPool.Add(SetMenu.SelectLanguage, new DCreateMenu(CreateMenuSelectLanguage));
        //    menuPool.Add(SetMenu.SelectActWordsList, new DCreateMenu(CreateMenuWordsList));
        //    menuPool.Add(SetMenu.SelectPartOfSpeech, new DCreateMenu(CreateMenuPartOfSpeech));
        //    menuPool.Add(SetMenu.SelectGender, new DCreateMenu(CreateMenuSelectGender));
        //    menuPool.Add(SetMenu.SelectTransitive, new DCreateMenu(CreateMenuSelectTransitive));
        //    menuPool.Add(SetMenu.SelectСonjugationType, new DCreateMenu(CreateMenuSelectСonjugationType));
        //    menuPool.Add(SetMenu.ContinueStop, new DCreateMenu(CreateMenuContinueStop));
        //}

        public static Menu CreateMenuContinueStop()
        {
            Menu menu = new Menu(2);
            menu[0] = new MenuOption("\t     Возврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\tПродолжение работы со словарем - цифра 1");
            return menu;

        }
        public static Menu CreateMenuModeOfUsing()
        {
            Menu menu = new Menu(4);
            menu[0] = new MenuOption("\t      Выход из приложения - цифра 0 -->");
            menu[1] = new MenuOption("\t   Работа со списком слов - цифра 1");
            menu[2] = new MenuOption("\t       Работа с переводом - цифра 2");
            menu[3] = new MenuOption("\t     Пользователь словаря - цифра 3");
            return menu;
        }
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
        public static Menu CreateMenuPartOfSpeech()
        {
            Menu menu = new Menu(3);
            menu[0] = new MenuOption("\t   Неопределено - цифра 0 -->");
            menu[1] = new MenuOption("\tСуществительное - цифра 1");
            menu[2] = new MenuOption("\t         Глагол - цифра 2");
            return menu;
        }
        public static Menu CreateMenuSelectGender()
        {
            Menu menu = new Menu(4);
            menu[0] = new MenuOption("\tНеопределено - цифра 0 -->");
            menu[1] = new MenuOption("\t     Мужской - цифра 1");
            menu[2] = new MenuOption("\t     Женский - цифра 2");
            menu[3] = new MenuOption("\t     Средний - цифра 3");
            return menu;
        }
        public static Menu CreateMenuSelectTransitive()
        {
            Menu menu = new Menu(3);
            menu[0] = new MenuOption("\tНеопределено - цифра 0 -->");
            menu[1] = new MenuOption("\t  Переходный - цифра 1");
            menu[2] = new MenuOption("\tНепереходный - цифра 2");
            return menu;
        }
        public static Menu CreateMenuSelectСonjugationType()
        {
            Menu menu = new Menu(3);
            menu[0] = new MenuOption("\tНеопределено - цифра 0 -->");
            menu[1] = new MenuOption("\t      Слабое - цифра 1");
            menu[2] = new MenuOption("\t     Сильное - цифра 2");
            return menu;
        }
        //public DCreateMenu this[SetMenu key] 
        //{ 
        //    get { return menuPool[key]; }
        //    set { menuPool[key] = value; } 
        //}
    }
}
