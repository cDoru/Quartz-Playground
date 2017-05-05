using System;
using System.Collections.Concurrent;
using QuartzWebTemplate.Quartz.Entities;
using QuartzWebTemplate.Quartz.Locking.Contracts;

namespace QuartzWebTemplate.Quartz.Locking.Impl
{
    public class SynchronizationTokenHolder : ISynchronizationTokenHolder
    {
        private readonly object _tokenObject;
        private readonly ConcurrentDictionary<TokenFor, object> _tokens;

        public SynchronizationTokenHolder()
        {
            _tokenObject = new object();
            _tokens = new ConcurrentDictionary<TokenFor, object>();
        }

        public object GetTokenFor(TokenFor @for)
        {
            lock (_tokenObject)
            {
                if (_tokens.ContainsKey(@for))
                {
                    object token;
                    if ((token = _tokens[@for]) == null)
                        throw new InvalidOperationException("token is null");

                    return token;
                }
                else
                {
                    var token = new object();
                    if (!_tokens.TryAdd(@for, token))
                        throw new InvalidOperationException("Could not add token");
                    return token;
                }
            }
        }
    }
}