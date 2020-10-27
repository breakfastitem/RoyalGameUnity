using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceText : MonoBehaviour{
  public string TotalText;
    // Start is called before the first frame update
    void Start(){
      TotalText="";
    }
    //Updates Text Of Total when called by roller.
    public void UpdateText(int x){
      TotalText= x.ToString();
      // Determines Output In text for Object
      this.GetComponent<Text>().text = "= " + TotalText;
    }
    public void NewTurnText(){
        this.GetComponent<Text>().text = "= ?";
    }
}
