using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YurikoMoveSceneView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void Initialize()
    {

    }

    public void OnScoreChanged(int score)
    {
        scoreText.text = $"回収した数:{score}";
    }
}
