using Microsoft.Azure.WebJobs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webjob.Template
{
    public class Functions
    {
        private readonly ILogger _logger;
        public Functions(ILogger logger)
        {
            _logger = logger;
        }
        [FunctionName("Test")]
        public async Task Run([TimerTrigger("*/10 * * * * *", RunOnStartup = true)] TimerInfo timerInfo,
           CancellationToken cancellationToken)
        {
            _logger.Information("Test is running...");
        }
     }
}
