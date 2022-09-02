using CrashApp.Data;
using CrashApp.Data.Entities;
using CrashApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CrashApp.Tests
{
    public class HighScoreService_Tests
    {
        private readonly HighScoreService _sut = new HighScoreService();
        private readonly Player _player;

        public HighScoreService_Tests()
        {
            _player = new Player
            {
                Id = 1,
                Username = "zbyniu",
                Password = "stonóżka",
                Balance = 100,
                Contact = new Contact
                {
                    Id = 1,
                    Name = "Zbigniew",
                    Surname = "Stonoga",
                    Email = "zbyniu_stonoga@wp.pl",
                    PhoneNumber = "69 69 69 69 69"
                }
            };
        }

        [Fact]
        public async Task SaveGameAsHighScoreAsync_ShouldSaveGameAsHighScore()
        {
            // Arrange
            var game = CreateGame();

            // Act
            await _sut.SaveGameAsHighScoreAsync(game);
            var theSameGame = await StaticContext.Context.Games.SingleAsync(g => g == game);
            bool gameWasSavedToTheDatabase = game == theSameGame;

            // Assert
            Assert.True(gameWasSavedToTheDatabase);

            StaticContext.Context.Games.Remove(game);
            await StaticContext.Context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetGameHighScorePlaceAsync_ShouldGiveCorrectPlace()
        {
            // Arrange
            var game = CreateGame();
            int allBetterHighScoresCount = await StaticContext.Context.HighScores.CountAsync(hs => hs.Game.Prize >= game.Prize);

            // Act
            int highScorePlace = await _sut.GetGameHighScorePlaceAsync(game);
            int properHighScorePlace = allBetterHighScoresCount + 1;
            bool gameHasProperHighScore = highScorePlace == properHighScorePlace;

            // Assert
            Assert.True(gameHasProperHighScore);
        }

        [Fact]
        public async Task GetAllHighScoresAsync_ShouldReturnProperGames()
        {
            // Arrange
            var properAllHighScoreGames = await StaticContext.Context.HighScores.Select(hs => hs.Game).ToListAsync();

            // Act
            var allHighScoreGames = await _sut.GetAllHighScoresAsync(_player);
            bool areAllHighScoreGamesProper = properAllHighScoreGames.Select((g, i) => g == properAllHighScoreGames[i]).All(x => x);

            // Assert
            Assert.True(areAllHighScoreGamesProper);
        }

        private Game CreateGame()
        {
            var player = new Player
            {
                Username = "zbyniu",
                Password = "stonóżka",
                Balance = 100,
                Contact = new Contact
                {
                    Name = "Zbigniew",
                    Surname = "Stonoga",
                    Email = "zbyniu_stonoga@wp.pl",
                    PhoneNumber = "69 69 69 69 69"
                }
            };

            var game = new Game
            {
                Player = player,
                Multiplier = 1,
                Bet = 100,
                Prize = 100,
                GameWin = true
            };

            return game;
        }
    }
}