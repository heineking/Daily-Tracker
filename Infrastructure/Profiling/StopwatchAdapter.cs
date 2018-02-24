using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Profiling {
  public class StopwatchAdapter : IStopwatch {
    private readonly Stopwatch _stopwatch;

    public StopwatchAdapter(Stopwatch stopwatch) {
      _stopwatch = stopwatch;
    }

    public void Start() {
      _stopwatch.Start();
    }

    public long Stop() {
      _stopwatch.Stop();
      var elapsedMs = _stopwatch.ElapsedMilliseconds;
      _stopwatch.Reset();
      return elapsedMs;
    }
  }
}
