using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    //Inititiate turn neccesities
    GameState gameState;
    DiceRoller diceRoller;


    public PlayerPiece[] pieces;
    // Start is called before the first frame update
    void Start()
    {
        gameState=GameObject.FindObjectOfType<GameState>();
        diceRoller= GameObject.FindObjectOfType<DiceRoller>();
    }
    //Iterates through all pieces to try to find legal move
    public void executeTurn(){
        diceRoller.RollTheDice();

        StartCoroutine(waitForRed());
        
    }
    //Wait Routine
    IEnumerator waitForRed(){
         //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1.5f);

         //While Ai's Turn
       
         for(int i=0;i<pieces.Length;i++){
            if(pieces[i]!=null){
              pieces[i].pieceMoveQuery();
            }
         }
        if(!gameState.RedTurn){
            gameState.NewTurn();
        }

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
