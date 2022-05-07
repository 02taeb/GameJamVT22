using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text speed;
    public int minSpeed = 30;
    public int maxSpeed = 100;
    public double minDrain = 0.01;
    private double currentSpeed = 50.0;
    private double currentDrain = 0.5;
    private double modifier = 0.01;
    private System.Random rnd = new System.Random();

    void Start()
    {
        
    }

    void Update()
    {
        RegulateDrain();
        
        if (currentSpeed.ToString().Contains("."))
            speed.text = currentSpeed.ToString().Substring(0, currentSpeed.ToString().IndexOf(","));
        else
            speed.text = currentSpeed.ToString();
        
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
        AffectDrain(Time.deltaTime / (1 / (currentDrain * modifier)));
    }
}
