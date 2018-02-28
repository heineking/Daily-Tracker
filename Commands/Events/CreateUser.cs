using Mediator.Contracts;
namespace Commands.Events {
  public class CreateUser : IEvent {
    public int UserId { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public void SetUserId(int id) {
      UserId = id;
    }
  }
}
