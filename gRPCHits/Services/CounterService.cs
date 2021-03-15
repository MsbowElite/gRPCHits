using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace gRPCHits
{
    public class CounterService : CounterSvc.CounterSvcBase
    {
        private readonly ILogger<CounterService> _logger;
        private static Counter _COUNTER = new Counter();
        private static string _FRAMEWORK;
        private static string _LOCAL;

        static CounterService()
        {
            _FRAMEWORK = Assembly
                        .GetEntryAssembly()?
                        .GetCustomAttribute<TargetFrameworkAttribute>()?
                        .FrameworkName;
            _LOCAL = Environment.MachineName;
        }

        public CounterService(ILogger<CounterService> logger)
        {
            _logger = logger;
        }

        public override Task<CounterReply> GenerateValue(
            CounterRequest request, ServerCallContext context)
        {
            _COUNTER.Increment();
            int currentValue = _COUNTER.CurrentValue;

            _logger.LogInformation($"Current value: {currentValue}");

            return Task.FromResult(new CounterReply
            {
                Message = "Hello " + request.Name,
                CurrentValue = currentValue,
                LocalSvc = _LOCAL,
                TargetFramework = _FRAMEWORK
            });
        }
    }
}