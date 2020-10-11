using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventMaker.BLL.Interfaces
{
    public interface IEventManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateEventAsync(string userId);
    }
}
