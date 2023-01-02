using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTestManager : MonoBehaviour
{
    public static GameTestManager instance;
    public GameObject currentPlatform;
    public GameObject stair;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
