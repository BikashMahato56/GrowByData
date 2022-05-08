using GrowByData.Data.Entities;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace GrowByData.Scheduler
{
    public class TaskScheduler : IHostedService
    {
        public IScheduler Scheduler { get; set; }
        private readonly IJobFactory _jobFactory;
        private readonly JobMetaData _jobMetaData;
        private readonly ISchedulerFactory _schedularFactory;
        public TaskScheduler(ISchedulerFactory schedularFactory, IJobFactory jobFactory, JobMetaData jobMetaData)
        {
            _jobFactory = jobFactory;
            _jobMetaData = jobMetaData;
            _schedularFactory = schedularFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //Create Schedular
            Scheduler = await _schedularFactory.GetScheduler();
            Scheduler.JobFactory = _jobFactory;
            //Create job
            IJobDetail jobDetail = CreateJob(_jobMetaData);
            //Create trigger
            ITrigger trigger = CreateTrigger(_jobMetaData);
            //Schedule job
             await Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
            //Start Schedular
            await Scheduler.Start(cancellationToken);
        }

        private ITrigger CreateTrigger(JobMetaData jobMetaData)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobMetaData.JobId.ToString())
                .WithCronSchedule(jobMetaData.CronExpression)
                .WithDescription(jobMetaData.JobName)
                .Build();   
        }

        private IJobDetail CreateJob(JobMetaData jobMetaData)
        {
            return JobBuilder.Create(jobMetaData.JobType)
                .WithIdentity(jobMetaData.JobId.ToString())
                .WithDescription(jobMetaData.JobName)
                .Build();   
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler.Shutdown();
        }
    }
}
