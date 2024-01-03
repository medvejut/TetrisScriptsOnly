using MyBox;
using UnityEngine;

namespace _Tetris.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private SceneReference mainMenuScene;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mainMenuScene.LoadScene();
            }
        }
    }
}