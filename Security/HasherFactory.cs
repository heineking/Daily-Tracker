using Security.Contracts.Hashing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security {
  public class HasherFactory : IHasherFactory {
    private readonly IHashSettings _settings;
    public HasherFactory(IHashSettings hashSettings) {
      _settings = hashSettings;
    }
    public IHasher Create(HashAlgorithm algorithm) {
      // TODO: not really happy with this switch statement...
      // refactor?
      switch (algorithm) {
        case HashAlgorithm.PDKDF2:
          return new PDKDF2Adapter(_settings);
        default:
          throw new NotImplementedException($"Factory not implemented to handle this HashAlgorithm type = {algorithm}");
      }
    }
  }
}
