using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FluteSay : MonoBehaviour
{
    #region Singelton
    public static FluteSay instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Instance of Inventory found");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject[] colours;
    public Transition transition;
    public Text score;
    int currentscore = 0;

    private List<int> activated;
    private List<int> playerdrilled;

    private int count = 1;

    System.Random rg;

    public bool inputEnabled = false;
    public bool[] holedrilled;
    public bool grabeddrill = false;
    public bool drilled = false;
    private int random;
    private bool started = false;

    private void SetHighScore()
    {
        if (currentscore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentscore);
        }
    }

    private void IncreaseScore()
    {
        currentscore += 1;
        score.text = currentscore.ToString();
    }

    private void Start()
    {
        random = RandomString(8).GetHashCode();
    }

    public void StartGame()
    {
        if (started)
        {
            return;
        }

        started = true;
        StartCoroutine(FluteSays());
    }

    private void PlayAudio(int index)
    {
        FindObjectOfType<AudioManager>().Play("Hole" + index);
    }

    public void OnDrill(int index)
    {
        PlayNote(index);

        playerdrilled.Add(index);

        if (activated[playerdrilled.Count - 1] != index)
        {
            GameOver();
            return;
        }

        if (activated.Count == playerdrilled.Count)
        {
            IncreaseScore();

            StartCoroutine(FluteSays());
        }
    }

    public void GameOver()
    {
        inputEnabled = false;

        SetHighScore();

        transition.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator FluteSays()
    {
        inputEnabled = false;

        rg = new System.Random(random);

        SetNotes();

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < activated.Count; i++)
        {
            PlayNote(activated[i]);

            yield return new WaitForSeconds(0.6f);
        }

        inputEnabled = true;

        yield return null;
    }

    private void PlayNote(int index)
    {
        colours[index].GetComponentInChildren<ParticleSystem>().Play();
        PlayAudio(index);
    }

    private void SetNotes()
    {
        activated = new List<int>();
        playerdrilled = new List<int>();

        for (int i = 0; i < count; i++)
        {
            activated.Add(rg.Next(0, colours.Length));
        }

        count++;
    }

    private string RandomString(int length)
    {
        System.Random r = new System.Random();
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Range(1, length).Select(_ => chars[r.Next(chars.Length)]).ToArray());
    }
}
