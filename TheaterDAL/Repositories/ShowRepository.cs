using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheaterDAL.Entities;
using TheaterDAL.Entities.Enum;
using TheaterDAL.IRepositories;
using System.Data.Entity;


namespace TheaterDAL.Repositories
{
    public class ShowRepository : Repository<ShowEntity, Guid>, IShowRepository
    {
        public ShowRepository(TheaterContext context) : base(context)
        {
        }

        public IEnumerable<ShowEntity> GetByGenre(Genre genre)
        {
            return db.Shows.Where(s => s.Genre == genre);
        }

        public IEnumerable<ShowEntity> GetByAuthor(AuthorEntity author)
        {
            return db.Shows.Where(s => s.Author.Id == author.Id);
        }

        
        public IEnumerable<ShowEntity> GetAll_IncludeAll()
        {
            return db.Shows.Include(s => s.Author).Include(s => s.TicketList);
        }

        
    }
}
