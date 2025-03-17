using System.Collections;
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
    [SerializeField] private bool isPlaying = false;
    private void Start() 
    {
        glasses.color = new Color(glasses.color.r,glasses.color.g,glasses.color.b,drunkness);
    }
    public void ChangeDrunkness(float drunkChange)
    {
        PlayAudioClip(cupSoundSource, drinkingSound);
        drunkness = Mathf.Clamp01(drunkness+drunkChange);
        glasses.color = new Color(glasses.color.r,glasses.color.g,glasses.color.b,drunkness);
        monsterSounds.volume = drunkness;
    }

    public void PlayAudioClip(AudioSource source, AudioClip clip)
    {
        if (!isPlaying)
        {
            StartCoroutine(PlayAudioCoroutine(source, clip));
        }
    }
    private IEnumerator PlayAudioCoroutine(AudioSource source, AudioClip clip)
    {
        isPlaying = true;
        source.clip = clip;
        source.Play();

        yield return new WaitForSeconds(clip.length);

        isPlaying = false;
    }
    public float GetDrunkness()
    {
        return drunkness;
    }
}
