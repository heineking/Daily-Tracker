using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Models {
  public class QuestionnaireModel {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
