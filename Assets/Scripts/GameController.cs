using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text speed, drain;
    public int minSpeed = 30;
    public int maxSpeed = 100;
    private int iter = 1;
    public double minDrain = 0.01;
    private double currentSpeed = 70.0;
    private double currentDrain = 0.5;
    private bool working;
    private float timer = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!working && Time.timeScale > 0)
            RegulateDrain();
        
        if (currentSpeed.ToString().Contains(","))
            speed.text = currentSpeed.ToString().Substring(0, currentSpeed.ToString().IndexOf(",") + 3);
        else
            speed.text = currentSpeed.ToString();

        drain.text = currentDrain.ToString();

        if (currentSpeed < minSpeed)
            Debug.Log("You Died!");
            //Exit application

        if (timer > 3)
        {
            timer = 0;
            switch (Random.Range(0, 1))
            {
                case 0:
                    GameObject.Find("InputTextField").GetComponent<TextPuzzle>().LoadNextPuzzle();
                    break;

                case 1:
                    // Trigger button puzzle.
                    break;

                // Keep filling with more cases for more puzzles

                default:
                    Debug.Log("Random.Range() > number of puzzle cases in switch");
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (Time.timeScale > 0)
            iter++;
        if (iter % 3 == 0)
            AffectSpeed(-currentDrain / 20);
    }

    public void AffectDrain(double effect)
    {
        if ((currentDrain += effect) < minDrain)
            currentDrain = minDrain;
    }

    public void AffectSpeed(double effect)
    {
        if ((currentSpeed += effect) > maxSpeed)
            currentSpeed = maxSpeed;
        else if (currentSpeed < minSpeed)
            currentSpeed = 0;
    }

    private void RegulateDrain()
    {
        working = true;
        currentDrain = Mathf.Lerp((float)currentDrain, (float)currentDrain + 0.1f, 1);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);

        working = false;
    }
}
