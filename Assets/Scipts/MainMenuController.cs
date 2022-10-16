using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayBackGroundSound(SoundTypes.StartMenuBG);
    }
    public void playSinglePlayerScene()
    {
        AudioManager.Instance.PlayEffectSound(SoundTypes.ButtonPressed);
        SceneManager.LoadScene("SinglePlayerScene");
    }
    public void playCoopPlayerScene()
    {
        AudioManager.Instance.PlayEffectSound(SoundTypes.ButtonPressed);
        SceneManager.LoadScene("CoopPlayerScene");
    }
    public void QuitGame()
    {
        AudioManager.Instance.PlayEffectSound(SoundTypes.ButtonPressed);
        Application.Quit();
    }
}
