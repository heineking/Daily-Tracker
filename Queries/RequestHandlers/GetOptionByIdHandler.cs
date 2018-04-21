using AutoMapper;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Infrastructure.Caching;
using Mediator.Contracts;
using Queries.Models;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.RequestHandlers {

  [Cache(60*60, CacheType.Absolute)]
  public class GetOptionByIdHandler : IRequestHandler<GetOptionById, OptionModel> {
    private readonly IRead<Option> _optionReader;
    private readonly IMapper _mapper;

    public GetOptionByIdHandler(IRead<Option> optionReader, IMapper mapper) {
      _optionReader = optionReader;
      _mapper = mapper;
    }

    public OptionModel Handle(GetOptionById request) {
      var option = _optionReader.GetById(request.OptionId);
      return _mapper.Map<OptionModel>(option);
    }
  }
}
