using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterAPI.Model.QueryProcessing.Util;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Models;

namespace TheaterAPI.Model.QueryProcessing
{
    class DateHandler : QueryHandler
    {
        public override List<Show> HandleRequest(ShowParameters showParameters, IShowService showService, List<Show> shows)
        {
            if (showParameters.ShowTime != new DateTime())
            {
                List<Show> showsByDate = showService.FindByShowTime(showParameters.ShowTime).Intersect(shows, new ShowComparer()).ToList();

                if (Successor != null)
                {
                    return Successor.HandleRequest(showParameters, showService, showsByDate);
                }
                else
                    return showsByDate;
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
