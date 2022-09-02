using CrashApp.Data;
using System.Threading.Tasks;

namespace CrashApp.Services
{
    // Serwis "developerski", do testów
    /// <summary>
    /// serwis do tesów
    /// </summary>
    public class TestService
    {
        // Metoda 
        public async Task ClearAllTheDataAsync()
        {
            await StaticContext.Context.Database.EnsureDeletedAsync();
            await StaticContext.Context.Database.EnsureCreatedAsync();
        }
    }
}