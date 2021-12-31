using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL.Entities;
using TheaterDAL.IRepositories;

namespace TheaterDAL.Repositories
{
    public class AuthorRepository : Repository<AuthorEntity, Guid>, IAuthorRepository
    {
        public AuthorRepository(TheaterContext context) : base(context)
        {

        }


    }
}
