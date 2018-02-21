using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Modules {
  public class OptionsModule : NancyModule {
    public OptionsModule() : base("/options") {

    }
  }
}
