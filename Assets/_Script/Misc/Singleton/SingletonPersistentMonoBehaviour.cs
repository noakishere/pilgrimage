using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPersistentMonoBehaviour : SingletonMonoBehaviour<SingletonPersistentMonoBehaviour>
{
    protected override void Awake()
    {
        //if (Instance != null) { Destroy(gameObject); }
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
