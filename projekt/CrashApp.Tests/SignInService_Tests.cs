using CrashApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace CrashApp.Tests
{
    public class SignInService_Tests
    {
        private readonly SignInService _sut = new SignInService();
        private readonly PlayerService _playerService = new PlayerService();

        [Fact]
        public async Task SignInAsync_ShouldReturnTrue_WhenUsernameAndPasswordAreCorrect()
        {
            // Arrange
            await _playerService.CreateInitialUserIfNeededAsync();
            var player = await Data.StaticContext.Context.Players.FirstAsync();

            // Act
            var signInResult = await _sut.SignInAsync(player.Username, player.Password);

            // Assert
            Assert.True(signInResult.SignInSuccess);
        }

        [Fact]
        public async Task SignInAsync_ShouldReturnPlayerObject_WhenUsernameAndPasswordAreCorrect()
        {
            // Arrange
            await _playerService.CreateInitialUserIfNeededAsync();
            var player = await Data.StaticContext.Context.Players.FirstAsync();

            // Act
            var signInResult = await _sut.SignInAsync(player.Username, player.Password);
            bool correctPlayerObjectWasReturned = signInResult.SignedInPlayer == player;

            // Assert
            Assert.True(correctPlayerObjectWasReturned);
        }

        [Fact]
        public async Task SignInAsync_ShouldReturnFalse_WhenUsernameOrPasswordAreWrong()
        {
            // Arrange
            string notExistingUsername = "3r92hfqf38fhaiufas";
            string notExistingPassword = "qweri2j3h7q73rhe12";

            // Act
            var signInResult = await _sut.SignInAsync(notExistingUsername, notExistingPassword);

            // Assert
            Assert.False(signInResult.SignInSuccess);
        }

        [Fact]
        public async Task SignInAsync_ShouldReturnNullAsPlayerObject_WhenUsernameOrPasswordAreWrong()
        {
            // Arrange
            string notExistingUsername = "3r92hfqf38fhaiufas";
            string notExistingPassword = "qweri2j3h7q73rhe12";

            // Act
            var signInResult = await _sut.SignInAsync(notExistingUsername, notExistingPassword);
            bool signedInPlayerIsNull = signInResult.SignedInPlayer == null;

            // Assert
            Assert.True(signedInPlayerIsNull);
        }
    }
}