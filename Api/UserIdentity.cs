using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy.Security;
using System.Security.Claims;
using System.Security.Principal;

namespace Api {
  public class UserIdentity : ClaimsPrincipal {

    public string Username { get; private set; }
    public int UserId { get;  private set; }

    public IIdentity Identity => throw new NotImplementedException();

    public UserIdentity(string username, int userId) {
      Username = username;
      UserId = userId;
    }

    public bool IsInRole(string role) {
      throw new NotImplementedException();
    }
  }
}
