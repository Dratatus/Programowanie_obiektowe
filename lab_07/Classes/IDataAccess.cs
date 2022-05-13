using System.Collections.Generic;

namespace lab_07.Classes
{
    // Interfejs, czyli obowiązkowa część klas (klasy implementujące ten intefejs muszą zawierać te rzeczy)
    public interface IDataAccess
    {
        List<UserEntity> GetAll();

        void AddNew(UserEntity userEntity);

        void Edit(long id, UserEntity userEntity);

        void Delete(long id);
    }
}