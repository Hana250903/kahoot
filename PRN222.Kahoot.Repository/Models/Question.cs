﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PRN222.Kahoot.Repository.Models;

public partial class Question
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

    public virtual ICollection<QuestionSession> QuestionSessions { get; set; } = new List<QuestionSession>();

    public virtual Quiz Quiz { get; set; }
}