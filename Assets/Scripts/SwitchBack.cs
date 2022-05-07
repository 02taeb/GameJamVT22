using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBack : MonoBehaviour
{
    private Vector3 trainPos = new Vector3(-0.0199999996f, 0.400000006f, 9.22869968f);
    private Quaternion startQuaternion = new Quaternion(0, 0.999999881f, 0, 0.000582541281f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            other.transform.position = trainPos;
            other.transform.rotation = startQuaternion;
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
