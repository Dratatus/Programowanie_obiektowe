using System;
using System.Data.Linq.Mapping;

namespace lab_07.Classes
{
    [Table(Name = "Users")]
    public class UserEntity
    {
        [Column(IsPrimaryKey = true, Name = "Id", IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public long Id { get; set; }

        [Column(Name = "Name", UpdateCheck = UpdateCheck.Never)]
        public string Name { get; set; }

        [Column(Name = "Role", UpdateCheck = UpdateCheck.Never)]
        public string Role { get; set; } // ADMIN, MODERATOR, TEACHER, STUDENT

        [Column(Name = "CreatedAt", UpdateCheck = UpdateCheck.Never)]
        public DateTime? CreatedAt { get; set; }

        [Column(Name = "RemovedAt", UpdateCheck = UpdateCheck.Never)]
        public DateTime? RemovedAt { get; set; }

        public UserEntity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
