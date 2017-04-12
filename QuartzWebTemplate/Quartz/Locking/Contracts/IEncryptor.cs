namespace QuartzWebTemplate.Quartz.Locking.Contracts
{
    public interface IEncryptor
    {
        string Encrypt(string val);
        string Decrypt(string val);
    }
}