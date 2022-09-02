using System;

namespace CrashApp.Data.Entities
{
    /// <summary>
    /// klasa przechowująca właściwości Highscorów
    /// </summary>
    public class HighScore : EntityBase
    {
        public DateTime Date { get; set; }

        public virtual int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}