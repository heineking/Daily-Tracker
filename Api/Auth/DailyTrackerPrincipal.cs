using System.Security.Claims;
using System.Security.Principal;

namespace Api.Auth {
  public class DailyTrackerPrincipal : ClaimsPrincipal {
    public int UserId { get; }

    public string Username { get; }

    public DailyTrackerPrincipal(int userId, string username) : base(new GenericIdentity(username)) {
      UserId = userId;
      Username = username;
    }
  }
}
