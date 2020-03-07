using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingvaDict
{
    public class Word : BaseWord, INoun, IVerb, IComparable<Word>
    {
        SetGender gender;
        string pluralForm;
        string description;
        public Word() 
        {
            Pronounce = "";
        }

        public SetGender GenderNoun 
        {
            get => gender;
            set => gender = value;
        }
        public string PluralForm 
        { get => pluralForm; set => pluralForm = value; }
        public SetTransitiveForm Transitive  { get ;  set ; }
        public SetConjugationType ConjugationType { get; set; }
        public string AuxiliaryVerb { get; set; }
        public string Description { get => description; set => description = value; }

        public override string ToString()
        {
            switch (PartOfSpeech)
            {
                case SetPartOfSpeech.Noun:
                return $"{this.WriteLetter}, {this.PartOfSpeech}, " +
                    $"{this.gender}, {this.pluralForm}, {this.description}";
                case SetPartOfSpeech.Verb:
                    return $"{this.WriteLetter}, {this.PartOfSpeech}, " +
                        $"{this.Transitive}, {this.ConjugationType}, " +
                        $"{this.AuxiliaryVerb}, {this.description}";
                default:
                    return $"{this.WriteLetter}";
            }
        }

        public override bool Equals(object obj)
        {
            if (this.WriteLetter.Equals((obj as Word).WriteLetter) &&
                this.description.Equals((obj as Word).description))
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
            return (this.WriteLetter.CompareTo(other.WriteLetter) +
                this.description.CompareTo(other.description));
        }

        public override int GetHashCode()
        {
            var hashCode = 318132550;
            hashCode = hashCode * -1521134295 + gender.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(pluralForm);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
            hashCode = hashCode * -1521134295 + GenderNoun.GetHashCode();
            return hashCode;
        }
    }
}
