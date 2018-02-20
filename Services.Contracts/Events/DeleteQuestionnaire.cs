using Mediator.Contracts;

namespace Services.Contracts.Events {
  public class DeleteQuestionnaire : IEvent {
    public int QuestionnaireId { get; set; }
  }
}
