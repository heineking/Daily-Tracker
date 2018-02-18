namespace DataAccessLayer.Contracts.Entities {
  public class User {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthDate { get; set; }
    public UserDirectory UserDirectory { get; set; }
  }
}