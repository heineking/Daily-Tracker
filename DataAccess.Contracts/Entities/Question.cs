﻿using System.Collections.Generic;

namespace DataAccessLayer.Contracts.Entities {
  public class Question {
    public int QuestionId { get; set; }
    public int QuestionnaireId { get; set; }
    public string QuestionText { get; set; }
    public ICollection<Option> Options { get; set; }
    public Questionnaire Questionnaire { get; set; }
  }
}