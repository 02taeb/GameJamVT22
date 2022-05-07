using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerLights : MonoBehaviour
{
    public Image[] lights;
    private bool flicker;
    
    void Update()
    {
        if (!flicker)
        {
            flicker = true;

            Flicker();
        }
    }

    void Flicker()
    {
        bool on = false;
        int index = Random.Range(0, lights.Length);
        if (Random.Range(0, 2) == 1)
            on = true;
        if (on)
            lights[index].enabled = true;
        else
            lights[index].enabled = false;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);

        flicker = false;
    }
}
