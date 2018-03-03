using JWT;
using Newtonsoft.Json;
using Security.Contracts.JWT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.JWT {
  public class JWTService : IJWTService {
    private readonly string _key;
    private readonly IJwtDecoder _decoder;
    private readonly IJwtEncoder _encoder;

    public JWTService(IJWTSettings jwtSettings, IJwtDecoder jwtDecoder, IJwtEncoder jwtEncoder) {
      _key = jwtSettings.Key;
      _decoder = jwtDecoder;
      _encoder = jwtEncoder;
    }

    public Token DecodeToken(string jwt) {
      var decoded = _decoder.Decode(jwt, _key, verify: true);
      var token = JsonConvert.DeserializeObject<Token>(decoded);
      if (token.Exp < DateTime.Now) {
        return null;
      }
      return token;
    }

    public string EncodeToken(Token token) {
      return _encoder.Encode(token, _key);
    }
  }
}
