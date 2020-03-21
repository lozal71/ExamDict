namespace LingvaDict
{   
    /// <summary>
    /// Набор частей речи
    /// </summary>
    public enum SetPartOfSpeech 
    {   /// <summary>
        /// Часть речи не определена
        /// </summary>
        Undefined, 
        /// <summary>
        /// Имя существительное
        /// </summary>
        Noun, 
        /// <summary>
        /// Глагол
        /// </summary>
        Verb
    }
    /// <summary>
    /// Класс содержит базовые признаки слова в словаре
    /// </summary>
    public class BaseWord
    {
        /// <summary>
        /// Конструктор для базового слова, в котором определяются:
        /// буквенное и фонетическое написание слова, часть речи и признак удаленного слова
        /// </summary>
        public BaseWord()
        {
            WriteLetter = "Undefined";
            Pronounce = "Undefined";
            PartOfSpeech = SetPartOfSpeech.Undefined;
            DeleteMarker = false;
        }
        /// <summary>
        /// Буквенное написание слова
        /// </summary>
        public string WriteLetter { get; set; }
        /// <summary>
        /// Фонетическое написание слова
        /// </summary>
        public string Pronounce { get; set; }
        /// <summary>
        /// Часть речи
        /// </summary>
        public SetPartOfSpeech PartOfSpeech { get; set; }
        /// <summary>
        /// Признак удаленного слова
        /// </summary>
        public bool DeleteMarker {get;set;}
    }
}
