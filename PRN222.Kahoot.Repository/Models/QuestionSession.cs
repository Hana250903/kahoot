﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PRN222.Kahoot.Repository.Models;

public partial class QuestionSession
{
    public int QuestionSessionId { get; set; }

    public int QuestionId { get; set; }

    public int QuizSessionId { get; set; }

    public int QuestionIndex { get; set; }

    public int Point { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual Question Question { get; set; }

    public virtual QuizSession QuizSession { get; set; }

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();
}