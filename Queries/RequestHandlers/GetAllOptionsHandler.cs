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

namespace Queries.RequestHandlers {
  
  public class GetAllOptionsHandler : IRequestHandler<GetAllOptions, List<OptionModel>> {
    private IRead<Option> _optionsReader;
    private IMapper _mapper;

    public GetAllOptionsHandler(IRead<Option> optionsReader, IMapper mapper) {
      _optionsReader = optionsReader;
      _mapper = mapper;
    }

    public List<OptionModel> Handle(GetAllOptions request) {
      return _optionsReader
        .GetAll()
        .Select(_mapper.Map<OptionModel>)
        .ToList();
    }
  }
}
