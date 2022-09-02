using CrashApp.Data;
using CrashApp.Data.Entities;
using CrashApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace CrashApp.Tests
{
    public class PlayerService_Tests
    {
        private readonly PlayerService _sut = new PlayerService();

        [Fact]
        public async Task CreateInitialUserIfNeededAsync_ShouldReturnFalse_WhenNoInitialPlayerIsNeeded()
        {
            // Arrange
            // Utworzenie nowego gracza, jeśli nie ma żadnych, żeby właściwy test przeszedł
            await _sut.CreateInitialUserIfNeededAsync();

            // Act
            var initialPlayerResult = await _sut.CreateInitialUserIfNeededAsync();

            // Assert
            Assert.False(initialPlayerResult.WasInitialPlayerCreated);
        }

        [Fact]
        public async Task CreateNewPlayerAsync_ShouldSavePlayerToDatabase()
        {
            // Arrange
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

            // Act
            await _sut.CreateNewPlayerAsync(player.Username,
                                            player.Password,
                                            player.Contact.Name,
                                            player.Contact.Surname,
                                            player.Contact.Email,
                                            player.Contact.PhoneNumber);

            bool theSamePlayerExists = await StaticContext.Context.Players.AnyAsync(p => p.Username == player.Username &&
                                                                                    p.Password == player.Password &&
                                                                                    p.Contact.Name == player.Contact.Name &&
                                                                                    p.Contact.Surname == player.Contact.Surname &&
                                                                                    p.Contact.Email == player.Contact.Email &&
                                                                                    p.Contact.PhoneNumber == player.Contact.PhoneNumber);

            // Assert
            Assert.True(theSamePlayerExists);
        }

        [Fact]
        public async Task PlayerWithSpecifiedUsernameExists_ShouldReturnTrue_IfPlayerExists()
        {
            // Arrange
            // Utworzenie nowego gracza, jeśli nie ma żadnych, żeby właściwy test przeszedł
            await _sut.CreateInitialUserIfNeededAsync();
            var player = await StaticContext.Context.Players.FirstAsync();

            // Act
            bool playerWithSpecifiedUsernameExists = await _sut.PlayerWithSpecifiedUsernameExists(player.Username);

            // Assert
            Assert.True(playerWithSpecifiedUsernameExists);
        }

        [Fact]
        public async Task PlayerWithSpecifiedUsernameExists_ShouldReturnFalse_IfPlayerDoesNotExists()
        {
            // Arrange
            string notExistingUsername = "12e98rj2ufrneiybf4h7829h3d9183dn";

            // Act
            bool playerWithSpecifiedUsernameExists = await _sut.PlayerWithSpecifiedUsernameExists(notExistingUsername);

            // Assert
            Assert.False(playerWithSpecifiedUsernameExists);
        }
    }
}