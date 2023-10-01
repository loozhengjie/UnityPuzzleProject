using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderBoardDisplay : MonoBehaviour
{
    private TextMeshProUGUI ranktext;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI difficultyText;
    private TextMeshProUGUI scoreText;


    private void Awake()
    {
        ranktext = this.transform.Find("Rank").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        nameText = this.transform.Find("NameTitle").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        difficultyText = this.transform.Find("DifficultyTitle").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        scoreText = this.transform.Find("PointTitle").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void DisplayHighScore(GameData data, int rank)
    {
        if (data == null)
        {
           
        }
        else
        {
            ranktext.text = string.Format("{00:00}", rank.ToString());  
            nameText.text = data.playerName;
            difficultyText.text = data.levelDifficulty.ToString();
            scoreText.text = string.Format("{0:000000}", data.puzzlingPoints.ToString());
        }
    }
    public void HideEntryDisplay()
    {
        ranktext.text = "";
        nameText.text = "";
        difficultyText.text = "";
        scoreText.text = "";
    }
}
