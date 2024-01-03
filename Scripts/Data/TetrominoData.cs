using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Tetris.Data
{
    public readonly struct TetrominoData
    {
        private readonly TetrominoType type;

        public readonly Vector2Int[] Cells;

        public TetrominoData(TetrominoType type)
        {
            this.type = type;
            Cells = GetCellsFromType(type);
        }

        private static Vector2Int[] GetCellsFromType(TetrominoType type)
        {
            return type switch
            {
                TetrominoType.I => new[] { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1) },
                TetrominoType.O => new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },
                TetrominoType.T => new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1) },
                TetrominoType.S => new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },
                TetrominoType.Z => new[] { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) },
                TetrominoType.J => new[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },
                TetrominoType.L => new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1) },
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        // wall kicks data from https://tetris.wiki/Super_Rotation_System
        public IEnumerable<Vector2Int> GetWallKicksOffsets(ClockwiseRotationIndex clockwiseRotationIndex)
        {
            return type switch
            {
                TetrominoType.O => new[] { Vector2Int.zero },

                TetrominoType.I => clockwiseRotationIndex switch
                {
                    ClockwiseRotationIndex.Right => new[] { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(+1, 0), new Vector2Int(-2, -1), new Vector2Int(+1, +2) },
                    ClockwiseRotationIndex.Down => new[] { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(+2, 0), new Vector2Int(-1, +2), new Vector2Int(+2, -1) },
                    ClockwiseRotationIndex.Left => new[] { new Vector2Int(0, 0), new Vector2Int(+2, 0), new Vector2Int(-1, 0), new Vector2Int(+2, +1), new Vector2Int(-1, -2) },
                    ClockwiseRotationIndex.Up => new[] { new Vector2Int(0, 0), new Vector2Int(+1, 0), new Vector2Int(-2, 0), new Vector2Int(+1, -2), new Vector2Int(-2, +1) },
                    _ => throw new ArgumentOutOfRangeException()
                },

                _ => clockwiseRotationIndex switch
                {
                    ClockwiseRotationIndex.Right => new[] { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, +1), new Vector2Int(0, -2), new Vector2Int(-1, -2) },
                    ClockwiseRotationIndex.Down => new[] { new Vector2Int(0, 0), new Vector2Int(+1, 0), new Vector2Int(+1, -1), new Vector2Int(0, +2), new Vector2Int(+1, +2) },
                    ClockwiseRotationIndex.Left => new[] { new Vector2Int(0, 0), new Vector2Int(+1, 0), new Vector2Int(+1, +1), new Vector2Int(0, -2), new Vector2Int(+1, -2) },
                    ClockwiseRotationIndex.Up => new[] { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, +2), new Vector2Int(-1, +2) },
                    _ => throw new ArgumentOutOfRangeException()
                }
            };
        }

        public Vector2 GetCellsOffset()
        {
            return type switch
            {
                TetrominoType.I or TetrominoType.O => new Vector2(0.5f, 0.5f),
                _ => Vector2.zero
            };
        }
    }
}