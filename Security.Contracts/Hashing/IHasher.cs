namespace Security.Contracts.Hashing {
  public interface IHasher {
    Password CreatePassword(string text, byte[] salt, int hashBytes, int iterations);
    Password CreatePassword(string text);
  }
}
