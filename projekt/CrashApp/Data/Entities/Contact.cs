namespace CrashApp.Data.Entities
{
    /// <summary>
    /// Klasa bazodanowa encji, zawierająca właściwości kontaktów
    /// </summary>
    public class Contact : EntityBase
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}