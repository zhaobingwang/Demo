using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User entity);
    }
}
