using UnityEngine;

public enum ingredientType { Liquid, Solid }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Ingredient", order = 1)]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public ingredientType ingredientType; // solid, liquid
    public Color ingredientColor;
}
