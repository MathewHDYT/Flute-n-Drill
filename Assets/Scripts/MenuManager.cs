using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Transition transition;
    public Text highscore;

    void Start()
    {
        Cursor.visible = true;
        if (highscore != null)
        {
            highscore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        transition.LoadLevel(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Menu()
    {
        transition.LoadLevel(0);
    }

    public void NextLevel()
    {
        transition.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
