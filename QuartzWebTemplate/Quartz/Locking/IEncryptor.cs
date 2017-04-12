namespace QuartzWebTemplate.Quartz.Locking
{
    public interface IEncryptor
    {
        string Encrypt(string val);
        string Decrypt(string val);
    }
}