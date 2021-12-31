using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterLogic.Models;

namespace TheaterAPI.Model.QueryProcessing.Util
{
    public class ShowComparer : IEqualityComparer<Show>
    {
        public bool Equals(Show x, Show y)
        {
            // TODO - Add null handling.
            return x.Id == y.Id;
        }

        public int GetHashCode(Show obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
