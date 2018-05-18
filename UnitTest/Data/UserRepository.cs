using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(User entity)
        {
            var created = await _context.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
