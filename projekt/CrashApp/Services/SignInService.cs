using CrashApp.Data;
using CrashApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CrashApp.Services
{
    /// <summary>
    /// Klasa logowania
    /// </summary>
    public class SignInService
    {
        /// <summary>
        /// Metoda odpowiadająca za logowanie utworzonego już użytkownika
        /// </summary>
        /// <param name="username">login</param>
        /// <param name="password">hasło</param>
        /// <returns> Czy udało się zalogować </returns>
        public async Task<SignInResult> SignInAsync(string username, string password)
        {
            // Metoda zwraca nulla, jeśli gracz nie został znaleziony
            var player = await StaticContext.Context.Players.SingleOrDefaultAsync(p => p.Username == username && p.Password == password);

            // Bool, czy został znaleziony gracz
            bool playerFound = player != null;

            // Zrócenie obiektu z informacjami o zalogowaniu
            var signInResult = new SignInResult
            {
                SignInSuccess = playerFound,
                SignedInPlayer = player
            };

            return signInResult;
        }
    }
}