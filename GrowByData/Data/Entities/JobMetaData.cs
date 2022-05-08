namespace GrowByData.Data.Entities
{
    public class JobMetaData
    {
        public Guid JobId { get; set; }
        public Type JobType { get;  }
        public string JobName { get; }
        public string CronExpression { get; }
        public JobMetaData(Guid Id, Type jobType, string jobName, string cronExpression)
        {
            JobId = Id;
            JobType = jobType;
            JobName = jobName;
            CronExpression = cronExpression;

        }
    }
}
