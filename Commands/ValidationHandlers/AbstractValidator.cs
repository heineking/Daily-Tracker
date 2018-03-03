using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Commands.ValidationHandlers
{
  public abstract class AbstractValidator<TEntity> where TEntity : class {
    public List<PropertyRule<TEntity>> PropertyRules { get; protected set; }

    public PropertyRule<TEntity> RuleFor<TProperty>(Expression<Func<TEntity, TProperty>> exp) {
      var rule = PropertyRule<TEntity>.Create(exp);
      PropertyRules.Add(rule);
      return rule;
    }

    public IEnumerable<string> Validate(TEntity entity) {
      return PropertyRules
        .Select(propertyRules => propertyRules.Validate(entity))
        .Where(result => !result.Item1)
        .Select(result => result.Item2);
    }

    public AbstractValidator() {
      PropertyRules = new List<PropertyRule<TEntity>>();
    }
  }
}
