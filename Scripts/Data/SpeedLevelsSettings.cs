using System;
using UnityEngine;

namespace _Tetris.Data
{
    [CreateAssetMenu(fileName = "Speed Levels Settings", menuName = "Tetris/Speed Levels Settings", order = 0)]
    public class SpeedLevelsSettings : ScriptableObject
    {
        [Serializable]
        public struct SpeedLevelData
        {
            public float FallInterval;
            public int ClearLinesCount;
        }

        public SpeedLevelData[] SpeedLevels = { new() { ClearLinesCount = 0, FallInterval = 1f } };
    }
}