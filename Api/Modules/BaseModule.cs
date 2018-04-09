using Api.Auth;
using Nancy;

namespace Api.Modules {
  public class BaseModule : NancyModule {

    protected DailyTrackerPrincipal User => (DailyTrackerPrincipal)Context.CurrentUser;

    public BaseModule() : base("api/v1/") {

    }

    public BaseModule(string module) : base($"api/v1/{module.TrimStart('/')}") {

    }
  }
}
