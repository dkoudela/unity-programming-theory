using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameObject : MonoBehaviour
{
    public static PersistentGameObject singleton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (null == singleton)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
