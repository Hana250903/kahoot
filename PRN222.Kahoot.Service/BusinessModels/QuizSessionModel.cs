using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.BusinessModels
{
    public class QuizSessionModel
    {
        public int SessionId { get; set; }

        public int QuizId { get; set; }

        public int HostId { get; set; }

        public string CodeRoom { get; set; }

        public DateTime? StartTime { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? EndTime { get; set; }

        public int? TotalQuestion { get; set; }

        public int? TotalScore { get; set; }

        public string? QuizTitle { get; set; }
    }
}
