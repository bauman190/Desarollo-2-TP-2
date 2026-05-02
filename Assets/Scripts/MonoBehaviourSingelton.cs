using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            OnAwaken();
        }
        else
            Destroy(gameObject);
    }

    private void Destroy()
    {
        if (Instance == null)
        {
            OnDestroy();
            Instance = null;
        }
    }

    protected virtual void OnAwaken() { }
    protected virtual void OnDestroy() { }
}

