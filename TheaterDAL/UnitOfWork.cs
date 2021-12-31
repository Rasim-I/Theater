using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL.Entities;
using TheaterDAL.IRepositories;
using TheaterDAL.Repositories;

namespace TheaterDAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TheaterContext _context;
        public IShowRepository Shows { get; private set; }
        public IAuthorRepository Authors { get; private set; }
        public ITicketRepository Tickets { get; private set; }
        public IPlaceRepository Places { get; private set; }
        //private readonly IRepositoryFactory _repositoryFactory;

        public UnitOfWork(TheaterContext context)
        {
            _context = context;
            //_reposityFactory = repositoryFactory;
            Shows = new ShowRepository(_context);
            Authors = new AuthorRepository(_context);
            Tickets = new TicketRepository(_context);
            Places = new PlaceRepository(_context);

        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
