using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace lab_07
{
    public class DatabaseOperations : IDatabaseOperations
    {
        private string _connectionString = @"Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=True";

        public List<UserEntity> GetAll()
        {
            using (DataContext dataContext = new DataContext(_connectionString))
            {
                Table<UserEntity> users = dataContext.GetTable<UserEntity>();

                IQueryable<UserEntity> query = from user in users
                                               where user.RemovedAt.HasValue == false
                                               select user;

                List<UserEntity> userEntities = query.ToList();

                return userEntities;
            }
        }

        public void AddNew(UserEntity userEntity)
        {
            using (DataContext dataContext = new DataContext(_connectionString))
            {
                Table<UserEntity> users = dataContext.GetTable<UserEntity>();

                users.InsertOnSubmit(userEntity);

                dataContext.SubmitChanges();
            }
        }

        public void Edit(long id, UserEntity userEntity)
        {
            using (DataContext dataContext = new DataContext(_connectionString))
            {
                Table<UserEntity> users = dataContext.GetTable<UserEntity>();

                UserEntity foundUserEntity = GetUser(users, id);
                
                //foundUserEntity.Name = userEntity.Name;
                //foundUserEntity.Role = userEntity.Role;

                dataContext.SubmitChanges();
            }
        }

        public void Delete(long id)
        {
            using (DataContext dataContext = new DataContext(_connectionString))
            {
                Table<UserEntity> users = dataContext.GetTable<UserEntity>();

                UserEntity foundUserEntity = GetUser(users, id);

                users.DeleteOnSubmit(foundUserEntity);

                dataContext.SubmitChanges();
            }
        }

        private UserEntity GetUser(Table<UserEntity> users, long id)
        {
            IQueryable<UserEntity> query = from user in users
                                           where user.RemovedAt.HasValue == false
                                           where user.Id == id
                                           select user;

            UserEntity userEntity = query.FirstOrDefault();

            return userEntity;
        }
    }
}
