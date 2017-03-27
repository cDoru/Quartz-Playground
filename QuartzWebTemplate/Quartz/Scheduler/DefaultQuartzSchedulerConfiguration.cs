using System.Collections.Specialized;
using QuartzWebTemplate.Configuration;

namespace QuartzWebTemplate.Quartz.Scheduler
{
    public class DefaultQuartzSchedulerConfiguration : IQuartzConfigurationProvider
    {
        private readonly IConfigurationProvider _configurationProvider;
        // Misfire : see http://nurkiewicz.blogspot.com/2012/04/quartz-scheduler-misfire-instructions.html
        private const int ThreadCount = 10;
        private const string ThreadPriority = "Normal";
        private const string InstanceId = "AUTO";
        private const string DataSource = "default";
        private const string Provider = "SqlServer-20";

        private const string InstanceNameKey = "quartz.scheduler.instanceName";
        private const string InstanceIdKey = "quartz.scheduler.instanceId";
        private const string ThreadPoolTypeKey = "quartz.threadPool.type";
        private const string ThreadPoolThreadCountKey = "quartz.threadPool.threadCount";
        private const string ThreadPoolThreadPriorityKey = "quartz.threadPool.threadPriority";
        private const string JobStoreMisforeThresholdKey = "quartz.jobStore.misfireThreshold";
        private const string JobStoreTypeKey = "quartz.jobStore.type";
        private const string ClusteredKey = "quartz.jobStore.clustered";

        private const string JobStoreDataSourceKey = "quartz.jobStore.dataSource";
        private const string JobStoreTablePrefixKey = "quartz.jobStore.tablePrefix";
        private const string LockHandlerTypeKey = "quartz.jobStore.lockHandler.type";
        private const string JobStoreUsePropertiesKey = "quartz.jobStore.useProperties";
        private const string QuartzConnectionString = "quartz.dataSource.default.connectionString";
        private const string QuartzDefaultProvider = "quartz.dataSource.default.provider";

        private const string JobSuccessMessageKey = "quartz.plugin.triggHistory.jobSuccessMessage";
        private const string JobSuccessMessageValue = "*** Job {1}.{0} execution completed (with no errors) at {2:HH:mm:ss MM/dd/yyyy} and reports: {8}";

        private const string JobFailureMessageKey = "quartz.plugin.triggHistory.jobFailedMessage";
        private const string JobFailureMessageValue = "quartz.plugin.triggHistory.jobFailedMessage = *** Job {1}.{0} execution failed at {2:HH:mm:ss MM/dd/yyyy} and reports: {8}";

        private const string HistoryPluginTypeKey = "quartz.plugin.triggHistory.type";
        private const string HistoryPluginTypeValue = "Quartz.Plugin.History.LoggingJobHistoryPlugin";

        public DefaultQuartzSchedulerConfiguration(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public NameValueCollection GetConfiguration()
        {
            var connectionString = _configurationProvider.Get(ConfigurationKeys.QuartzSqlConnection);
            var instanceName = _configurationProvider.Get(ConfigurationKeys.InstanceName);

            var collection = new NameValueCollection
            {
                {InstanceNameKey, instanceName},
                {InstanceIdKey, InstanceId},
                {ThreadPoolTypeKey, "Quartz.Simpl.SimpleThreadPool, Quartz"},
                {ThreadPoolThreadCountKey, ThreadCount.ToString()},
                {ThreadPoolThreadPriorityKey, ThreadPriority},
                {JobStoreMisforeThresholdKey, "60000"},
                {JobStoreTypeKey, "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz"},
                {JobStoreUsePropertiesKey, "true"},
                {ClusteredKey, "true"},
                {JobStoreDataSourceKey, DataSource},
                {JobStoreTablePrefixKey, "QRTZ_"},
                {LockHandlerTypeKey, "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz"},
                {QuartzConnectionString, connectionString},
                {QuartzDefaultProvider, Provider},
                {JobSuccessMessageKey, JobSuccessMessageValue},
                {JobFailureMessageKey, JobFailureMessageValue},
                {HistoryPluginTypeKey, HistoryPluginTypeValue}
            };

            return collection;
        }
    }
}