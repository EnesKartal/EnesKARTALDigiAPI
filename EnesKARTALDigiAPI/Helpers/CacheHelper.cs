using EnesKARTALDigiAPI.Data.Models;
using EnesKARTALDigiAPI.Data.Repositories.Infra;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnesKARTALDigiAPI.Helpers
{
    public interface ICacheManager
    {
        bool CheckUserExists(string email);
        void RemoveCache(string key);
    }

    public class CacheManager : ICacheManager
    {
        readonly IMemoryCache memoryCache;
        IUserRepository userRepository;
        public CacheManager(IUserRepository userRepository, IMemoryCache memoryCache)
        {
            this.userRepository = userRepository;
            this.memoryCache = memoryCache;
        }

        public bool CheckUserExists(string email)
        {
            memoryCache.TryGetValue("USERLIST", out List<User> items);
            if (items == null)
            {
                items = userRepository.GetAll().ToList();
                memoryCache.Set("USERLIST", items, TimeSpan.FromDays(1));
            }
            return items.Any(p => p.Email == email);
        }

        public void RemoveCache(string key)
        {
            memoryCache.Remove(key);
        }
    }
}