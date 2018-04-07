using AutoMapper;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Caching;
using Mediator.Contracts;
using Queries.Models;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queries.RequestHandlers
{
  [Cache(30, CacheType.Absolute)]
  public class GetAllQuestionsHandler : IRequestHandler<GetAllQuestions, List<QuestionModel>> {
    private readonly IRead<Question> _questionReader;
    private readonly IMapper _mapper;
    public GetAllQuestionsHandler(IRead<Question> questionReader, IMapper mapper) {
      _questionReader = questionReader;
      _mapper = mapper;
    }
    public List<QuestionModel> Handle(GetAllQuestions request) {
      return _questionReader
        .GetAll()
        .Select(_mapper.Map<QuestionModel>)
        .ToList();
    }
  }
}
