using UnityEngine;
using UnityEngine.SceneManagement;

enum Scenes
{
    MenuScene,
    GameScene
}

public class Menu : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene((int)Scenes.GameScene);
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene((int)Scenes.MenuScene);
    }
}
