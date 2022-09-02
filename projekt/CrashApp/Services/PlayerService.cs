using CrashApp.Constants;
using CrashApp.Data;
using CrashApp.Data.Entities;
using CrashApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CrashApp.Services
{
    /// <summary>
    /// klasa odpowiadająca za graczy (utworzenie podstawowego graczy jeżeli nie istnieje, tworzenie własnego gracza)
    /// </summary>
    public class PlayerService
    {
        public async Task<InitialPlayerResult> CreateInitialUserIfNeededAsync()
        {
            bool anyPlayerExists = await StaticContext.Context.Players.AnyAsync();

            bool shouldCreateInitialPlayer = !anyPlayerExists;

            var initialPlayerResult = new InitialPlayerResult();

            if (shouldCreateInitialPlayer)
            {
                initialPlayerResult.InitialPlayer = await CreateNewPlayerAsync("a", "a", "A_Imię", "A_Nazwisko", "aaaa@aa.aa", "69 69 69 69 69");
            }

            return initialPlayerResult;
        }
        /// <summary>
        ///  Klasa odpowiadjąca za tworzenie nowego gracza
        /// </summary>
        /// <param name="username">login</param>
        /// <param name="password">hasło</param>
        /// <param name="name">imię</param>
        /// <param name="surname">nazwisko</param>
        /// <param name="email">email</param>
        /// <param name="phoneNumber">numer telefonu </param>
        /// <returns> Nowego utworzonego użytkownika </returns>
        public async Task<Player> CreateNewPlayerAsync(string username, string password, string name, string surname, string email, string phoneNumber)
        {
            var player = new Player
            {
                Username = username,
                Password = password,
                Balance = SettingConstants.INITIAL_BALANCE,
                Contact = new Contact
                {
                    Name = name,
                    Surname = surname,
                    Email = email,
                    PhoneNumber = phoneNumber
                }
            };

            var entityEntry = StaticContext.Context.Players.Add(player);
            await StaticContext.Context.SaveChangesAsync();

            var newPlayer = entityEntry.Entity;

            return newPlayer;
        }
        /// <summary>
        /// Metoda sprawdzająca czy użytkownik istnieje
        /// </summary>
        /// <param name="username">login</param>
        /// <returns> Zwraca boola z informacją czy dany użytkownik istnieje </returns>
        public async Task<bool> PlayerWithSpecifiedUsernameExists(string username)
        {
            bool playerWithSpecifiedUsernameExists = await StaticContext.Context.Players.AnyAsync(p => p.Username == username);

            return playerWithSpecifiedUsernameExists;
        }
    }
}