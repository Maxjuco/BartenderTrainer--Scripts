using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IngredientData
{
    public Ingredient ingredient;
    public float quantity;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Recette", order = 1)]
public class Recette : ScriptableObject
{
    public string recetteName;
    public List<IngredientData> listIngredient;
}
