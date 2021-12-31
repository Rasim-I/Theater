using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterAPI.Model.QueryProcessing.Util;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Models;
using TheaterLogic.Models.Enum;

namespace TheaterAPI.Model.QueryProcessing
{
    class GenreHandler : QueryHandler
    {
        public override List<Show> HandleRequest(ShowParameters showParameters, IShowService showService, List<Show> shows)
        {
            if (showParameters.Genre != null)
            {
                try
                {
                    Genre genreToSearch = (Genre)Enum.Parse(typeof(Genre), showParameters.Genre, true);
                    List<Show> showsByGenre = showService.FindByGenre(genreToSearch).Intersect(shows, new ShowComparer()).ToList();

                    if (Successor != null)
                        return Successor.HandleRequest(showParameters, showService, showsByGenre);
                    else
                        return showsByGenre;
                }
                catch
                {
                    if (Successor != null)
                        return Successor.HandleRequest(showParameters, showService, shows);
                    else
                        return shows;
                }









            }
            else
            {
                if (Successor != null)
                    return Successor.HandleRequest(showParameters, showService, shows);
                else
                    return shows;
            }
        }

    }
}
