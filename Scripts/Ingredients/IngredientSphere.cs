using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSphere : MonoBehaviour
{
    public Ingredient ingredient;

    public int quantity;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LiquidContainer" && gameObject.transform.parent != collision.transform && gameObject.transform.parent != collision.transform.parent)
        {
            collision.gameObject.GetComponentInParent<Container>().AddSolid(ingredient, quantity);
            GameObject.Destroy(gameObject);
        }
    }
}
