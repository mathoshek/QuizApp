﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class QuizQuestionSubdomainDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DomainId { get; set; }
    }
}
