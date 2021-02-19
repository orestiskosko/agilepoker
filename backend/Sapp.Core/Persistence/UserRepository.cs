using System;
using Sapp.Core.Entities;
using Sapp.Core.Interfaces;

namespace Sapp.Core.Persistence
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(ApiContext dbContext) : base(dbContext)
        { }
    }
}