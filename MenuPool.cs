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
        menuSelectPartOfSpeec, menuSelectGender
    };
    public delegate Menu DCreateMenu();
    public class MenuPool
    {
        IDictionary<SetMenu, DCreateMenu> menuPool;

        public MenuPool()
        {
            menuPool = new Dictionary<SetMenu, DCreateMenu>();
            menuPool.Add(SetMenu.ModeOfUsing, new DCreateMenu(CreateMenuModeOfUsing));
            menuPool.Add(SetMenu.SelectLanguage, new DCreateMenu(CreateMenuSelectLanguage));
            menuPool.Add(SetMenu.SelectActWordsList, new DCreateMenu(CreateMenuWordsList));
            menuPool.Add(SetMenu.menuSelectPartOfSpeec, new DCreateMenu(CreateMenuPartOfSpeech));
            menuPool.Add(SetMenu.menuSelectGender, new DCreateMenu(CreateMenuSelectGender));
        }
        Menu CreateMenuModeOfUsing()
        {
            Menu menu = new Menu(4);
            menu[0] = new MenuOption("\t      Выход из приложения - цифра 0 -->");
            menu[1] = new MenuOption("\t   Работа со списком слов - цифра 1");
            menu[2] = new MenuOption("\t       Работа с переводом - цифра 2");
            menu[3] = new MenuOption("\t     Пользователь словаря - цифра 3");
            return menu;
        }
         Menu CreateMenuSelectLanguage()
        {
            Menu menu = new Menu(5);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t             Русский язык - цифра 1");
            menu[2] = new MenuOption("\t          Английский язык - цифра 2");
            menu[3] = new MenuOption("\t            Немецкий язык - цифра 3");
            menu[4] = new MenuOption("\t           Китайский язык - цифра 4");
            return menu;
        }
         Menu CreateMenuWordsList()
        {
            Menu menu = new Menu(5);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t           Добавить слово - цифра 1");
            menu[2] = new MenuOption("\t            Удалить слово - цифра 2");
            menu[3] = new MenuOption("\t      Редактировать слово - цифра 3");
            menu[4] = new MenuOption("\t           Показать слова - цифра 4");
            return menu;
        }

         Menu CreateMenuPartOfSpeech()
        {
            Menu menu = new Menu(3);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t          Существительное - цифра 1");
            menu[2] = new MenuOption("\t                   Глагол - цифра 2");
            return menu;
        }
         Menu CreateMenuSelectGender()
        {
            Menu menu = new Menu(4);
            menu[0] = new MenuOption("\tВозврат в предыдущее меню - цифра 0 -->");
            menu[1] = new MenuOption("\t                  Мужской - цифра 1");
            menu[2] = new MenuOption("\t                  Женский - цифра 2");
            menu[3] = new MenuOption("\t                  Средний - цифра 3");
            return menu;
        }
        public DCreateMenu this[SetMenu key] 
        { 
            get { return menuPool[key]; }
            set { menuPool[key] = value; } 
        }
    }
}
