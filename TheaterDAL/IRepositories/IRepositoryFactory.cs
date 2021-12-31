using System;
using System.Collections.Generic;
using System.Text;

namespace TheaterDAL.IRepositories
{
    public interface IRepositoryFactory
    {
        IAuthorRepository CreateAuthorRepository();
        ITicketRepository CreateTicketRepository();
        IShowRepository CreateShowRepository();

    }
}
