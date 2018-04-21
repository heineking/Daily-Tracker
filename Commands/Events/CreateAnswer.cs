using Mediator.Contracts;

namespace Commands.Events {
  public class CreateAnswer : IEvent {
    public int AnswerId { get; set; }
    public int OptionId { get; set; }
    public int UserId { get; set; }
  }
}
