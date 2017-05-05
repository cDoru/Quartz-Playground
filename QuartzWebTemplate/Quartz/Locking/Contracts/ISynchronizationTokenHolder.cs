using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Locking.Contracts
{
    interface ISynchronizationTokenHolder
    {
        object GetTokenFor(TokenFor @for);
    }
}
