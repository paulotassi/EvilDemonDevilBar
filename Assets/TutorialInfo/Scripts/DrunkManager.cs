using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DrunkManager : MonoBehaviour
{
    public float drunkness;
    [SerializeField] private Material glasses;
    [SerializeField] private Material glassesShade;
    [SerializeField] private float maxShade;
    [SerializeField] private float minShade;
    private void Start() 
    {
        glasses.color = new Color(glasses.color.r,glasses.color.g,glasses.color.b,drunkness);
        glassesShade.color = new Color(glassesShade.color.r,glassesShade.color.g,glassesShade.color.b,Mathf.Clamp(drunkness*maxShade,minShade,maxShade));
    }
    public void ChangeDrunkness(float drunkChange)
    {
        drunkness = Mathf.Clamp01(drunkness+drunkChange);
        glasses.color = new Color(glasses.color.r,glasses.color.g,glasses.color.b,drunkness);
        glassesShade.color = new Color(glassesShade.color.r,glassesShade.color.g,glassesShade.color.b,Mathf.Clamp(drunkness*maxShade,minShade,maxShade));
    }

    public float GetDrunkness()
    {
        return drunkness;
    }
}
