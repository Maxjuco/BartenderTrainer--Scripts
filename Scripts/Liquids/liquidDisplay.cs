using UnityEngine;

public class liquidDisplay : MonoBehaviour
{

    public GameObject liquidMesh;
    //public GameObject contenant;
    //public GameObject sphere;

    public float fillAmount;

    public float minContenance;
    public float maxContenance;

    public MeshRenderer LiquidMeshRend;

    public Container container;

    // Start is called before the first frame update
    void Start()
    {
        LiquidMeshRend = liquidMesh.GetComponent<MeshRenderer>();


        //LiquidMeshRend.material.SetFloat("_FillAmount", fillAmount);
    }

    // Update is called once per frame
    void Update()
    {
        fillAmount = minContenance - ((container.currentLiquidAmount / container.maxLiquidAmount) * (minContenance - maxContenance));
        LiquidMeshRend.material.SetFloat("_FillAmount", fillAmount);
        Color color = container.GetColorOfMostAmountLiquid();
        LiquidMeshRend.material.SetColor("_Tint", color);
        LiquidMeshRend.material.SetColor("_TopColor", color);
    }
}
