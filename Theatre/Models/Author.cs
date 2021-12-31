using System;
using System.Collections.Generic;
using System.Text;

namespace TheaterLogic.Models
{
    public class Author
    {
        private Guid _Id;

        public Guid Id
        {
            get => _Id;
            set => _Id = value;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }


        private string _surname;
        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }


        private int _phoneNumber;
        public int PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public Author(string name, string surname, int phoneNumber = 0)
        {
            this.Name = name;
            this.Surname = surname;
            this.PhoneNumber = phoneNumber;
        }

        public Author(){}
    }
}
