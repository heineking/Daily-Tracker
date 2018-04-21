using Mediator.Contracts;

namespace Commands.Events {
  public class DeleteQuestionnaire : IEvent {
    public int DeletedById { get; set; }
    public int QuestionnaireId { get; set; }
  }
}
