using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Profiling {
  public interface IStopwatch {
    void Start();
    long Stop();
  }
}
