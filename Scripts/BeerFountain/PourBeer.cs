using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourBeer : MonoBehaviour
{

    public GameObject beerDrop;
    public Transform spawnPoint;
    public float speed = 20;
    public Ingredient liquid;

    //angle Z from 10 (lever up) to -70 (lever down)
    public Transform LeverTransform;

    private bool canPour = true;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (canPour && (LeverTransform.eulerAngles.z < 360 && LeverTransform.eulerAngles.z >290))
        {
            PourBeerDrop(Mathf.RoundToInt(Mathf.Abs(LeverTransform.eulerAngles.z-360)/10));
            //PourBeerDrop(Mathf.RoundToInt(LeverTransform.eulerAngles.z));
            StartCoroutine(WaitForPour());
        }
    }


    private void PourBeerDrop(int dropsNb)
    {
        for(int i = 0; i < dropsNb; i++) { 
            GameObject newBeerDrop = Instantiate(beerDrop);
            newBeerDrop.transform.position = spawnPoint.position;
            newBeerDrop.GetComponent<LiquidSphere>().liquid = liquid;
            //newBeerDrop.GetComponent<Rigidbody>().velocity = spawnPoint.forward * speed;
            Destroy(newBeerDrop, 1);
        }
    }

   private IEnumerator WaitForPour()
    {
        canPour = false;
        yield return new WaitForSeconds(1);
        canPour = true;
    }
}
