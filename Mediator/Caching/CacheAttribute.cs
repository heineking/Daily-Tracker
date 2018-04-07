using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator.Caching {
  public enum CacheType {
    Absolute
  }

  public class CacheAttribute : Attribute {
    public uint Seconds { get; }
    public CacheType CacheType { get; }
    public CacheAttribute(uint seconds, CacheType cacheType) {
      Seconds = seconds;
      CacheType = CacheType;
    }
  }
}
