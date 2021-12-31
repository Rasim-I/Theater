using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Models;

namespace TheaterAPI.Model.QueryProcessing
{
    abstract class QueryHandler
    {
        public QueryHandler Successor { get; set; }
        public abstract List<Show> HandleRequest(ShowParameters showParameters, IShowService showService, List<Show> shows);

    }
}
