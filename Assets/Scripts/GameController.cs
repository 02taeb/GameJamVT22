using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text speed;
    public int minSpeed = 30;
    private double currentSpeed = 50.0;
    private double currentDrain = 1.0;
    private double stopWatch = 0.0;

    void Start()
    {
        
    }

    void Update()
    {
        stopWatch += Time.deltaTime;
        RegulateDrain();
        if (stopWatch % 1 == 0)
            currentSpeed -= currentDrain;
        if (currentSpeed < minSpeed)
            Debug.Log("You Died!");
    }

    public void AffectDrain(double effect)
    {
        currentDrain += effect;
    }

    public void AffectSpeed(double effect)
    {
        currentSpeed += effect;
    }

    private void RegulateDrain()
    {
        // Don't let the drain get too small, shouldn't be too easy to survive.
        // Slowly tick up the drain
            // AffectDrain(Time.deltaTime / ((1 / currentDrain) * someModifier)); //perhaps, where someModifier keeps it in check.
    }
}
