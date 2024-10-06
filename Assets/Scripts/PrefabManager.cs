using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;

    public GameObject shootSound1;
    public GameObject hitSound1;
    public GameObject hitSound2;

    private void Awake()
    {
        Instance = this;
    }
}