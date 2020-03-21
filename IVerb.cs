namespace LingvaDict
{
    /// <summary>
    /// набор переходность/непереходность глагола
    /// </summary>
    public enum SetTransitiveForm 
    { 
        /// <summary>
        /// переходность не определена
        /// </summary>
        Undefined, 
        /// <summary>
        /// глагол переходный
        /// </summary>
        Transitive, 
        /// <summary>
        /// глагол непереходный
        /// </summary>
        NonTransitive
    }
    /// <summary>
    /// набор для видов спряжения глагола
    /// </summary>
    public enum SetConjugationType 
    {   
        /// <summary>
        /// вид спряжения не определен
        /// </summary>
        Undefined, 
        /// <summary>
        /// спряжение слабое
        /// </summary>
        Weak, 
        /// <summary>
        /// спряжение сильное
        /// </summary>
        Strong
    }
    interface IVerb
    {
        SetTransitiveForm Transitive { get; set; }
        SetConjugationType ConjugationType { get; set; }
        string AuxiliaryVerb { get; set; }
    }
}
