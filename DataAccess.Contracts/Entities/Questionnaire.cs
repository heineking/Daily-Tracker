using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Contracts.Entities {
  public class Questionnaire {
    public int QuestionnaireId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<Question> Questions { get; set; }
    public ICollection<Result> Results { get; set; }
  }
}
