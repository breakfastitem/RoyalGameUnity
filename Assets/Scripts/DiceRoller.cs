using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour {

  //Audio capabilities
  AudioManager audioPlayer;
  public int[] Rolls;
  public int DiceTotal;

  public Sprite[] DiceImageOne;
  public Sprite[] DiceImageZero;

  DiceText totalDisplay;

  GameState gameState;

  public bool CanRoll;
    // Start is called before the first frame update
    void Start() {
      //Initialize audio variable
      audioPlayer = GameObject.FindObjectOfType<AudioManager>();

      Rolls=new int[4];
      gameState= GameObject.FindObjectOfType<GameState>();
      totalDisplay= GameObject.FindObjectOfType<DiceText>();
      DiceTotal=0;
    }
    // Update is called once per frame
    void Update() {
    }
    //Button Initiates check to see if rolling is a legal action at time.
    public void RollQuery(){
      if(gameState.CanRoll){
        RollTheDice();
      }else{

        //Audio que
        audioPlayer.Play("failedMove");

        Debug.Log("Roll Is limited to once per Turn");
      }
    }
    //Rolls The dice animations and calls update on text total.
    //CanRoll Is set to false. It will return back to true when the turn is
    //ended.
    public void RollTheDice() {

      //Audio que
      audioPlayer.Play("rollDice");

      //Rolls 4 binary dice and totals results in DiceTotal
      DiceTotal = 0;
      for(int i=0;i<Rolls.Length;i++) {
        Rolls[i]= Random.Range(0,2);
        DiceTotal += Rolls[i];
        //Contains 4 children each with an image
        if(Rolls[i]==0){
          this.transform.GetChild(i).GetComponent<Image>().sprite = DiceImageZero[Random.Range(0,DiceImageZero.Length)];
        }else{
          this.transform.GetChild(i).GetComponent<Image>().sprite = DiceImageOne[Random.Range(0,DiceImageOne.Length)];
        }
      }
      //Updates Total Text on Side of Dice Roller
      totalDisplay.UpdateText(DiceTotal);
      gameState.CanRoll=false;
    }
}
