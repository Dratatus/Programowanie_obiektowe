using CrashApp.Constants;
using CrashApp.Data;
using CrashApp.Data.Entities;
using CrashApp.Models;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CrashApp.Services
{
    /// <summary>
    /// klasa odpowiedzialna za logikę gry
    /// </summary>
    public class GameService
    {
        private readonly HighScoreService _highScoreService = new HighScoreService();

        private readonly int _multiplierInterval = 200;

        private int _millisecondsToStopTheGame;
        private int _currentMillisecondsPassed;

        private readonly DispatcherTimer _multiplierTimer;
        
        private decimal _multiplier = 0;

        public event Action<decimal, decimal> OnMultiplierChange;
        public event Action<FinishedGameResult> OnGameFinish;

        private Player _player;

        private decimal _bet;

        private bool _isGameRunning;

        public GameService(Player player)
        {
            _player = player;

            // DispatcherPriority.Normal sprawia, że tiki są regularne a nie dzikie i z turbo opóźnieniem
            _multiplierTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(_multiplierInterval)
            };

            _multiplierTimer.Tick += _timer_Tick;
        }

        /// <summary>
        /// Wystartowanie nowej Gry
        /// </summary>
        /// <param name="bet">Ilosć pieniędzy do obstawienia</param>
        /// <exception cref="InvalidOperationException">Wyjątek występujący jeśli podczas wystartowania gry jakaś już się toczy</exception>
        public void StartNewGame(decimal bet)
        {
            if (_isGameRunning)
            {
                throw new InvalidOperationException("Gra jest już w toku.");
            }

            _isGameRunning = true;
            _bet = bet;
            _currentMillisecondsPassed = 0;
            _millisecondsToStopTheGame = RandomNumberGenerator.GetInt32(3000, 10_000);

            _multiplierTimer.Start();
        }

        /// <summary>
        /// Zatrzymanie obecnej gry
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Wyjątek występujący gdy żadna gra się nie toczy </exception>
        public async Task StopCurrentGameAsync()
        {
            if (!_isGameRunning)
            {
                throw new InvalidOperationException("Żadna gra nie jest w toku.");
            }

            await StopCurrentGameAsync(true);
        }
        /// <summary>
        /// Zapisywanie gry do Highscoru
        /// </summary>
        /// <param name="game">gra którą zapisujemy</param>
        /// <returns></returns>
        public async Task SaveGameAsync(Game game)
        {
            StaticContext.Context.Games.Add(game);
            await StaticContext.Context.SaveChangesAsync();

            if (game.GameWin)
            {
                int gameHighScorePlace = await _highScoreService.GetGameHighScorePlaceAsync(game);

                bool isNewHighScore = gameHighScorePlace <= SettingConstants.HIGH_SCORE_GAME_MAX_AMOUNT;

                if (isNewHighScore)
                {
                    await _highScoreService.SaveGameAsHighScoreAsync(game);
                } 
            }
        }
        private async void _timer_Tick(object sender, EventArgs e)
        {
            _multiplier += 0.1m;

            decimal updatedPrize = _multiplier * _bet;

            OnMultiplierChange?.Invoke(_multiplier, updatedPrize);
            _currentMillisecondsPassed += _multiplierInterval;

            if (_currentMillisecondsPassed >= _millisecondsToStopTheGame)
            {
                await StopCurrentGameAsync(false);
            }
        }

        private async Task StopCurrentGameAsync(bool isWin)
        {
            _multiplierTimer.Stop();
            _isGameRunning = false;
            var game = await CreateFinishedGameResultAsync(isWin);
            _multiplier = 0;
            OnGameFinish?.Invoke(game);
        }

        private async Task<FinishedGameResult> CreateFinishedGameResultAsync(bool isWin)
        {
            decimal prize = _bet * _multiplier;

            var game = new Game
            {
                Player = _player,
                Multiplier = _multiplier,
                Bet = _bet,
                Prize = prize,
                GameWin = isWin
            };

            int gameHighScorePlace = await _highScoreService.GetGameHighScorePlaceAsync(game);

            bool isNewHighScore = gameHighScorePlace <= SettingConstants.HIGH_SCORE_GAME_MAX_AMOUNT;

            var finishedGameResult = new FinishedGameResult
            {
                Game = game,
                IsNewHighScore = isNewHighScore,
                HighScorePlace = gameHighScorePlace
            };

            return finishedGameResult;
        }
    }
}