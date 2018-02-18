using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Contracts.Entities {
  public class UserDirectory {
    [Key]
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public User User { get; set; }
  }
}