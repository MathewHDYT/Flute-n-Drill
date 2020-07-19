using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popups;
    private int popUpIndex = 0;
    private float waitTime = .5f;

    FluteSay fs;

    void Start()
    {
        fs = FluteSay.instance;
    }

    void Update()
    {
        popups[popUpIndex].SetActive(true);

        if (popUpIndex == 0)
        {
            if (fs.grabeddrill)
            {
                popups[popUpIndex].SetActive(false);
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (waitTime <= 0)
            {
                fs.StartGame();

                if (fs.drilled)
                {
                    popups[popUpIndex].SetActive(false);
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
