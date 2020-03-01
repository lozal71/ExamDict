using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingvaDict
{
    enum SetPartOfSpeech { Undefined, Noun, Verb}
    public class BaseWord
    {
        SetLanguage wordLanuage;
        SetLanguage userLanguge;
        SetModeWrite modeWrite;
        string writeLetter;
        string pronounce;
        SetPartOfSpeech partOfSpeech;

        public BaseWord(SetLanguage wordLanuage, SetLanguage userLanguge, SetModeWrite modeWrite)
        {
            this.wordLanuage = wordLanuage;
            this.userLanguge = userLanguge;
            this.modeWrite = modeWrite;
        }

        public string WriteLetter { get => writeLetter; set => writeLetter = value; }
        public string Pronounce { get => pronounce; set => pronounce = value; }
        internal SetPartOfSpeech PartOfSpeech { get => partOfSpeech; set => partOfSpeech = value; }
    }
}
