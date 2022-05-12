using System.Collections.Generic;

namespace lab_07
{
    public interface IDatabaseOperations
    {
        List<UserEntity> GetAll();

        void AddNew(UserEntity userEntity);

        void Edit(long id, UserEntity userEntity);

        void Delete(long id);
    }
}