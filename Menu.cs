using System;
using static System.Console;
using static System.Convert;

namespace LingvaDict
{   /// <summary>
    ///  Класс для формирования и вывода на консоль опций меню
    /// </summary>
    public class MenuOption 
    {
        string sOption;
        /// <summary>
        /// Конструктор - определяет строку-опцию меню
        /// </summary>
        /// <param name="sOption"> Текстовое написание опции </param>
        public MenuOption(string sOption)
        {
            this.sOption = sOption;
        }
        /// <summary>
        /// строка-опция меню
        /// </summary>
        public string SOption { get => sOption; set => sOption = value; }
        /// <summary>
        /// вывод на консоль строки-опции меню
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{sOption}";
        }
    }
    /// <summary>
    /// Класс <c>Menu</c> содержит методы для формирования меню
    /// </summary>
    public class Menu
     {
        MenuOption[] menuArr;
        /// <summary>
        /// Конструктор, в котором объявляется массив опций меню 
        /// </summary>
        /// <param name="n"></param>
        public Menu(int n)
        {
            menuArr = new MenuOption[n];
        }
        /// <summary>
        /// возвращает количество опций меню
        /// </summary>
        public int Length
        {
            get { return menuArr.Length; }
        }
        /// <summary>
        /// индексатор меню по номеру опции
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MenuOption this [int index]
        {
            get
            {
                if (index >= 0 && index < menuArr.Length)
                {
                    return menuArr[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                menuArr[index] = value;
            }
        }
        /// <summary>
        /// Метод для выбора опции меню, с проверкой правильности ввода
        /// </summary>
        /// <param name="sMess">
        /// Используется для вывода заголовка меню
        /// </param>
        /// <returns>Возврат - ответ пользователя - целое число</returns>
        public int SelectOption(string sMess)
        {
            int replay = -1;
            WriteLine(sMess); // вывод заголовка меню
            do
            {
                try
                {
                    // вывод на всех опций меню из массива
                    for (int i = 1; i < menuArr.Length; i++)
                    {
                        WriteLine(menuArr[i]);
                    }
                    Write(menuArr[0]);
                    // запрос ответа пользователя
                    replay = ToInt32(ReadLine());
                    // если ответ некорректный - генерация исключения и запрос правильного ответа
                    if (replay < 0 || replay > menuArr.Length)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception)
                {
                    WriteLine("Некорректный ответ. Попробуйте еще раз...");
                }
            } while (replay < 0 || replay > menuArr.Length);
            return replay;
        }
    }
}
