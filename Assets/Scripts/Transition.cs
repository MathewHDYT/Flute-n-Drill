using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator transition;
    public float transitiontime = 1f;

    public void LoadLevel(int levelindex)
    {
        StartCoroutine(LoadLevelEnum(levelindex));
    }

    IEnumerator LoadLevelEnum(int level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(level);
    }
}
