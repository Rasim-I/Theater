using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterAPI.Model.QueryProcessing.Util;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Models;

namespace TheaterAPI.Model.QueryProcessing
{
    class AuthorHandler : QueryHandler
    {
        public override List<Show> HandleRequest(ShowParameters showParameters, IShowService showService, List<Show> shows)
        {
            if(showParameters.AuthorId != Guid.Empty)
            {
                List<Show> showsByAuthor = showService.FindByAuthor(showParameters.AuthorId).Intersect(shows, new ShowComparer()).ToList();

                if (Successor != null)
                {
                    return Successor.HandleRequest(showParameters, showService, showsByAuthor);
                }
                else
                    return showsByAuthor;
            }
            else
            {
                if (Successor != null)
                {
                    return Successor.HandleRequest(showParameters, showService, shows);
                }
                else
                    return shows;
            }

        }
    }
}
