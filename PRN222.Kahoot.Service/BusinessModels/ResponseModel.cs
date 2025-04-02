using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.BusinessModels
{
    public class ResponseModel
    {
        public int ResponseId { get; set; }

        public int ParticipantId { get; set; }

        public int QuestionSessionId { get; set; }

        public bool IsCorrect { get; set; }

        public DateTime? AnsweredAt { get; set; }
    }
}
