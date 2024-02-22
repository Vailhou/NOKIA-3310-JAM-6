using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PersistentObject : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}