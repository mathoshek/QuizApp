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
    
    public partial class QuizInstance
    {
        public QuizInstance()
        {
            this.QuizQuestionInstances = new HashSet<QuizQuestionInstance>();
            this.User_QuizInstance = new HashSet<User_QuizInstance>();
        }
    
        public int Id { get; set; }
        public int QuizId { get; set; }
        public bool IsStarted { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
    
        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<QuizQuestionInstance> QuizQuestionInstances { get; set; }
        public virtual ICollection<User_QuizInstance> User_QuizInstance { get; set; }
    }
}
