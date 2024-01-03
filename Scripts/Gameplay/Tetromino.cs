using System.Collections.Generic;
using System.Linq;
using _Tetris.Data;
using UnityEngine;

namespace _Tetris.Gameplay
{
    public class Tetromino
    {
        private readonly TetrominoData data;

        public Vector2Int Position;

        private ClockwiseRotationIndex RotationIndex { get; set; }
        public Vector2Int[] Cells { get; private set; }

        public Tetromino(TetrominoData data)
        {
            this.data = data;
            Cells = data.Cells;
        }

        public void Rotate(bool clockwise)
        {
            var angle = clockwise ? -Mathf.PI / 2 : Mathf.PI / 2;
            Cells = Cells.Select(cell => RotatePoint(cell, data.GetCellsOffset(), angle)).ToArray();
            RotationIndex = (ClockwiseRotationIndex)(((int)RotationIndex + (clockwise ? 1 : 3)) % 4);
        }

        private static Vector2Int RotatePoint(Vector2Int point, Vector2 origin, float angle)
        {
            var diff = point - origin;
            var x = origin.x + diff.x * Mathf.Cos(angle) - diff.y * Mathf.Sin(angle);
            var y = origin.y + diff.x * Mathf.Sin(angle) + diff.y * Mathf.Cos(angle);
            return new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
        }

        public IEnumerable<Vector2Int> GetWallKicksOffsets()
        {
            return data.GetWallKicksOffsets(RotationIndex);
        }
    }
}