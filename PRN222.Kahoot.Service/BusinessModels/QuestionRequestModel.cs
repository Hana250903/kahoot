using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.BusinessModels
{
    public class QuestionRequestModel
    {
        public string QuestionText { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public int Answer { get; set; }
        public int TimeLimit { get; set; }
        public int QuestionIndex { get; set; }
    }
}
