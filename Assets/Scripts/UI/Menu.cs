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
        Audio.Instance.MenuSelect.Play();
        SceneManager.LoadScene((int)Scenes.GameScene);
    }

    public static void LoadMenu()
    {
        Audio.Instance.MenuSelect.Play();
        SceneManager.LoadScene((int)Scenes.MenuScene);
    }

    public static void LoadBuilder()
    {
        Audio.Instance.MenuSelect.Play();
        SceneManager.LoadScene((int)Scenes.BuildScene);
    }

    public static void LoadUpgrades()
    {
        Audio.Instance.MenuSelect.Play();
        SceneManager.LoadScene((int)Scenes.UpgradeScene);
    }

    public static void Terminate()
    {
        Application.Quit();
    }
}
