using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public string scoreText;
   
    // Start is called before the first frame update
    void Start()
    {
        scoreText="6";
    }
    public void updateScore(int x){
        scoreText = x.ToString();

        this.GetComponent<Text>().text=scoreText;
    }
}
