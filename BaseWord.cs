using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingvaDict
{
    public enum SetPartOfSpeech { Undefined, Noun, Verb}
    public class BaseWord
    {
        //string writeLetter;
        //string pronounce;
        //SetPartOfSpeech partOfSpeech;

        public string WriteLetter { get; set; }
        public string Pronounce { get; set; }
        public SetPartOfSpeech PartOfSpeech { get; set; }
    }
}
