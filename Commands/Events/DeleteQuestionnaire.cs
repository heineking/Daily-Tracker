using Mediator.Contracts;

namespace Commands.Events {
  public class DeleteQuestionnaire : IEvent {
    public int QuestionnaireId { get; set; }
  }
}
