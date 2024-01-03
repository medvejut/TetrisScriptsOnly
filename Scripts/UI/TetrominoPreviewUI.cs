using System;
using _Tetris.Data;
using UnityEngine;
using UnityEngine.UI;

namespace _Tetris.UI
{
    public class TetrominoPreviewUI : MonoBehaviour
    {
        public void SetTetromino(TetrominoData tetromino)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            var tileSize = GetComponent<RectTransform>().sizeDelta.x / 4;
            foreach (var cell in tetromino.Cells ?? Array.Empty<Vector2Int>())
            {
                var tile = new GameObject().AddComponent<Image>().rectTransform;
                tile.sizeDelta = new Vector2(tileSize, tileSize);
                tile.anchoredPosition = new Vector2(cell.x - 0.5f, cell.y - 0.5f) * tileSize;
                tile.SetParent(transform, false);
            }
        }
    }
}