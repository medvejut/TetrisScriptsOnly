using System;
using JetBrains.Annotations;
using UnityEngine;

namespace _Tetris.Gameplay
{
    public class Controls : MonoBehaviour
    {
        [SerializeField] private Board board;

        private Tetromino activeTetromino;

        public Action Dropped;

        public void SetActive([CanBeNull] Tetromino tetromino)
        {
            activeTetromino = tetromino;
        }

        private void Update()
        {
            if (activeTetromino == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TryMove(Vector2Int.left);
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                TryMove(Vector2Int.right);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                TryMove(Vector2Int.down);
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                TryRotate();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                while (TryMove(Vector2Int.down))
                {
                }

                Dropped?.Invoke();
            }
        }

        public bool TryMove(Vector2Int offset)
        {
            board.Remove(activeTetromino);

            activeTetromino.Position += offset;
            var canPlace = board.CanPlace(activeTetromino);
            if (!canPlace)
            {
                activeTetromino.Position -= offset;
            }

            board.Add(activeTetromino);

            return canPlace;
        }

        private void TryRotate()
        {
            board.Remove(activeTetromino);

            activeTetromino.Rotate(clockwise: true);
            var canPlace = TryPlaceWithWallKicks();
            if (!canPlace)
            {
                activeTetromino.Rotate(clockwise: false);
            }

            board.Add(activeTetromino);
        }

        private bool TryPlaceWithWallKicks()
        {
            var position = activeTetromino.Position;

            var offsets = activeTetromino.GetWallKicksOffsets();
            foreach (var offset in offsets)
            {
                activeTetromino.Position = position + offset;
                if (board.CanPlace(activeTetromino))
                {
                    return true;
                }
            }

            activeTetromino.Position = position;
            return false;
        }
    }
}