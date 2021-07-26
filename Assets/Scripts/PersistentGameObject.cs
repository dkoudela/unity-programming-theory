using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameObject : MonoBehaviour
{
    // ENCAPSULATION
    public static PersistentGameObject Singleton { get; private set; }

    private void Awake()
    {
        if (null != Singleton)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }
}
