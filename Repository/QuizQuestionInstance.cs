//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuizQuestionInstance
    {
        public int Id { get; set; }
        public int QuizQuestionId { get; set; }
        public bool AnswerSaved { get; set; }
        public bool Choice1 { get; set; }
        public bool Choice2 { get; set; }
        public bool Choice3 { get; set; }
    
        public virtual QuizQuestion QuizQuestion { get; set; }
    }
}
