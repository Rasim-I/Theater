using System;
using System.Collections.Generic;
using System.Text;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using TheaterDAL;
using TheaterDAL.Entities;
using TheaterDAL.IRepositories;
using TheaterDAL.Repositories;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Implementation.Mappers;
using TheaterLogic.Implementation.Services;
using TheaterLogic.Models;

namespace TheaterConsolePL.App_start
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IShowRepository>().To<ShowRepository>();
            Bind<ITicketRepository>().To<TicketRepository>();
            Bind<IAuthorRepository>().To<AuthorRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();

            Bind<IMapper<AuthorEntity, Author>>().To<AuthorMapper>();
            Bind<IMapper<TicketEntity, Ticket>>().To<TicketMapper>();
            Bind<IMapper<ShowEntity, Show>>().To<ShowMapper>();
            Bind<IMapper<PlaceEntity, Place>>().To<PlaceMapper>();

            Bind<IShowService>().To<ShowService>();
            Bind<IAuthorService>().To<AuthorService>();
            Bind<ITicketService>().To<TicketService>();

            Bind<IServiceFactory>().ToFactory();
        }

    }
}
