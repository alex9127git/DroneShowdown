using UnityEngine;
using UnityEngine.SceneManagement;

enum Scenes
{
    MenuScene,
    GameScene,
    BuildScene,
    UpgradeScene
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

    public static void LoadUpgrades()
    {
        SceneManager.LoadScene((int)Scenes.UpgradeScene);
    }
}
