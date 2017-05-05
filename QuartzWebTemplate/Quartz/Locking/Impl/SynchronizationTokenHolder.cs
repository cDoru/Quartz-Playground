using System;
using System.Collections.Concurrent;
using QuartzWebTemplate.Quartz.Locking.Contracts;

namespace QuartzWebTemplate.Quartz.Locking.Impl
{
    public class SynchronizationTokenHolder : ISynchronizationTokenHolder
    {
        private readonly object _tokenObject;
        private readonly ConcurrentDictionary<TokenFor, object> Tokens;

        public SynchronizationTokenHolder()
        {
            _tokenObject = new object();
            Tokens = new ConcurrentDictionary<TokenFor, object>();
        }

        public object GetTokenFor(TokenFor @for)
        {
            lock (_tokenObject)
            {
                if (Tokens.ContainsKey(@for))
                {
                    object token;
                    if ((token = Tokens[@for]) == null)
                        throw new InvalidOperationException("token is null");

                    return token;
                }
                else
                {
                    var token = new object();
                    if (!Tokens.TryAdd(@for, token))
                        throw new InvalidOperationException("Could not add token");
                    return token;
                }
            }
        }
    }
}