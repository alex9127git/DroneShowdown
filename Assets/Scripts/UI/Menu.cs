using UnityEngine;
using UnityEngine.SceneManagement;

enum Scenes
{
    MenuScene,
    GameScene,
    BuildScene
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

    public static void LoadBuilder()
    {
        SceneManager.LoadScene((int)Scenes.BuildScene);
    }
}
