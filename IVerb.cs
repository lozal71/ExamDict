using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingvaDict
{
    public enum SetTransitiveForm { Undefined, Transitive, NonTransitive}
    public enum SetConjugationType { Undefined, Weak, Strong}
    interface IVerb
    {
        SetTransitiveForm Transitive { get; set; }
        SetConjugationType ConjugationType { get; set; }
        string AuxiliaryVerb { get; set; }
    }
}
