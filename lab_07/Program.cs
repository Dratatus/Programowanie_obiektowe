using System;
using System.Collections.Generic;

namespace lab_07
{
    public class Program
    {
        private static IDatabaseOperations _databaseOperations = new DatabaseOperations();

        public static void Main(string[] args)
        {
            //List<UserEntity> users = _databaseOperations.GetAll();

            //UserEntity daniel = users[0];

            //daniel.Name = "Seba";

            //_databaseOperations.Edit(daniel.Id, daniel);

            //List<UserEntity> usersAgain = _databaseOperations.GetAll();

            List<UserEntity> users = _databaseOperations.GetAll();

            UserEntity daniel = users[0];

            _databaseOperations.Delete(daniel.Id);
            
            List<UserEntity> usersAgain = _databaseOperations.GetAll();

            Console.ReadKey();
        }

        private static void InsertNewUser(string name, string role)
        {
            UserEntity userEntity = new UserEntity
            {
                Name = name,
                Role = role
            };

            _databaseOperations.AddNew(userEntity);
        }
    }
}
