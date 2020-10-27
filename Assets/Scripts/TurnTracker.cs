using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTracker : MonoBehaviour
{
    string redTurnText;
    string blueTurnText;
    // Start is called before the first frame update
    void Start(){
      redTurnText = "Current Turn: Red";
      blueTurnText= "Current Turn: Blue";
    }

    // Update is called once per frame
    void Update(){

    }
    public void SetTurnText(bool redTurn){
      if(redTurn){
      this.GetComponent<Text>().text  =redTurnText;
      this.GetComponent<Text>().color = Color.red;
      }else{
        this.GetComponent<Text>().text = blueTurnText;
        this.GetComponent<Text>().color = Color.blue;

      }

    }
}
