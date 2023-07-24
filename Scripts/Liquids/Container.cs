using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LiquidData
{
    public Ingredient liquid;
    public float amount;
}

public class SolidData
{
    public Ingredient solid;
    public int quantity;
}

public class Container : MonoBehaviour
{

    public float maxLiquidAmount = 1000f;

    public float currentLiquidAmount = 0f;

    public List<LiquidData> liquids = new();

    
    [SerializeField]
    public List<SolidData> solids = new();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddLiquid(Ingredient liquid, float amount)
    {

        // Check if adding this amount of liquid will exceed the maximum capacity of the container
        if (currentLiquidAmount + amount > maxLiquidAmount)
        {
            Debug.LogWarning("Cannot add liquid. Container is full.");
            return;
        }

        // Check if the liquid is already in the container
        foreach (LiquidData liquidData in liquids)
        {
            if (liquidData.liquid == liquid)
            {
                // Increase the amount of the existing liquid
                liquidData.amount += amount;
                currentLiquidAmount += amount;

                return;
            }
        }

        // The liquid is not already in the container, so add it
        liquids.Add(new LiquidData { liquid = liquid, amount = amount });
        currentLiquidAmount += amount;
    }
    public Ingredient RemoveLiquid(float amount)
    {
        // Sort the liquids by amount in descending order
        liquids.Sort((a, b) => b.amount.CompareTo(a.amount));
        LiquidData liquidToRemove = liquids[0];

        if(liquidToRemove == null)
        {
            return null;
        }

        if (liquidToRemove.amount >= amount)
        {
           liquidToRemove.amount -= amount;
           currentLiquidAmount -= amount;
        }
        else
        {
            currentLiquidAmount -= liquidToRemove.amount;
            amount -= liquidToRemove.amount;
            liquidToRemove.amount = 0f;
        }

        // Remove any liquids with a zero amount
        liquids.RemoveAll(liquidData => liquidData.amount == 0f);

        return liquidToRemove.liquid;
    }


    public void RemoveLiquid(Ingredient liquid, float amount)
    {
        // Check if the liquid is already in the container
        foreach (LiquidData liquidData in liquids)
        {
            if (liquidData.liquid == liquid)
            {
                // Decrease the amount of the existing liquid
                if (liquidData.amount >= amount)
                {
                    liquidData.amount -= amount;
                    currentLiquidAmount -= amount;
                }
                else
                {
                    currentLiquidAmount -= liquidData.amount;
                    liquidData.amount = 0f;
                    liquids.Remove(liquidData);
                }

                return;
            }
        }
    }

    public float GetLiquidAmount(Ingredient ingredient)
    {
        foreach (LiquidData ld in liquids)
        {
            if (ld.liquid == ingredient)
            {
                return ld.amount;
            }
        }
        return 0f;
    }
    public int GetSolidAmount(Ingredient ingredient)
    {
        foreach (SolidData sd in solids)
        {
            if (sd.solid == ingredient)
            {
                return sd.quantity;
            }
        }
        return 0;
    }

    public Color GetColorOfMostAmountLiquid()
    {
        float maxAmount = 0f;
        Color maxAmountColor = Color.white;

        foreach (LiquidData liquidData in liquids)
        {
            if (liquidData.amount > maxAmount)
            {
                maxAmount = liquidData.amount;
                maxAmountColor = liquidData.liquid.ingredientColor;
            }
        }

        return maxAmountColor;
    }


    public void AddSolid(Ingredient solid, int quantity)
    {

        // TODO : check if its a solid 

        // Check if the liquid is already in the container
        foreach (SolidData solidData in solids)
        {
            if (solidData.solid == solid)
            {
                // Increase the amount of the existing liquid
                solidData.quantity += quantity;

                return;
            }
        }

        // The liquid is not already in the container, so add it
        solids.Add(new SolidData { solid = solid, quantity = quantity });
    }

    public void RemoveSolid(Ingredient solid, int quantity)
    {
        // Check if the solid is already in the container
        foreach (SolidData solidData in solids)
        {
            if (solidData.solid == solid)
            {
                // Decrease the quantity of the existing solid
                if (solidData.quantity >= quantity)
                {
                    solidData.quantity -= quantity;
                }
                else
                {
                    solidData.quantity = 0;
                    solids.Remove(solidData);
                }

                return;
            }
        }
    }

    public void ResetContainer()
    {
        currentLiquidAmount = 0;
        liquids.Clear();
        solids.Clear();
    }

}
