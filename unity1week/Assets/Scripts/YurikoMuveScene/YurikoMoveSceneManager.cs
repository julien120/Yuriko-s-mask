using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YurikoMoveSceneManager : MonoBehaviour
{
    [SerializeField]
    private Yuriko yuriko;

    void Start()
    {
        yuriko.Initialize();
        yuriko.OnGameStart();
    }
}
