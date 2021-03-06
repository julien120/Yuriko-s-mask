using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class YurikoMoveSceneManager : MonoBehaviour
{
    [SerializeField]
    private Yuriko yuriko;

    [SerializeField]
    private YurikoMoveSceneView yurikoMoveSceneView;

    void Start()
    {
        yuriko.Initialize();

        yuriko.OnScoreChanged
            .Subscribe(score => OnScoreChanged(score));

        yuriko.OnGameStart();
    }

    private void OnScoreChanged(int score)
    {
        yurikoMoveSceneView.OnScoreChanged(score);
    }
}
