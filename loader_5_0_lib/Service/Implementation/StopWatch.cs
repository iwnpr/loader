using loader_5_0_lib.Service.Interfaces;
using System.Diagnostics;

namespace loader_5_0_lib.Service.Implementation
{
    public class StopWatch : IStopWatch
    {
        public Stopwatch Stopwatch => new();
    }
}
