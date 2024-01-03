using System;
using _Tetris.Data;

namespace _Tetris.Gameplay
{
    public class GameplayModel
    {
        private readonly SpeedLevelsSettings speedLevelsSettings;

        public int Score { get; private set; }
        public int ClearLinesCount { get; private set; }
        public TetrominoData NextTetromino { get; private set; }
        public bool IsGameOver { get; private set; }
        public int SpeedLevel { get; private set; }
        public float FallInterval { get; private set; }

        public event Action ScoreChanged;
        public event Action LinesCleared;
        public event Action NextTetrominoChanged;
        public event Action GameOver;
        public event Action SpeedLevelChanged;

        private GameplayModel(SpeedLevelsSettings speedLevelsSettings)
        {
            this.speedLevelsSettings = speedLevelsSettings;
            SpeedLevel = 1;
            FallInterval = speedLevelsSettings.SpeedLevels[0].FallInterval;
        }

        public void IncreaseScore()
        {
            Score++;
            ScoreChanged?.Invoke();
        }

        public void IncreaseClearLinesCount(int clearLinesCount)
        {
            ClearLinesCount += clearLinesCount;
            LinesCleared?.Invoke();
            UpdateSpeedLevel();
        }

        private void UpdateSpeedLevel()
        {
            for (var levelIndex = SpeedLevel - 1; levelIndex < speedLevelsSettings.SpeedLevels.Length; levelIndex++)
            {
                var speedLevelData = speedLevelsSettings.SpeedLevels[levelIndex];
                if (ClearLinesCount < speedLevelData.ClearLinesCount)
                {
                    break;
                }

                SpeedLevel = levelIndex + 1;
                FallInterval = speedLevelData.FallInterval;
                SpeedLevelChanged?.Invoke();
            }
        }

        public void SetNextTetromino(TetrominoData tetromino)
        {
            NextTetromino = tetromino;
            NextTetrominoChanged?.Invoke();
        }

        public void SetGameOver()
        {
            IsGameOver = true;
            GameOver?.Invoke();
        }

        private static GameplayModel current;
        public static GameplayModel Get() => current;
        public static void StartNewGame(SpeedLevelsSettings speedLevelsSettings) => current = new GameplayModel(speedLevelsSettings);
    }
}