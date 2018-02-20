using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Mapper {
  public interface IModelMapper {
    T ShallowClone<T>(T obj) where T : class;
  }
}
