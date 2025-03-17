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
    [SerializeField] private Animator bartenderAnim;
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
        PlayTriggerClip(cupSoundSource, drinkingSound, bartenderAnim);
        drunkness = Mathf.Clamp01(drunkness+drunkChange);
        glasses.color = new Color(glasses.color.r,glasses.color.g,glasses.color.b,drunkness);
        monsterSounds.volume = drunkness;
    }

    public void PlayTriggerClip(AudioSource source, AudioClip clip, Animator anim)
    {
        if (!isPlaying)
        {
            StartCoroutine(PlayAudioCoroutine(source, clip));
            StartCoroutine(PlayAnimCoroutine(anim));
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
    private IEnumerator PlayAnimCoroutine(Animator anim)
    {
        Debug.Log("trying to trigger pouring animation");
        anim.SetTrigger("Drink");
        
        yield return new WaitForSeconds(1.28f);

    }
    public float GetDrunkness()
    {
        return drunkness;
    }
}
