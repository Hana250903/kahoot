using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.BusinessModels
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }

        public int QuizId { get; set; }

        public string QuestionText { get; set; }

        public string QuestionType { get; set; }

        public int Duration { get; set; }

        public string Question1 { get; set; }

        public string Question2 { get; set; }

        public string Question3 { get; set; }

        public string Question4 { get; set; }

        public int Answer { get; set; }
    }
}
