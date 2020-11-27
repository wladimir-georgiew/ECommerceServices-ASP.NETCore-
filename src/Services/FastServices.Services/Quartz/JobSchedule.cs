namespace FastServices.Services.Quartz
{
    using System;

    public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression)
        {
            this.JobType = jobType;
            this.CronExpression = cronExpression;
        }

        public Type JobType { get; }

        public string CronExpression { get; }
    }
}
