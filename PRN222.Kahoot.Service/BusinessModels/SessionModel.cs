using System;

namespace PRN222.Kahoot.Service.BusinessModels
{
    public class SessionModel
    {
        public int SessionId { get; set; }
        public string CodeRoom { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}