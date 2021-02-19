using System;
using Sapp.Core.Entities;

namespace Sapp.Core.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    { }
}