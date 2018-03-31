using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Commands.ValidationHandlers {
  public class PropertyRule<TEntity> where TEntity : class {
    private List<IRule<TEntity>> Rules;

    private Func<TEntity, object> PropertySelector;

    private PropertyRule(Func<TEntity, object> propertySelector) {
      PropertySelector = propertySelector;
      Rules = new List<IRule<TEntity>>();
    }

    public static PropertyRule<T> Create<T, TProperty>(Expression<Func<T, TProperty>> exp) where T : class {
      Func<T, object> propertySelector = x => exp.Compile()(x);
      return new PropertyRule<T>(propertySelector);
    }

    public Tuple<bool, string> Validate(TEntity entity) {
      var message = Rules.FirstOrDefault(rule => !rule.IsValid(entity))?.Message;
      return new Tuple<bool, string>(string.IsNullOrEmpty(message), message);
    }

    public PropertyRule<TEntity> NotNull(string message) {
      var rule = new Rule<TEntity> {
        Message = message,
        IsValid = entity => PropertySelector(entity) != null
      };
      Rules.Add(rule);
      return this;
    }

    public PropertyRule<TEntity> NotEmpty(string message) {
      var rule = new Rule<TEntity> {
        Message = message,
        IsValid = entity => (string)PropertySelector(entity) != String.Empty
      };
      Rules.Add(rule);
      return this;
    }

    public PropertyRule<TEntity> MaxLength(int maxLen, string message) {
      var rule = new Rule<TEntity> {
        Message = message,
        IsValid = entity => DoWhenStringDefined(PropertySelector(entity), str => {
          if (str.Length > maxLen) {
            return false;
          }
          return true;
        })
      };
      Rules.Add(rule);
      return this;
    }

    public PropertyRule<TEntity> MinDate(DateTime minDate, string message) {
      var rule = new Rule<TEntity> {
        Message = message,
        IsValid = entity => DoWhenNotNull<object>(PropertySelector(entity), obj => {
          if (obj.GetType() == typeof(string)) {
            DateTime.TryParse(obj.ToString(), out DateTime asDateTime);
            return asDateTime <= minDate;
          }
          return (DateTime)obj <= minDate;
        })
      };
      Rules.Add(rule);
      return this;
    }

    public PropertyRule<TEntity> MatchesPattern(string pattern, string message) {
      var rule = new Rule<TEntity> {
        Message = message,
        IsValid = entity => DoWhenStringDefined(PropertySelector(entity), str => {
          var regex = new Regex(pattern);
          return regex.IsMatch(str);
        })
      };
      Rules.Add(rule);
      return this;
    }

    public PropertyRule<TEntity> Required(string message) {
      var rule = new Rule<TEntity> {
        Message = message,
        IsValid = entity => {
          var propertyValue = PropertySelector(entity);
          return !(propertyValue == null || propertyValue == Activator.CreateInstance(propertyValue.GetType()));
        }
      };
      Rules.Add(rule);
      return this;
    }

    public PropertyRule<TEntity> MustBe<TRule>(TRule rule) where TRule : IRule<TEntity> {
      Rules.Add(rule);
      return this;
    }

    public PropertyRule<TEntity> MustBe(Func<object, bool> predicate, string message) {
      var rule = new Rule<TEntity> {
        Message = message,
        IsValid = entity => predicate(PropertySelector(entity))
      };
      Rules.Add(rule);
      return this;
    }

    private bool DoWhenNotNull<T>(object maybeNull, Func<T, bool> action) {
      if (maybeNull != null)
        return action((T)maybeNull);
      return false;
    }

    private bool DoWhenStringDefined(object maybeString, Func<string, bool> action) {
      var str = (string)maybeString;
      if (!string.IsNullOrEmpty(str))
        return action(str);
      return true;
    }
  }
}
