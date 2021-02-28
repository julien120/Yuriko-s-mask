using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Result
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text endText;

        void Start()
        {
            //TODO:InGameSceneで管理してるスコアデータをこっちに持ってくる。
            // scoreText.text = +"個";
            EndText();
        }

        public void OnClickRetryButton()
        {
            //TODO:誰かが作ってるsceneControllerマネージャーを用いてロードシーン
        }

        private void EndText()
        {
            int score = 11;
            if (score < 10)
            {
                endText.text = "小池帝国は凋落した！！";
            }
            else
            {
                endText.text = "都民も小池施政下による安寧を喜んでいる！！！";

            }
        }
    }
}