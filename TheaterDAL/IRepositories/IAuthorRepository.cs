using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL.Entities;

namespace TheaterDAL.IRepositories
{
    public interface IAuthorRepository : IRepository<AuthorEntity, Guid>
    {

    }
}
