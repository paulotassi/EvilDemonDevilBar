using UnityEngine;

public class DrunkManager : MonoBehaviour
{
    public float drunkness;
    [SerializeField] private Material glasses;

    public void ChangeDrunkness(float drunkChange)
    {
        drunkness = Mathf.Clamp01(drunkness+drunkChange);
        glasses.color = new Color(glasses.color.r,glasses.color.g,glasses.color.b,drunkness);
    }

    public float GetDrunkness()
    {
        return drunkness;
    }
}
