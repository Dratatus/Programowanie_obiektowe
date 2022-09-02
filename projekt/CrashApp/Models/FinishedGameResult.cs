using CrashApp.Data.Entities;

namespace CrashApp.Models
{
    /// <summary>
    /// klasa zawierająca właściowości skończonych gier
    /// </summary>
    public class FinishedGameResult
    {
        public Game Game { get; set; }

        public bool IsNewHighScore { get; set; }

        public int HighScorePlace { get; set; }
    }
}