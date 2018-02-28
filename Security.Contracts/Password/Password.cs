using Security.Contracts.Hashing;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Security.Contracts {

  public class Password {

    private const char Delimiter = ':';

    private IList<string> Order = new List<string> {
      nameof(Password.Algorithm),
      nameof(Password.Iterations),
      nameof(Password.Hash),
      nameof(Password.Salt)
    };

    private Dictionary<string, Func<object, string>> ToStringsMap = new Dictionary<string, Func<object, string>> {
      { nameof(Algorithm), (algo) => ((int)algo).ToString() },
      { nameof(Hash), bytes => Convert.ToBase64String((byte[])bytes) },
      { nameof(Salt), bytes => Convert.ToBase64String((byte[])bytes) },
      { nameof(Iterations), iterations => $"{iterations}" }
    };

    public IReadOnlyCollection<byte> Hash { get; }
    public IReadOnlyCollection<byte> Salt { get; }
    public int Iterations { get; }
    public HashAlgorithm Algorithm { get; }
    public int? ComputationTimeInMs { get; }

    public Password(string str) {
      var tokens = str.Split(new char[] { Delimiter }).ToList();
      var hashToken = tokens.ElementAt(GetPropertyIndex(nameof(Hash)));
      var saltToken = tokens.ElementAt(GetPropertyIndex(nameof(Salt)));
      var iterationToken = tokens.ElementAt(GetPropertyIndex(nameof(Iterations)));
      var algorithmToken = tokens.ElementAt(GetPropertyIndex(nameof(Algorithm)));

      Hash = Convert.FromBase64String(hashToken);
      Salt = Convert.FromBase64String(saltToken);
      Iterations = Int32.Parse(iterationToken);
      Algorithm = (HashAlgorithm)Enum.Parse(typeof(HashAlgorithm), algorithmToken);
      EnsurePreconditions();
    }

    public Password(byte[] hash, byte[] salt, int iterations, HashAlgorithm algorithm) {
      Hash = hash;
      Salt = salt;
      Iterations = iterations;
      Algorithm = algorithm;
      EnsurePreconditions();
    }

    public override string ToString() {
      var propertyInfos = typeof(Password).GetProperties().ToList();
      return String.Join(Delimiter, Order.Select(fieldName => {
        var propertyInfo = propertyInfos.Single(f => f.Name == fieldName);
        var propertyValue = propertyInfo.GetValue(this);
        return ToStringsMap[propertyInfo.Name](propertyValue);
      }));
    }

    public bool IsEqualTo(Password password) {
      var a = password.Hash.ToArray();
      var b = Hash.ToArray();
      return a.IsEqualToLinearCompare(b);
    }

    public bool SettingsUpdated(Password password) {
      return password.Iterations != Iterations || password.Algorithm != Algorithm || password.Hash.Count != Hash.Count || password.Salt.Count != Salt.Count;
    }

    private int GetPropertyIndex(string propertyName) {
      return Order.IndexOf(propertyName);
    }

    private void EnsurePreconditions() {
      // assert some pre-conditions
      Contract.Assert(Iterations >= 0);
      Contract.Assert(Algorithm <= Enum.GetValues(typeof(HashAlgorithm)).Cast<HashAlgorithm>().Last());
    }
  }
}
