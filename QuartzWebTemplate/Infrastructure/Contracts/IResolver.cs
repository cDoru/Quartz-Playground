namespace QuartzWebTemplate.Infrastructure.Contracts
{
    public interface IResolver
    {
        T Resolve<T>();

        T ResolveKeyed<T>(string key);
    }
}