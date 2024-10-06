using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(string audioName, float volume = 1)
    {
        AudioSource audioSource = null;

        switch (audioName)
        {
            case "hit1":
                if (PrefabManager.Instance != null)
                    audioSource = Instantiate(PrefabManager.Instance.hitSound1).GetComponent<AudioSource>();
                break;
            case "hit2":
                if (PrefabManager.Instance != null)
                    audioSource = Instantiate(PrefabManager.Instance.hitSound2).GetComponent<AudioSource>();
                break;
            case "shoot1":
                if (PrefabManager.Instance != null)
                    audioSource = Instantiate(PrefabManager.Instance.shootSound1).GetComponent<AudioSource>();
                break;
            case "death1":
                if (PrefabManager.Instance != null)
                    audioSource = Instantiate(PrefabManager.Instance.deathSound1).GetComponent<AudioSource>();
                break;
        }

        if (audioSource != null)
            audioSource.volume = volume;
    }
}