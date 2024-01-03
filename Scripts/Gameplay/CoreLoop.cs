using System;
using _Tetris.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Tetris.Gameplay
{
    public class CoreLoop : MonoBehaviour
    {
        [SerializeField] private Board board;
        [SerializeField] private Controls controls;

        private float lastStepTime;
        private readonly GameplayModel gameplayModel = GameplayModel.Get();

        private void Start()
        {
            gameplayModel.SetNextTetromino(GetRandomTetromino());
            Spawn();
            lastStepTime = Time.time;
        }

        private void OnEnable()
        {
            controls.Dropped += Dropped;
        }

        private void OnDisable()
        {
            controls.Dropped -= Dropped;
        }

        private void Update()
        {
            if (gameplayModel.IsGameOver)
            {
                return;
            }

            if (Time.time < lastStepTime + gameplayModel.FallInterval)
            {
                return;
            }

            if (!controls.TryMove(Vector2Int.down))
            {
                ClearLines();
                Spawn();
            }

            lastStepTime = Time.time;
        }

        private void Dropped()
        {
            ClearLines();
            Spawn();
            lastStepTime = Time.time;
        }

        private void ClearLines()
        {
            var clearLinesCount = board.ClearLines();
            if (clearLinesCount != 0)
            {
                gameplayModel.IncreaseClearLinesCount(clearLinesCount);
            }
        }

        private void Spawn()
        {
            var tetromino = new Tetromino(gameplayModel.NextTetromino) { Position = board.SpawnPosition };

            if (!board.CanPlace(tetromino))
            {
                controls.SetActive(null);
                gameplayModel.SetGameOver();
                return;
            }

            board.Add(tetromino);
            controls.SetActive(tetromino);

            gameplayModel.IncreaseScore();
            gameplayModel.SetNextTetromino(GetRandomTetromino());
        }

        private TetrominoData GetRandomTetromino()
        {
            var types = Enum.GetValues(typeof(TetrominoType));
            var type = (TetrominoType)Random.Range(0, types.Length);
            return new TetrominoData(type);
        }
    }
}