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
        public async Task<User> AddUserAsync(User entity)
        {
            var result = await _uerContext.User.AddAsync(entity);
            bool success = await _uerContext.SaveChangesAsync() > 0;
            if (success)
            {
                return result.Entity;
            }
            return null;
        }
    }
}
