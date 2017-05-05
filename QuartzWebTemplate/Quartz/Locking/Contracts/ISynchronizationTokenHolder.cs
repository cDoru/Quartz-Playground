namespace QuartzWebTemplate.Quartz.Locking.Contracts
{
    interface ISynchronizationTokenHolder
    {
        object GetTokenFor(TokenFor @for);
    }

    public enum TokenFor
    {
        Undefined,
        JobsLocking,
        ApiLocking
    }
}
