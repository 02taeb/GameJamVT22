using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameController : MonoBehaviour
{
    public Text speed, drain;
    public GameObject deathMsg;
    public int minSpeed = 30;
    public int maxSpeed = 100;
    private int iter = 1;
    public double minDrain = 0.01;
    private double currentSpeed = 70.0;
    private double currentDrain = 0.5;
    private bool working;
    private bool call;
    private VideoPlayer vp;

    private void Start()
    {
        Time.timeScale = 0;
        vp = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        vp.Play();
        vp.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (vp.isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            vp.Stop();
        }

        if (Time.timeScale != 0)
        {
            if (!working && Time.timeScale > 0)
                RegulateDrain();

            if (currentSpeed.ToString().Contains(","))
                speed.text = currentSpeed.ToString().Substring(0, currentSpeed.ToString().IndexOf(",") + 3);
            else
                speed.text = currentSpeed.ToString();

            drain.text = currentDrain.ToString();

            if (currentSpeed < minSpeed)
            {
                if (!call)
                {
                    StartCoroutine(ReloadSceneTimer());
                    deathMsg.SetActive(true);
                }
            }
        }
    }

    IEnumerator ReloadSceneTimer()
    {
        call = true;
        
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            iter++;
            if (iter % 4 == 0)
                AffectSpeed(-currentDrain / 20);
        }
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
