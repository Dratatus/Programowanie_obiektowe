using CrashApp.Data.Entities;

namespace CrashApp.Models
{
    /// <summary>
    /// klasa zawierająca właściwości logowania (czy udane, zalogowany gracz)
    /// </summary>
    public class SignInResult
    {
        public bool SignInSuccess { get; set; }

        public Player SignedInPlayer { get; set; }
    }
}