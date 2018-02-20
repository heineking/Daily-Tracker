using System;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Mapper {
  public class ModelMapper : IModelMapper {

    public T ShallowClone<T>(T obj) where T : class {
      var objType = obj.GetType();
      var clone = (T)Activator.CreateInstance(objType);
      var properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
      properties.ForEach(prop => prop.SetValue(clone, prop.GetValue(obj)));

      return clone;
    }

    public TModel Merge<TModel, TEntity>(TModel model, TEntity entity) where TModel : class where TEntity : class {
      throw new NotImplementedException();
    }
  }
}
