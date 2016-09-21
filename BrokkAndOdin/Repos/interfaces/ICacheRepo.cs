using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokkAndOdin.Repos.interfaces
{
    public interface ICacheRepo
    {
        T Get<T>(string key);

        void Add(object obj, string key);

        void Add(object obj, string key, int millisecondsToCache);

        void Remove(string key);
    }
}
