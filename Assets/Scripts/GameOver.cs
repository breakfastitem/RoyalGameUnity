using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    string blueWonText;
    string redWonText;
    // Start is called before the first frame update
    void Start()
    {
      redWonText="Red Wins!!" ;
      blueWonText="Blue Wins!!"; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Displays Large Text of game result
    public void DetermineWinner(bool redWon){
      if(redWon){
      this.GetComponent<Text>().text  =redWonText;
      this.GetComponent<Text>().color = Color.red;
      }else{
        this.GetComponent<Text>().text = blueWonText;
        this.GetComponent<Text>().color = Color.blue;
      }

    }
}
