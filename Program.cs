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
        static void Main(string[] args)
        {
            ModeOfJob modeOfJob = new ModeOfJob();
            MenuPool menuPool = new MenuPool();
            SetModeJob selectedModeOfJob = 0;
            do
            {
                selectedModeOfJob = (SetModeJob)menuPool[SetMenu.ModeOfUsing]().
                                    SelectOption("Выбор режима работы:");
                Write("\n\tВыбран режим: ");
                modeOfJob.DictJob[selectedModeOfJob]();
            } while (selectedModeOfJob != SetModeJob.Undefined);

        }
    }
}
