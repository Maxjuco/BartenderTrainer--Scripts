using UnityEngine;

public class LiquidSphere : MonoBehaviour
{
    public Ingredient liquid;

    public float volume;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LiquidContainer" && gameObject.transform.parent != collision.transform && gameObject.transform.parent != collision.transform.parent)
        {
            collision.gameObject.GetComponentInParent<Container>().AddLiquid(liquid, volume);
            // collision.gameObject.getCompenant<LiquidDContainer>.AddLiquid(liquid, 2.0f);
            GameObject.Destroy(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "LiquidContainer" && gameObject.transform.parent == collision.transform)
        {
            //collision.gameObject.GetComponent<liquidDisplay>().subLiquid();
            // liquid = collision.gameObject.getCompenant<LiquidDContainer>.RemoveLiquid(2.0f);
        }
    }
}
