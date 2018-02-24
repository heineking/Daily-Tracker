using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts {
  public static class ByteArrayExtensions {
    public static Tuple<byte[], byte[]> Split(this byte[] bytes, uint splitAt) {
      var totalLen = bytes.Length;
      var item1Len = splitAt;
      var item2Len = totalLen - item1Len;
      var item1 = new byte[item1Len];
      var item2 = new byte[item2Len];
      Array.Copy(bytes, 0, item1, 0, item1Len);
      Array.Copy(bytes, item1Len, item2, 0, item2Len);
      return new Tuple<byte[], byte[]>(item1, item2);
    }
    public static bool IsEqualTo(this byte[] lhs, byte[] rhs) {
      if (lhs.Length != rhs.Length)
        return false;

      for (var i = 0; i < lhs.Length; ++i) {
        if (lhs[i] != rhs[i])
          return false;
      }

      return true;
    }
    public static byte[] CombineWith(this byte[] lhs, byte[] rhs) {
      var totalLen = lhs.Length + rhs.Length;
      var combined = new byte[totalLen];
      Array.Copy(lhs, 0, combined, 0, lhs.Length);
      Array.Copy(rhs, 0, combined, lhs.Length, rhs.Length);
      return combined;
    }
  }
}
