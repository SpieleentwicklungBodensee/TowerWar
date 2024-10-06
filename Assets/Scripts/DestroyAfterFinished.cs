using UnityEngine;

public class DestroyAfterFinished : MonoBehaviour
{
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
            Destroy(gameObject);
    }
}