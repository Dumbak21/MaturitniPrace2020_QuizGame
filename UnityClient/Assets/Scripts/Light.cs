using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Singleton Lightning for all scenes - solved bugs with random shadows
public class Light : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private static Light _ins;
    public static Light Instance { get { return _ins; } set { _ins = value; } }

    void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
