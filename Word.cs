using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System.Collections;

namespace LingvaDict
{
    /// <summary>
    /// Класс содержит признаки слова:
    /// для имени существительного: род и множественное число
    /// для глагола: переходность, вид спряжения, вспомогательный глагол,
    /// смысловое описание слова;
    /// наследует интерфейсы INoun, IVerb, IComparable<Word>
    /// </summary>
    [Serializable]
    public class Word : BaseWord, INoun, IVerb, IComparable<Word>
    {
        /// <summary>
        /// Конструктор, в котором определяются:
        /// для имени существительного: род и множественное число
        /// для глагола: переходность, вид спряжения, вспомогательный глагол,
        /// смысловое описание слова
        /// </summary>
        public Word() 
        {
            GenderNoun = SetGender.Undefined;
            PluralForm = "Undefined";
            Transitive = SetTransitiveForm.Undefined;
            ConjugationType = SetConjugationType.Undefined;
            AuxiliaryVerb = "Undefined";
            Description = "Undefined";
        }
        /// <summary>
        /// свойство для рода имени существительного
        /// </summary>
        public SetGender GenderNoun { get; set; }
        /// <summary>
        /// свойство для множественного числа имени существительного
        /// </summary>
        public string PluralForm { get; set; }
        /// <summary>
        /// свойство для обозначения переходности глагола
        /// </summary>
        public SetTransitiveForm Transitive  { get ;  set ; }
        /// <summary>
        /// свойство для определения вида спряжения глагола (слабое/сильное)
        /// </summary>
        public SetConjugationType ConjugationType { get; set; }
        /// <summary>
        /// свойство для определения вспомогательного глагола
        /// </summary>
        public string AuxiliaryVerb { get; set; }
        /// <summary>
        /// свойство для смыслового описания
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// вывод на консоль признаков слова
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// определение равенства двух слов
        /// </summary>
        /// <param name="obj"> Слово, с которым сравнивают </param>
        /// <returns> возвращает true, если совпадает буквенное написание и смысловое описание </returns>
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
        /// <summary>
        /// хеширование
        /// </summary>
        /// <returns></returns>
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
