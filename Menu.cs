using System;
using static System.Console;
using static System.Convert;

namespace LingvaDict
{ 
    public class MenuOption 
    {
        string sOption;

        public MenuOption(string sOption)
        {
            this.sOption = sOption;
        }
        public string SOption { get => sOption; set => sOption = value; }
        public override string ToString()
        {
            return $"{sOption}";
        }
    }

    public class Menu
     {
 
        MenuOption[] menuArr;
        public Menu(int n)
        {
            menuArr = new MenuOption[n];
        }
        public int Length
        {
            get { return menuArr.Length; }
        }
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
        public int SelectOption(string sMess, int k = 0)
        {
            int replay = -1;
            WriteLine(sMess, k + 1);
            do
            {
                try
                {
                    for (int i = 1; i < menuArr.Length; i++)
                    {
                        WriteLine(menuArr[i]);
                    }
                    Write(menuArr[0]);
                    replay = ToInt32(ReadLine());
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
