using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LingvaDict
{
    class Program
    {
        public static event dMenuOption SelectMenu; // событие выбора пункта меню
        public static event DJob SelectJob; // событие выбора работы
        static void Main(string[] args)
        {
            ModeOfJob modeOfJob = new ModeOfJob();
            SetModeJob selectedModeOfJob = SetModeJob.Exit;
            SelectMenu = null;
            SelectJob = null;
            do
            {
                SelectMenu += MenuPool.CreateMenuModeOfUsing().SelectOption;
                selectedModeOfJob = (SetModeJob)SelectMenu("Выбор режима работы пользователя:");
                SelectMenu = null;
                if (selectedModeOfJob != SetModeJob.Exit)
                {
                    SelectJob += modeOfJob.DictJob[selectedModeOfJob];
                    SelectJob();
                    SelectJob = null;
                }
            } while (selectedModeOfJob != SetModeJob.Exit);

        }
    }
}
