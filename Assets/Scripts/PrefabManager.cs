using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;

    public GameObject shootSound1;
    public GameObject hitSound1;
    public GameObject hitSound2;
    public GameObject deathSound1;

    private void Awake()
    {
        Instance = this;
    }
}