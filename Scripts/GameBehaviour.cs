using _Tetris.Gameplay;
using UnityEngine;

namespace _Tetris
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreenPrefab;

        private readonly GameplayModel gameplayModel = GameplayModel.Get();

        private void OnEnable()
        {
            gameplayModel.GameOver += GameplayModelOnGameOver;
        }

        private void OnDisable()
        {
            gameplayModel.GameOver -= GameplayModelOnGameOver;
        }

        private void GameplayModelOnGameOver()
        {
            Instantiate(gameOverScreenPrefab);
        }
    }
}