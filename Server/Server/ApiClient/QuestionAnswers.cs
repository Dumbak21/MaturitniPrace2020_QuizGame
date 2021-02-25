using System;
using System.Collections.Generic;
using System.Text;

namespace Server.ApiClient
{
    class QuestionAnswers
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public int Area { get; set; }
        public string Question { get; set; }
        public string Answers { get; set; }
        public string Answer { get; set; }
    }
}

