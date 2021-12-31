using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL.IRepositories;

namespace TheaterDAL
{
    public interface IUnitOfWork : IDisposable
    {
        IShowRepository Shows { get; }
        IAuthorRepository Authors { get; }
        ITicketRepository Tickets { get; }
        IPlaceRepository Places { get; }
        int Save();

    }
}
