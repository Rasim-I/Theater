using System;
using System.Collections.Generic;
using System.Text;
using TheaterLogic.Abstraction.IServices;

namespace TheaterConsolePL.App_start
{
    public interface IServiceFactory
    {
        IAuthorService CreateAuthorService();
        ITicketService CreateTicketService();
        IShowService CreateShowService();

    }
}
