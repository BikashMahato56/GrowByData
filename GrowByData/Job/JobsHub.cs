using Microsoft.AspNetCore.SignalR;

namespace GrowByData.Job
{
    public class JobsHub: Hub
    {
        public Task SendConcurrentJobsMessage(string message)
        {
            return Clients.All.SendAsync("ConcurrentJobs", message);
        }

        public Task SendNonConcurrentJobsMessage(string message)
        {
            return Clients.All.SendAsync("NonConcurrentJobs", message);
        }

    }
}
