﻿using System.Collections.Generic;

namespace Queries.Models {
  public class QuestionModel {
    public int Id { get; set; }
    public int QuestionnaireId { get; set; }
    public string QuestionText { get; set; }
  }
}