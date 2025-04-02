using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.BusinessModels
{
    public class QuizModel
    {
        public int QuizId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Visibility { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateAt { get; set; }
    }
}
