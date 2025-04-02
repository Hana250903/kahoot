using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.BusinessModels
{
    public class QuestionSessionModel
    {
        public int QuestionSessionId { get; set; }

        public int QuestionId { get; set; }

        public int QuizSessionId { get; set; }

        public int QuestionIndex { get; set; }

        public int Point { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
