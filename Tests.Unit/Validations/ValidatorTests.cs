using Commands.Events;
using Commands.Validators;
using Mediator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Unit.Validations {
  [TestClass]
  public class ValidatorTests {
    public void FooBar() {
      var container = new Container(cfg => {
        cfg.For<UniqueUsernameRule>().Use<UniqueUsernameRule>();
        cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
        cfg.For<CreateUserValidator>().Use<CreateUserValidator>();
      });
      var validator = container.GetInstance<CreateUserValidator>();
      var errors = validator.Validate(new CreateUser { Username = "ebheineking" }).ToList();
    }
  }
}
