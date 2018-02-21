﻿using AutoMapper;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Models;
using Queries.Requests;
using System.Collections.Generic;
using System.Linq;

namespace Queries.RequestHandlers {
  public class GetAllQuestionnairesHandler : IRequestHandler<GetAllQuestionnaires, List<QuestionnaireModel>> {
    private readonly IRead<Questionnaire> _questionnaireReader;
    private readonly IRead<Question> _questionReader;
    private readonly IMapper _mapper;

    public GetAllQuestionnairesHandler(IRead<Questionnaire> questionnaireReader, IRead<Question> questionReader, IMapper mapper) {
      _questionnaireReader = questionnaireReader;
      _questionReader = questionReader;
      _mapper = mapper;
    }

    public List<QuestionnaireModel> Handle(GetAllQuestionnaires request) {
      var questionnaire = _questionnaireReader
        .GetAll()
        .ToList();

      return questionnaire
            .Select(_mapper.Map<QuestionnaireModel>)
            .ToList();
    }
  }
}
