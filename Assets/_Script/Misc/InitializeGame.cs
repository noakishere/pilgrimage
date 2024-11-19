using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    [Tooltip("Order important")]
    [SerializeField] private List<GameObject> gameObjectsToEnable;
    void Awake()
    {
        EnableGameObjects();
    }

    private void EnableGameObjects()
    {
        foreach (GameObject gameObject in gameObjectsToEnable)
        {
            gameObject.SetActive(true);
        }
    }
}
