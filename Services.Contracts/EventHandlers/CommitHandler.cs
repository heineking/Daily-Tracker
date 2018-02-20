using DataAccess.Contracts.Persistance;
using Mediator.Contracts;
using Services.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts.EventHandlers
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
