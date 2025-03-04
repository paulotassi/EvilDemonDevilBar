using UnityEngine;

public class Glass : MonoBehaviour, I_Interactable
{
    private DrunkManager dm;
    [SerializeField] private float drunkIncrement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dm == null)
        {
            dm = FindFirstObjectByType<DrunkManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        dm.ChangeDrunkness(drunkIncrement);
    }
}
