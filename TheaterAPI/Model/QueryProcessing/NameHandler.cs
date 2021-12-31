using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterAPI.Model.QueryProcessing.Util;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Models;

namespace TheaterAPI.Model.QueryProcessing
{
    class NameHandler : QueryHandler
    {
        public override List<Show> HandleRequest(ShowParameters showParameters, IShowService showService, List<Show> shows)
        {
            if (showParameters.ShowName != null)
            {
                List<Show> showsByName = showService.FindByName(showParameters.ShowName).Intersect(shows, new ShowComparer()).ToList();

                if (Successor != null)
                {
                    return Successor.HandleRequest(showParameters, showService, showsByName);
                }
                else
                    return showsByName;
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
