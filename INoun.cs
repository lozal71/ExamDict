using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingvaDict
{
    public enum SetGender { Undefined, Male, Famale, Neuter}
    interface INoun
    {
        SetGender GenderNoun { get; set; }
        string PluralForm { get; set; }
    }
}
