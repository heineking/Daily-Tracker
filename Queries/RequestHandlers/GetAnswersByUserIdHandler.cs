using AutoMapper;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Models;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queries.RequestHandlers {
  public class GetAnswersByUserIdHandler : IRequestHandler<GetAnswersByUserId, List<AnswerModel>> {
    public IRead<Answer> _answerReader;
    public IMapper _mapper;

    public GetAnswersByUserIdHandler(IRead<Answer> answerReader, IMapper mapper) {
      _answerReader = answerReader;
      _mapper = mapper;
    }

    public List<AnswerModel> Handle(GetAnswersByUserId request) {
      return _answerReader
          .Where(a => a.UserId == request.UserId)
          .Select(_mapper.Map<AnswerModel>)
          .ToList();
    }
  }
}
