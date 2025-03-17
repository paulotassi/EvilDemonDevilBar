using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DrunkManager : MonoBehaviour
{
    public float drunkness;
    [SerializeField] private Material glasses;
    [SerializeField] private Material glassesShade;
    [SerializeField] private float maxShade;
    [SerializeField] private float minShade;
    [SerializeField] private AudioSource monsterSounds;
    [SerializeField] private AudioSource cupSoundSource;
    [SerializeField] private AudioClip drinkingSound;
    private void Start() 
    {
        glasses.color = new Color(glasses.color.r,glasses.color.g,glasses.color.b,drunkness);
    }
    public void ChangeDrunkness(float drunkChange)
    {
        cupSoundSource.PlayOneShot(drinkingSound);
        drunkness = Mathf.Clamp01(drunkness+drunkChange);
        glasses.color = new Color(glasses.color.r,glasses.color.g,glasses.color.b,drunkness);
        monsterSounds.volume = drunkness;
    }

    public float GetDrunkness()
    {
        return drunkness;
    }
}
