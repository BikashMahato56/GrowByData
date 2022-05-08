using GrowByData.Services;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace GrowByData.Job
{
    [DisallowConcurrentExecution]
    public class FetchingProductFromApi : IJob
    {
        private readonly IGrowByDataService _service;
        private readonly IHubContext<JobsHub> _hubContext;

        public FetchingProductFromApi(IGrowByDataService service, IHubContext<JobsHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _service.RecordApiDataToDatabase();
            return Task.CompletedTask;
        }
    }
}
