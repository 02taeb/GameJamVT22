using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCollider : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Train"))
        {
            audioSource.PlayOneShot(audioClip);
            meshRenderer.enabled = false;
            StartCoroutine (KillWait());
        }
    }

    IEnumerator KillWait()
    {
        yield return new WaitForSeconds(3f);
        meshRenderer.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
