namespace LingvaDict
{
    /// <summary>
    /// набор родов имени существительного
    /// </summary>
    public enum SetGender 
    {   /// <summary>
        /// не определен
        /// </summary>
        Undefined, 
        /// <summary>
        /// мужской
        /// </summary>
        Male, 
        /// <summary>
        /// женский
        /// </summary>
        Famale, 
        /// <summary>
        /// средний
        /// </summary>
        Neuter
    }
    interface INoun
    {
        SetGender GenderNoun { get; set; }
        string PluralForm { get; set; }
    }
}
