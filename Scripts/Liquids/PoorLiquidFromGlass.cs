using System.Collections;
using UnityEngine;

public class PoorLiquidFromGlass : MonoBehaviour
{
    public Wobble Wobble;
    public GameObject liquidDrop;
    public Transform spawnPoint;
    public Container container;
    bool canPour = true;
    private float timeBetweenPoor = 0.5f;


    private void FixedUpdate()
    {
        if (canPour && Wobble.lastRot.z < 300 && Wobble.lastRot.z > 100 && container.currentLiquidAmount > 0)
        {
            //start coroutine to wait next pooring:
            StartCoroutine(WaitForPour());
            //poor the liquid : 
            GameObject newLiquidDrop = Instantiate(liquidDrop);
            newLiquidDrop.transform.position = spawnPoint.position;
            newLiquidDrop.transform.parent = gameObject.transform;
            newLiquidDrop.GetComponent<LiquidSphere>().liquid = container.RemoveLiquid(newLiquidDrop.GetComponent<LiquidSphere>().volume);
            Destroy(newLiquidDrop, 1);
        }
    }

    private IEnumerator WaitForPour()
    {
        canPour = false;
        yield return new WaitForSeconds(timeBetweenPoor);
        canPour = true;
    }
}
