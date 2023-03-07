using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDomain.Models;

namespace TaskManagerDomain.Abstractions.Services
{
    public interface IUserServices
    {

        Task<string> GetUsername();
    }
}
