using UnityEngine;

public class DrunkManager : MonoBehaviour
{
    public float drunkness;

    public void ChangeDrunkness(float drunkChange)
    {
        drunkness = Mathf.Clamp01(drunkness+drunkChange);
        //TODO: Apply drunk level to stencil
    }

    public float GetDrunkness()
    {
        return drunkness;
    }
}
