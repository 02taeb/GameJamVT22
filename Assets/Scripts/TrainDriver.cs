using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDriver : MonoBehaviour
{
    
    [SerializeField] double speed = 0f;
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
       
        rb.transform.Translate(0, 0, -(float)speed * Time.deltaTime /10);

        speed = GameObject.Find("GameController").GetComponent<GameController>().getSpeed();
    }
        
}
