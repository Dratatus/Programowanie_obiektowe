using CrashApp.Data.Entities;

namespace CrashApp.Models
{
    /// <summary>
    /// klasa zawierająca właściowości czy wynik zalicza się do highscoru
    /// </summary>
    public class HighScoreGridRowResult
    {
        public int Number { get; set; }

        public string PlayerDescription => $"{HighScore.Game.Player.Contact.Name} {HighScore.Game.Player.Contact.Surname}";

        public HighScore HighScore { get; set; }

        public bool IsHighlighted { get; set; }
    }
}