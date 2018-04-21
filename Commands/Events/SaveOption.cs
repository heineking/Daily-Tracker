using Commands.Mapping;
using DataAccessLayer.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {

  public interface ISaveOption {
    int OptionId { get; set; }

    [Mapping(nameof(Option.QuestionId))]
    int QuestionId { get; set; }

    [Mapping(nameof(Option.OptionText))]
    string Text { get; set; }

    [Mapping(nameof(Option.OptionValue))]
    int Value { get; set; }

    int SavedById { get; set; }
  }

  public abstract class SaveOption : ISaveOption {
    public int OptionId { get; set; }

    public int SavedById { get; set; }

    public int QuestionId { get; set; }

    public string Text { get; set; }

    public int Value { get; set; }
  }
}
