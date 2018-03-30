using Mediator.Contracts;

namespace Commands.Events {
  public class DeleteQuestionnaire : IEvent {
    public int DeletedByUserId { get; set; }
    public int QuestionnaireId { get; set; }
  }
}
