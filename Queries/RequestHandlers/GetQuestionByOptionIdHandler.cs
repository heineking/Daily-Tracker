using AutoMapper;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Infrastructure.Caching;
using Mediator.Contracts;
using Queries.Models;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queries.RequestHandlers
{
  [Cache(60*60, CacheType.Absolute)]
  public class GetQuestionByOptionIdHandler : IRequestHandler<GetQuestionByOptionId, QuestionModel> {
    private IRead<Question> _questionReader;
    private IMapper _mapper;

    public GetQuestionByOptionIdHandler(IRead<Question> questionReader, IMapper mapper) {
      _questionReader = questionReader;
    }

    public QuestionModel Handle(GetQuestionByOptionId request) {
      return _questionReader
        .Where(q => q.Options.Any(o => o.OptionId == request.OptionId))
        .Select(_mapper.Map<QuestionModel>)
        .Single();
    }
  }
}
