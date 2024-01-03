using UnityEngine;

namespace _Tetris.Gameplay
{
    public class Board : MonoBehaviour
    {
        private class Tile
        {
            public GameObject GameObject;
        }

        [SerializeField] private Transform tilesParent;
        [SerializeField] private GameObject tilePrefab;

        private readonly Tile[] tiles = new Tile[Size.x * Size.y];
        private static readonly Vector2Int Size = new(10, 20);

        public Vector2Int SpawnPosition => new(Size.x / 2 - 1, Size.y - 2);

        public void Add(Tetromino tetromino)
        {
            foreach (var cell in tetromino.Cells)
            {
                AddTile(tetromino.Position.x + cell.x, tetromino.Position.y + cell.y);
            }
        }

        public void Remove(Tetromino tetromino)
        {
            foreach (var cell in tetromino.Cells)
            {
                RemoveTile(tetromino.Position.x + cell.x, tetromino.Position.y + cell.y);
            }
        }

        public bool CanPlace(Tetromino tetromino)
        {
            foreach (var cell in tetromino.Cells)
            {
                var x = tetromino.Position.x + cell.x;
                var y = tetromino.Position.y + cell.y;

                if (x < 0 || x >= Size.x || y < 0 || y >= Size.y)
                {
                    return false;
                }

                if (!IsEmptyTile(x, y))
                {
                    return false;
                }
            }

            return true;
        }

        private void AddTile(int x, int y)
        {
            var tile = Instantiate(tilePrefab, tilesParent);
            tile.transform.localPosition = GetTilePosition(x, y);

            SetTile(x, y, new Tile { GameObject = tile });
        }

        private void RemoveTile(int x, int y)
        {
            Destroy(GetTile(x, y).GameObject);

            SetTile(x, y, null);
        }

        private Vector3 GetTilePosition(int x, int y)
        {
            return new Vector3(x, y, 0f);
        }

        public int ClearLines()
        {
            var clearLinesCount = 0;

            var line = 0;
            while (line < Size.y)
            {
                if (IsLineFull(line))
                {
                    RemoveLine(line);
                    clearLinesCount++;
                }
                else
                {
                    line++;
                }
            }

            return clearLinesCount;
        }

        private bool IsLineFull(int line)
        {
            for (var column = 0; column < Size.x; column++)
            {
                if (IsEmptyTile(column, line))
                {
                    return false;
                }
            }

            return true;
        }

        private void RemoveLine(int line)
        {
            for (var column = 0; column < Size.x; column++)
            {
                RemoveTile(column, line);
            }

            for (var x = 0; x < Size.x; x++)
            {
                for (var y = line; y < Size.y - 1; y++)
                {
                    var aboveTile = GetTile(x, y + 1);
                    SetTile(x, y, aboveTile);

                    if (aboveTile != null)
                    {
                        aboveTile.GameObject.transform.localPosition = GetTilePosition(x, y);
                    }
                }

                SetTile(x, Size.y - 1, null);
            }
        }

        private Tile GetTile(int x, int y)
        {
            return tiles[x * Size.y + y];
        }

        private void SetTile(int x, int y, Tile tile)
        {
            tiles[x * Size.y + y] = tile;
        }

        private bool IsEmptyTile(int x, int y)
        {
            return GetTile(x, y) == null;
        }
    }
}