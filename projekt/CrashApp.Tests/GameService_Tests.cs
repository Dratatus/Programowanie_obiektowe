using CrashApp.Data;
using CrashApp.Data.Entities;
using CrashApp.Models;
using CrashApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CrashApp.Tests
{
    /// <summary>
    /// Klasa testowania Gry
    /// </summary>
    public class GameService_Tests
    {
        private readonly GameService _sut;

        public GameService_Tests()
        {
            var player = new Player
            {
                Id = 1,
                Username = "zbyniu",
                Password = "stonó¿ka",
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

            _sut = new GameService(player);
        }
    
        [Fact]
        public void StartNewGame_Should_ThrowException_WhenGameIsRunning()
        {
            var testCode = new Action(() =>
            {
                _sut.StartNewGame(100);
                _sut.StartNewGame(100);
            });

            Assert.Throws<InvalidOperationException>(testCode);
        }

        [Fact]
        public async Task StopCurrentGameAsync_Should_ThrowException_WhenGameIsNotRunning()
        {
            var testCode = new Func<Task>(async () =>
            {
                await _sut.StopCurrentGameAsync();
            });

            await Assert.ThrowsAsync<InvalidOperationException>(testCode);
        }

        [Fact]
        public async Task StopCurrentGameAsync_Should_RaiseOnGameFinishEventWithWonGame()
        {
            // Arrange
            FinishedGameResult finishedGameResult = null;
            _sut.OnGameFinish += fgr => finishedGameResult = fgr;

            // Act
            _sut.StartNewGame(100);
            await _sut.StopCurrentGameAsync();

            // Assert
            Assert.True(finishedGameResult.Game.GameWin);
        }

        [Fact]
        public async Task SaveGameAsync_Should_SaveGameToDatabase()
        {
            // Arrange
            var player = new Player
            {
                Username = "zbyniu",
                Password = "stonó¿ka",
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

            // Act
            await _sut.SaveGameAsync(game);

            var theSameGame = await StaticContext.Context.Games.SingleAsync(g => g == game);

            bool wasGameSavedToDatabase = game == theSameGame;

            // Assert
            Assert.True(wasGameSavedToDatabase);
        }
    }
}