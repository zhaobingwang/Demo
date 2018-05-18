using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Service
{
    public class UserService : UserRepository
    {
        private readonly IUserRepository _repository;
        public UserService(UserContext context, IUserRepository repository) : base(context)
        {
            context = new UserContext();
            _repository = repository;
        }

        public async Task<bool> AddUserAsync(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }
            if (entity.UserStatus == 1)
            {
                return false;
            }
            return await _repository.AddAsync(entity);
        }
    }
}
