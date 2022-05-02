using UnityEngine;

public abstract class Singleton : MonoBehaviour
{
}

public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    var obj = new GameObject();
                    obj.name = typeof(T).Name;
                    obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        instance = this as T;
        DontDestroyOnLoad(this);
    }

    public static void Destroy()
    {
        if (instance == null)
        {
            return;
        }

        Destroy(instance.gameObject);
        instance = null;
    }
}