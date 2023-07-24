using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerDropZone : MonoBehaviour
{
    public string collisionTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == collisionTag)
        {
            Debug.LogWarning("Beer drop hit a glass");
            Destroy(collision.collider.gameObject);
            //TODO: Add function to fill the glass
        }
    }
}
