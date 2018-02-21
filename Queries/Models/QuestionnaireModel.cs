using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Models {
  public class QuestionnaireModel {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<QuestionModel> Questions { get; set; }
  }
}
