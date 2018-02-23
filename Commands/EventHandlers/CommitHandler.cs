using DataAccess.Contracts.Persistance;
using Mediator.Contracts;
using Commands.Events;

namespace Commands.EventHandlers
{
  public class CommitHandler : IEventHandler<Commit> {
    private readonly IUnitOfWork _uow;

    public CommitHandler(IUnitOfWork unitOfWork) {
      _uow = unitOfWork;
    }

    public void Handle(Commit @event) {
      _uow.Commit();
    }
  }
}
