using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace LingvaDict
{
    [Serializable]
    public class Word : BaseWord, INoun, IVerb, IComparable<Word>
    {
        public Word() 
        {
            GenderNoun = SetGender.Undefined;
            PluralForm = "Undefined";
            Transitive = SetTransitiveForm.Undefined;
            ConjugationType = SetConjugationType.Undefined;
            AuxiliaryVerb = "Undefined";
            Description = "Undefined";
        }

        public SetGender GenderNoun { get; set; }
        public string PluralForm { get; set; }
        public SetTransitiveForm Transitive  { get ;  set ; }
        public SetConjugationType ConjugationType { get; set; }
        public string AuxiliaryVerb { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            switch (PartOfSpeech)
            {
                case SetPartOfSpeech.Noun:
                return $"{this.WriteLetter}, {this.PartOfSpeech}, " +
                    $"{this.GenderNoun}, {this.PluralForm}, {this.Description}," +
                    $"{this.DeleteMarker}";
                case SetPartOfSpeech.Verb:
                    return $"{this.WriteLetter}, {this.PartOfSpeech}, " +
                        $"{this.Transitive}, {this.ConjugationType}, " +
                        $"{this.AuxiliaryVerb}, {this.Description}," +
                        $"{this.DeleteMarker}";
                default:
                    return $"{this.WriteLetter}";
            }
        }

        public override bool Equals(object obj)
        {
            if (this.WriteLetter.Equals((obj as Word).WriteLetter) &&
                this.Description.Equals((obj as Word).Description))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        int IComparable<Word>.CompareTo(Word other)
        {
            if (this.WriteLetter.CompareTo(other.WriteLetter) == 0 &&
                this.Description.CompareTo(other.Description) == 0)
            {
                return 0;
            }
            else 
            {
                return this.WriteLetter.CompareTo(other.WriteLetter);
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 1881619974;
            hashCode = hashCode * -1521134295 + GenderNoun.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PluralForm);
            hashCode = hashCode * -1521134295 + Transitive.GetHashCode();
            hashCode = hashCode * -1521134295 + ConjugationType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AuxiliaryVerb);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }

    }
}
