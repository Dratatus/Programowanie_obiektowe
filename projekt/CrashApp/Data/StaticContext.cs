namespace CrashApp.Data
{
    /// <summary>
    /// statyczna klasa z instancją zawartego kontekstu
    /// Statyczna dlatego, żeby była tylko 1 instancja tego contextu
    /// </summary>
    public static class StaticContext
    {
        public static CrashAppContext Context { get; } = new CrashAppContext();

        // Konstruktor klasy statycznej wykonuje się TYLKO 1 raz po 1 użyciu klasy statycznej
        static StaticContext()
        {
            // Metoda sprawdzająca, czy baza istnieje. Jeśli nie, wygeneruje ją na podstawie kontekstu.
            Context.Database.EnsureCreated();
        }
    }
}   