using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Contracts.Entities {
  public class UserDirectory {
    public int UserId { get; set; }
    [Key]
    public string Username { get; set; }
    public string Password { get; set; }
    public User User { get; set; }
  }
}