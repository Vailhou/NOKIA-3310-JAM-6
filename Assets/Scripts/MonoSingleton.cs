using UnityEngine;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
{
    public static T Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(this);
        }
        else
        {
            Instance = (T)this;
            OnAwake();
        }
    }

    protected virtual void OnAwake() { }
}