using System;
using System.Collections.Generic;
using System.Text;
using TheaterLogic.Models;

namespace TheaterLogic.Abstraction.IServices
{
    public interface IAuthorService
    {
        public List<Author> GetAll();
        public Author GetAuthor(Guid authorId);
        public void AddAuthor(Author author);
        public List<Author> FindByName(string authorName);
    }
}
