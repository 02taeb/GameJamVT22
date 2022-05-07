using System;
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

    void Start()
    {
        
    }

    void Update()
    {
        RegulateDrain();
        
        if (currentSpeed.ToString().Contains(","))
            speed.text = currentSpeed.ToString().Substring(0, currentSpeed.ToString().IndexOf(",") + 3);
        else
            speed.text = currentSpeed.ToString();

        drain.text = currentDrain.ToString().Substring(0, currentDrain.ToString().IndexOf(",") + 3);

        if (currentSpeed < minSpeed)
            Debug.Log("You Died!");
            //Exit application
    }

    private void FixedUpdate()
    {
        AffectSpeed(-currentDrain / 10);
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
        AffectDrain((-Math.Log(currentDrain) + 4) / 1000);
    }
}
