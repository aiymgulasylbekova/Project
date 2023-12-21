using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class User
    {
        public int nextID = 1;

        public int ID { get; } //id
        public string Username { get; }//name
        public double Balance { get; set; }// balance

        public User(string username)//конструктор класса юзер
        {
            ID = nextID++;
            Username = username;
            Balance = 1000; // Начальный баланс пользователя
        }
    }
}
