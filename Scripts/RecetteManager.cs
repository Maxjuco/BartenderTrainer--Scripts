using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RecetteManager : MonoBehaviour
{
    // affichage
    public List<GameObject> listPanel;
    public List<TMP_Text> listeIngredientMojito;
    public List<TMP_Text> listeIngredientPinaColada;
    public List<TMP_Text> listeIngredientSexOnTheBeach;
    public TMP_Text amountOfBeer;

    public TMP_Text scoreText;

    private int currentPanel;
    private int score;
    [SerializeField] private List<Recette> listRecettes;
    private List<bool> isAddList; 
    [SerializeField] private Container container;
    private Recette currentRecette;
    // ajouter un verre à remplir

    // Start is called before the first frame update
    void Start()
    {
        currentPanel = 0;
        score = 0;
        currentRecette = listRecettes[currentPanel];
        isAddList = new List<bool>(new bool[currentRecette.listIngredient.Count]);
    }

    // Update is called once per frame
    void Update()
    {
        bool isDone = IsRecetteCompleted(currentRecette, container);

        if (isDone && (currentPanel < listPanel.Count) && currentRecette != listRecettes.Last())
        {
            listPanel[currentPanel].SetActive(false);
            currentPanel++;
            score += 100;
            calculateScore();
            currentRecette = listRecettes[currentPanel];
            scoreText.text = score.ToString();
            listPanel[currentPanel].SetActive(true);
            isAddList = new List<bool>(new bool[currentRecette.listIngredient.Count]);
            container.ResetContainer();
        }
    }

    private bool IsRecetteCompleted(Recette currentRecette, Container LiquidContainer)
    {

        // TODO : Ecouter les évenements de realisation de la composition et marquer le text en vert
        foreach(IngredientData ingredientData in currentRecette.listIngredient)
        {
            if(ingredientData.ingredient.ingredientType == ingredientType.Liquid)
            {
                int quantity = (int)LiquidContainer.GetLiquidAmount(ingredientData.ingredient);
                setText(currentRecette.name, ingredientData, quantity);
                // TODO : Vérifier la quantité de liquid
                if (ingredientData.quantity <= quantity)
                {
                    // Set la color du text quand c'est fait
                    setTextColor(currentRecette.name, ingredientData, Color.green);
                    isAddList[getIngredientIndex(ingredientData)] = true;
                }
                else
                {
                    isAddList[getIngredientIndex(ingredientData)] = false;
                }
            }
            else // cas solid
            {
                int quantity = LiquidContainer.GetSolidAmount(ingredientData.ingredient);
                setText(currentRecette.name, ingredientData, quantity);
                // TODO : Verifier le colider entre les objets et le verre
                if (ingredientData.quantity <= quantity)
                {
                    // Set la color du text quand c'est fait
                    setTextColor(currentRecette.name, ingredientData, Color.green);
                    isAddList[getIngredientIndex(ingredientData)] = true;
                }
                else
                {
                    isAddList[getIngredientIndex(ingredientData)] = false;
                }
            }
        }

        int count = 0;

        foreach(bool isadd in isAddList) {
            if (isadd == true)
            {
                count++;
            }
        }

        return count == isAddList.Count;
    }

    private void setTextColor(string recetteName, IngredientData ingredientData, Color color)
    {
        int ingredientIndex = getIngredientIndex(ingredientData);
        switch (recetteName)
        {
            case "Mojito":
                listeIngredientMojito[ingredientIndex].color = color;
                break;
            case "Pina Colada":
                listeIngredientPinaColada[ingredientIndex].color = color;
                break;
            case "Sex On The Beach":
                listeIngredientSexOnTheBeach[ingredientIndex].color = color;
                break;
            case "Pinte de biere":
                amountOfBeer.color = color;
                break;
            default: break;
        }
    }     
    
    private void setText(string recetteName, IngredientData ingredientData, int quantity)
    {
        int ingredientIndex = getIngredientIndex(ingredientData);
        switch (recetteName)
        {
            case "Mojito":
                listeIngredientMojito[ingredientIndex].text = quantity.ToString();
                break;
            case "Pina Colada":
                listeIngredientPinaColada[ingredientIndex].text = quantity.ToString();
                break;
            case "Sex On The Beach":
                listeIngredientSexOnTheBeach[ingredientIndex].text = quantity.ToString();
                break;
            case "Pinte de biere":
                amountOfBeer.text = quantity.ToString();
                break;
            default: break;
        }
    } 

    private IngredientData isInListIngredient(List<IngredientData> listIngredients, Ingredient ingredient) {

        foreach(IngredientData id in listIngredients)
        {
            if (id.ingredient == ingredient)
            {
                return id;
            }
        }

        return null;
    }

    private void decreaseScore(int penality)
    {
        if (score > penality)
        {
            score -= penality;
        } else
        {
            score = 0;
        }
    }

    private void calculateScore()
    {
        // pénalité de solid et liquid qui ne sont pas dans la recette
        foreach (SolidData solidData in container.solids)
        {
            IngredientData tempIngr = isInListIngredient(currentRecette.listIngredient, solidData.solid);
            if (tempIngr == null)
            {
                decreaseScore(solidData.quantity * 10);
            } else if (solidData.quantity > tempIngr.quantity)
            {
                setTextColor(currentRecette.name, tempIngr, Color.red);
                decreaseScore(10 * (solidData.quantity - (int)tempIngr.quantity));
            }
        }

        foreach (LiquidData liquidData in container.liquids)
        {
            IngredientData tempIngr = isInListIngredient(currentRecette.listIngredient, liquidData.liquid);
            if (tempIngr == null)
            {
                decreaseScore((int)liquidData.amount);
            }
            else if (liquidData.amount + 2.0 > tempIngr.quantity)
            {
                setTextColor(currentRecette.name, tempIngr, Color.red);
                decreaseScore((int)liquidData.amount - (int)tempIngr.quantity);
            }
        }
    }

    private int getIngredientIndex(IngredientData ingredientData)
    {
        return currentRecette.listIngredient.IndexOf(ingredientData);
    }
}
