using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.BusinessModels
{
    public class ParticipantModel
    {
        public int ParticipantId { get; set; }

        public int SessionId { get; set; }

        public int UserId { get; set; }

        public string Team { get; set; }

        public int? Score { get; set; }

        public DateTime JoinAt { get; set; }
    }
}
