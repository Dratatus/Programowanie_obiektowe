using CrashApp.Services;
using CrashApp.Windows;
using System.Diagnostics;
using System.Windows;

namespace CrashApp
{
    public partial class SignInWindow : Window
    {
        private readonly SignInService _signInService = new SignInService();
        private readonly PlayerService _playerService = new PlayerService();

        // Serwis developerski do testów. Cała klasa i ten obiekt do usunięcia jak praca nad apką będzie zakończona i zweryfikowana
        private readonly TestService _testDataService = new TestService();

        public SignInWindow()
        {
            InitializeComponent();
        }

        private async void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var initialPlayerResult = await _playerService.CreateInitialUserIfNeededAsync();

            if (initialPlayerResult.WasInitialPlayerCreated)
            {
                tbxUsername.Text = initialPlayerResult.InitialPlayer.Username;
                tbxPassword.Password = initialPlayerResult.InitialPlayer.Password;

                MessageBox.Show("Initial player was created.");
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string password = tbxPassword.Password;

            EnableInterface(false);
            var signInResult = await _signInService.SignInAsync(username, password);
            EnableInterface(true);

            if (signInResult.SignInSuccess)
            {
                // Stworzenie obiektu nowego okna
                var gameWindow = new GameWindow(signInResult.SignedInPlayer);

                // Podczepienie się pod event wylogowania - obsługą jest ujawnienie okna logowania
                gameWindow.OnSignOut += () =>
                {
                    Visibility = Visibility.Visible;
                };

                // Podczepienie się pod event zamknięcia formy krzyżykiem - obsługą jest zamknięcie tej formy (formy logowania)
                gameWindow.OnCrossExit += () =>
                {
                    Close();
                };

                // Ukazanie okna gracza
                gameWindow.Show();

                // Schowanie okna logowania
                Visibility = Visibility.Collapsed;

                tbxUsername.Clear();
                tbxPassword.Clear();
            }
            else
            {
                MessageBox.Show("Incorrect username or password.");
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var newPlayerWindow = new NewPlayerWindow();

            bool? dialogResult = newPlayerWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                tbxUsername.Text = newPlayerWindow.NewPlayer.Username;
                tbxPassword.Password = newPlayerWindow.NewPlayer.Password;
            }
        }

        // Metoda blokująca interfejs. Potrzebne jest to do tego, że jeśli metoda synchroniczna wykonuje się x sekund, a interfejs podczas tego czasu nie jest zamrożony,
        // trzeba zablokować użytkownikowi klikania rzeczy
        private void EnableInterface(bool enabled)
        {
            tbxUsername.IsEnabled = enabled;
            tbxPassword.IsEnabled = enabled;
            btnLogin.IsEnabled = enabled;
            hyperLinkShowNewPlayerWindow.IsEnabled = enabled;
        }
    }
}