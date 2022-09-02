using CrashApp.Data.Entities;

namespace CrashApp.Models
{
    /// <summary>
    /// klasa zawierająca właściwości konta podstawowego gracza, jeśli go nie ma - tworzy go
    /// </summary>
    public class InitialPlayerResult
    {
        public Player InitialPlayer { get; set; }

        public bool WasInitialPlayerCreated => InitialPlayer != null;
    }
}