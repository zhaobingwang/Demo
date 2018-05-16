using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User entity);
    }
}
