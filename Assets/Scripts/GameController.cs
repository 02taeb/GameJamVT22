using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text speed, drain;
    public int minSpeed = 30;
    public int maxSpeed = 100;
    public double minDrain = 0.01;
    private double currentSpeed = 70.0;
    private double currentDrain = 0.5;
    private bool working;

    void Start()
    {
        
    }

    void Update()
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
    }

    private void FixedUpdate()
    {
        if(Time.timeScale > 0)
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
