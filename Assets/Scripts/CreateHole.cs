using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHole : MonoBehaviour
{
    public Transform changeyscale;
    private float ypos;
    public ParticleSystem woodchips;
    private float change;
    private bool diddrilling = false;
    public int buttonnumber = 0;

    FluteSay fs;

    private void Start()
    {
        fs = FluteSay.instance;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("Drill") || !fs.grabeddrill)
        {
            woodchips.Stop();
            return;
        }
        woodchips.Play();
        ypos = col.transform.position.y;
        diddrilling = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Drill"))
        {
            return;
        }
        woodchips.Stop();
    }

    private void Update()
    {
        if (!diddrilling)
            return;

        change = -ypos + 2.2f;
        if (change > changeyscale.localScale.y)
        {
            ChangeyScale(change);
        }
    }

    public void ResetHole()
    {
        fs.holedrilled = new bool[10];
        ChangeyScale(0f);
    }

    IEnumerator StartReseting()
    {
        yield return new WaitForSeconds(1f);
        ResetHole();
    }

    private void ChangeyScale(float yscale)
    {
        changeyscale.localScale = new Vector3(changeyscale.localScale.x, yscale, changeyscale.localScale.z);

        if (changeyscale.localScale.y >= 0.4f && !fs.holedrilled[buttonnumber])
        {
            fs.holedrilled[buttonnumber] = true;
            fs.drilled = true;

            if (fs.inputEnabled)
            {
                fs.OnDrill(buttonnumber);
                StartCoroutine(StartReseting());
            }
        }
        if (changeyscale.localScale.y >= 0.9f)
        {
            fs.GameOver();
        }
    }
}
