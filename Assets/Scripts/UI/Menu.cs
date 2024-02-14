using UnityEngine;
using UnityEngine.SceneManagement;

enum Scenes
{
    MenuScene,
    GameScene
}

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene((int)Scenes.GameScene);
    }
}
