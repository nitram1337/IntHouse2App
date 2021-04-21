using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntHouse2App.Repository
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(string uri, string authToken = "");
        Task<R> GetAsync<T, R>(string uri, T data, string authToken = "");
    }
}
