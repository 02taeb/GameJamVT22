using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            if(GameObject.Find("TrackChooser").GetComponent<TrackSwitches>().getLeft())
            {
                other.transform.rotation = new Quaternion(0, 0.981489658f, 0, 0.191515192f);
                
               
            }
            else
            {
                other.transform.rotation = new Quaternion(0, 0.979269803f, 0, -0.202560186f);
                
                
            }
        }
    }
}
