using AutoMapper;
using AutoMapper.Mappers;
using DataAccessLayer.Contracts.Entities;
using Queries.Models;
using System.Linq;

namespace Infrastructure.Mapper {
  public class DomainProfile : Profile {
    public DomainProfile() {
      // TODO: there must be a generic way to map $entityNameId == Id

      CreateMap<Questionnaire, QuestionnaireModel>()
        .ForMember(_ => _.Id, opt => opt.MapFrom(_ => _.QuestionnaireId));

      CreateMap<Question, QuestionModel>()
        .ForMember(_ => _.Id, opt => opt.MapFrom(_ => _.QuestionId))
        .ForMember(_ => _.Text, opt => opt.MapFrom(_ => _.QuestionText));

      CreateMap<Option, OptionModel>()
        .ForMember(_ => _.Id, opt => opt.MapFrom(_ => _.OptionId));

      CreateMap<QuestionOption, QuestionOptionModel>()
        .ForMember(_ => _.Id, opt => opt.MapFrom(_ => _.QuestionOptionId));

    }
  }
}
