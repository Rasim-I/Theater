using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL.Entities;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Models;

namespace TheaterLogic.Implementation.Mappers
{
    public class AuthorMapper : IMapper<AuthorEntity, Author>
    {
        public AuthorEntity ToEntity(Author model)
        {
            return new AuthorEntity
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                PhoneNumber = model.PhoneNumber
            };
        }

        public Author ToModel(AuthorEntity entity)
        {
            return new Author
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                PhoneNumber = entity.PhoneNumber
            };
        }
    }
}
