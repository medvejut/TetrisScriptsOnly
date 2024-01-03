using _Tetris.Gameplay;
using TMPro;
using UnityEngine;

namespace _Tetris.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI linesText;
        [SerializeField] private TetrominoPreviewUI previewUI;
        [SerializeField] private TextMeshProUGUI speedLevelText;

        private readonly GameplayModel gameplayModel = GameplayModel.Get();

        private void OnEnable()
        {
            UpdateScore();
            UpdatePreview();
            UpdateClearedLines();
            UpdateSpeedLevel();

            gameplayModel.ScoreChanged += UpdateScore;
            gameplayModel.LinesCleared += UpdateClearedLines;
            gameplayModel.NextTetrominoChanged += UpdatePreview;
            gameplayModel.SpeedLevelChanged += UpdateSpeedLevel;
        }

        private void OnDisable()
        {
            gameplayModel.ScoreChanged -= UpdateScore;
            gameplayModel.LinesCleared -= UpdateClearedLines;
            gameplayModel.NextTetrominoChanged -= UpdatePreview;
            gameplayModel.SpeedLevelChanged -= UpdateSpeedLevel;
        }

        private void UpdateSpeedLevel()
        {
            speedLevelText.text = $"Level: {gameplayModel.SpeedLevel}";
        }

        private void UpdateScore()
        {
            scoreText.text = $"Score: {gameplayModel.Score}";
        }

        private void UpdateClearedLines()
        {
            linesText.text = $"Lines: {gameplayModel.ClearLinesCount}";
        }

        private void UpdatePreview()
        {
            previewUI.SetTetromino(gameplayModel.NextTetromino);
        }
    }
}