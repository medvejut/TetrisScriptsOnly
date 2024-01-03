using _Tetris.Data;
using _Tetris.Gameplay;
using MyBox;
using UnityEngine;

namespace _Tetris.UI
{
    public class StartMenuUI : MonoBehaviour
    {
        [SerializeField] private SceneReference gameplayScene;
        [SerializeField] private SpeedLevelsSettings speedLevelsSettings;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameplayModel.StartNewGame(speedLevelsSettings);
                gameplayScene.LoadScene();
            }
        }
    }
}