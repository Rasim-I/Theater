using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheaterDAL;
using TheaterDAL.Entities;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Models;

namespace TheaterLogic.Implementation.Services
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper<AuthorEntity, Author> _authorMapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper<AuthorEntity, Author> authorMapper)
        {
            _unitOfWork = unitOfWork;
            _authorMapper = authorMapper;
        }

        public List<Author> GetAll()
        {
            return new List<Author>(_unitOfWork.Authors.GetAll().ToList().ConvertAll(_authorMapper.ToModel));
        }

        public Author GetAuthor(Guid authorId)
        {
            AuthorEntity authorEntity = _unitOfWork.Authors.Get(authorId);

            if (authorEntity != null)
            {
                return _authorMapper.ToModel(authorEntity);
            }
            else
            {
                return null;
            }
            
        }

        public void AddAuthor(Author author)
        {
            _unitOfWork.Authors.Create(_authorMapper.ToEntity(author));
            _unitOfWork.Save();
        }

        public List<Author> FindByName(string authorName)
        {
            return _unitOfWork.Authors
                .Find(a => (a.Name.ToLower() + a.Surname.ToLower()).Contains(authorName.ToLower()))
                .ToList()
                .ConvertAll(a => _authorMapper.ToModel(a));

        }

    }
}
