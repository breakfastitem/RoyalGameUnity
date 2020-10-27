using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
    //Boolean for selector mode
    public bool gameModeTwoPlayer;
    //Game mod Selector Panel
    public GameObject gameModePanel;
    public bool CanRoll;
    public bool RedTurn;

    //For win condition
    public int redPieceCount;
    public int bluePieceCount;
    //Score Cards 0-> red 1->blue
    public ScoreText[] scores;

    AudioManager audioPlayer;

    TurnTracker turnDisplay;

    DiceText moveNumber;

    AIPlayer blueAI;

    GameOver gameEnder;
    // Start is called before the first frame update
    void Start(){
      audioPlayer = GameObject.FindObjectOfType<AudioManager>();

      blueAI =GameObject.FindObjectOfType<AIPlayer>();

      //CanRoll is initial false so player can choose mode
      CanRoll=false;

      RedTurn=true;
      redPieceCount=6;
      bluePieceCount=6;
      turnDisplay = GameObject.FindObjectOfType<TurnTracker>();
      moveNumber =GameObject.FindObjectOfType<DiceText>();
      gameEnder =GameObject.FindObjectOfType<GameOver>();
    }

    // Update is called once per frame
    void Update() {
      //On Escape Key press Return to main menu
      if(Input.GetKeyDown(KeyCode.Escape)){
        this.ReturnToMain();
        Debug.Log("Escape Was pressed");
      }
    }
    //Button for onePlayer and two Player
    public void onePlayer(){

      //Audio player
      audioPlayer.Play("buttonClick");

      gameModeTwoPlayer =false;
      CanRoll=true;
      gameModePanel.SetActive(false);

    }
    //Configures Button For Two Player
    public void twoPlayer(){

      //Audio player
      audioPlayer.Play("buttonClick");

      gameModeTwoPlayer=true;
      CanRoll=true;
      gameModePanel.SetActive(false);
      
    }
    //Determines Logic for Scoring
    public bool ScoreCheck(Tile currentTile,int moves,bool RedTeam){
      if(currentTile.movesTillFinish==moves){
        if(RedTeam){
          redPieceCount --;

          scores[0].updateScore(redPieceCount);
        }else{
          bluePieceCount --;
          
          scores[1].updateScore(bluePieceCount);
        }
        return true;
      }
      return false;
    }
    //checks what is on tile
    //true if action passes
    //false if action is invalid
    //also removes enemy piece.
    public bool TileCheck(Tile desiredTile,bool attackTeam){
      PlayerPiece defendingPiece = desiredTile.defendingPiece;
      if(defendingPiece==null){
        return true;
      }else if(defendingPiece.RedTeam!= attackTeam){

        //Audio que
        audioPlayer.Play("pieceCapture");

        defendingPiece.ReturnPieceToStartGrid();
        return true;
      }else{
        return false;
      }
    }
    //Completes All processes for new turn
    public void NewTurn(){
      if(redPieceCount==0||bluePieceCount==0){
        this.GameOver();
      }else{
        
        RedTurn = (!RedTurn);
        turnDisplay.SetTurnText(RedTurn);
        moveNumber.NewTurnText();
        
        if(RedTurn||gameModeTwoPlayer){
          CanRoll=true;
        }else{
          blueAI.executeTurn();
        }
      
        Debug.Log("New Turn Triggered");
      }
      
    }
    //Process A player Victory
    void GameOver(){
      
      //Audio que
      audioPlayer.Play("gameOver");

      gameEnder.DetermineWinner(redPieceCount==0);
    }
    //Returns To main Menu
    public void ReturnToMain(){

      //Audio player
      audioPlayer.Play("buttonClick");

      SceneManager.LoadScene( "MainMenu" ,LoadSceneMode.Single);
      Debug.Log("Returning to Menu");
    }
    //Prevents abuse of pass button
    public void passTurnQuery(){
      if(RedTurn||gameModeTwoPlayer){
        this.NewTurn();
      }else{
        //Audio
        audioPlayer.Play("failedMove");

        Debug.Log("Wait for AI");
      }
    }
  
}
