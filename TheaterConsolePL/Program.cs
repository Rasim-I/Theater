using System;
using System.Reflection;
using Ninject;
using TheaterConsolePL.App_start;
using TheaterDAL;
using TheaterDAL.Entities;
using TheaterDAL.IRepositories;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Implementation.Mappers;
using TheaterLogic.Implementation.Services;
using TheaterLogic.Models;

namespace TheaterConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {

            StandardKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            IServiceFactory serviceFactory = kernel.Get<IServiceFactory>();

            {
                //TheaterContext context = new TheaterContext();
                //IUnitOfWork unitOfWork = new UnitOfWork(context);

                //IMapper<AuthorEntity, Author> authorMapper = new AuthorMapper();
                //IMapper<TicketEntity, Ticket> ticketMapper = new TicketMapper();
                //IMapper<ShowEntity, Show> showMapper = new ShowMapper();
                //IMapper<PlaceEntity, Place> placeMapper = new PlaceMapper();

                //IAuthorService authorService = new AuthorService(unitOfWork, authorMapper);
                //ITicketService ticketService = new TicketService(unitOfWork, ticketMapper, placeMapper);
                //IShowService showService = new ShowService(unitOfWork, authorService, ticketService, showMapper);
            }


            string action_user = "user";
            string action_admin_create = "admin_create";
            string action_admin_remove = "admin_remove";
            
            Window w = new Window(serviceFactory, action_user);

        }
    }
}
