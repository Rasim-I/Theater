using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL.Entities;
using TheaterDAL.Entities.Enum;

namespace TheaterDAL.IRepositories
{
    public interface IShowRepository : IRepository<ShowEntity, Guid>
    {
        IEnumerable<ShowEntity> GetByGenre(Genre genre);

        IEnumerable<ShowEntity> GetByAuthor(AuthorEntity author);

        IEnumerable<ShowEntity> GetAll_IncludeAll();

    }
}
