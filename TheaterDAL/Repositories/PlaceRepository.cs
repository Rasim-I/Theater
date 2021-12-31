using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL.Entities;
using TheaterDAL.IRepositories;

namespace TheaterDAL.Repositories
{
    public class PlaceRepository : Repository<PlaceEntity, Guid>, IPlaceRepository
    {
        public PlaceRepository(TheaterContext context) : base(context)
        {

        }
    }
}
