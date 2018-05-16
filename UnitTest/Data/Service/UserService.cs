using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Service
{
    public class UserService : IUserService
    {
        private UserContext _uerContext { get; }
        public UserService(UserContext userContext)
        {
            _uerContext = userContext;
        }
        public async Task<bool> AddUserAsync(User entity)
        {
            await _uerContext.User.AddAsync(entity);
            return await _uerContext.SaveChangesAsync() > 0;
        }
    }
}
