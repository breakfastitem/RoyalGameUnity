using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour {
    //Setup audio Manager
    AudioManager audioPlayer;
    DiceRoller theRoller;
    GameState gameState;

    //Assigned it unity ui from object
    public Tile StartingTile;
    Tile currentTile;

    Vector3[] moveQueue;

    int moveQueueIndex;

    Vector3 targetPostion;
    Vector3 velocity;
    Vector3 heightFromStart;
    Vector3 homePosition;

    //Assigned in unity
    public bool RedTeam;


    float smoothTime;
    float smoothDistance;

    // Start is called before the first frame update
    void Start() {
      //Initialize game objects
      audioPlayer=GameObject.FindObjectOfType<AudioManager>();

      theRoller = GameObject.FindObjectOfType<DiceRoller>();
      gameState = GameObject.FindObjectOfType<GameState>();

      targetPostion = this.transform.position;
      heightFromStart= targetPostion+Vector3.up;
      velocity= Vector3.zero;
      homePosition= this.transform.position;

      smoothTime= 0.2f;
      smoothDistance=0.02f;

    }

    // Update is called once per frame
    //TODO: prevent actions during animation bool in game state
    void Update() {
      if(Vector3.Distance(this.transform.position,targetPostion)> smoothDistance){
        this.transform.position = Vector3.SmoothDamp(this.transform.position,targetPostion,ref velocity,smoothTime);
        //Smooths movement as to prevent cutting across board.
      }else if(moveQueue != null && moveQueueIndex < moveQueue.Length){
        SetnewTargetPosition(moveQueue[moveQueueIndex]);
        moveQueueIndex++;
      }
    }

    //Changes vector for update.
    void SetnewTargetPosition(Vector3 pos){
      targetPostion=pos;
      velocity=Vector3.zero;
    }
    //Called When Piece Is defeated
    public void ReturnPieceToStartGrid(){
      //Clear Move Queue
      moveQueue= new Vector3[3];

      //movements that return to home
      moveQueue[0]=this.transform.position+Vector3.up;
      moveQueue[1]=homePosition+2*Vector3.up;
      moveQueue[2]=homePosition;

      currentTile = null;
      moveQueueIndex=0;
    }

    //Checks whether piece has valid turn first
    void OnMouseUp(){
     pieceMoveQuery();
    }
    //This method handles piece movement and also passing the turn
    public void pieceMovement(){

      bool isFirstMove=false;

      Tile targetTile = currentTile;

      int spacesToMove = theRoller.DiceTotal;

      moveQueue = new Vector3[spacesToMove];

      // TODO: Is the mouse over a different Component Ignore if true.
      Debug.Log("Click Registered.");

      //If the Roller Can't Roll move's piece or moves 0
      if(gameState.CanRoll){
        moveQueue = null;
        return;
      }

      //TODO: Change so that if player rolls 0
      //they don't have to click to endturn
      if(spacesToMove==0){
        moveQueue=null;
        gameState.NewTurn();
        return;
      }

      //Loop iterates through tile array and creates move queue to send to update
      for(int i=0;i<spacesToMove;i++){
        if(targetTile==null){
          targetTile= StartingTile;
          // If its lieing on the gorund it gets lifted to board height as to not clip
          isFirstMove=true;
          //IF HIT THE END OF BOARD
        }else if(targetTile.NextTiles==null||targetTile.NextTiles.Length==0){

          //IS IT A LEGAL SCORE
          //TODO: animate score.
          if(gameState.ScoreCheck(currentTile,spacesToMove,RedTeam)){

            //Audio que
            audioPlayer.Play("score");

            Debug.Log("Scored");
            gameState.NewTurn();
            Destroy(gameObject);

          }else if(gameState.gameModeTwoPlayer){
            //Audio
            audioPlayer.Play("failedMove");

            Debug.Log("No Score: Invalid Action");
           
          }
          moveQueue=null;
          return;
        }else if(targetTile.NextTiles.Length > 1){

          if(RedTeam){
            targetTile=targetTile.NextTiles[0];
          }else{
            targetTile=targetTile.NextTiles[1];
          }

        }else{
          targetTile=targetTile.NextTiles[0];
        }
        //up so it doesnt clip the other gameobjects
        moveQueue[i]=targetTile.transform.position+Vector3.up/2;
      }
      //makes last move at tile hieght
      moveQueue[spacesToMove-1]= moveQueue[spacesToMove-1]-Vector3.up/2;

      //Checks tile piece and if move is valid
      if(!gameState.TileCheck(targetTile,RedTeam)){

        if(gameState.gameModeTwoPlayer||RedTeam){
          //Audio que
        audioPlayer.Play("failedMove");

        Debug.Log("This Action is Invalid.");
        }

        moveQueue=null;
        return;
      }
      
      //Play Succesful Movement Sound
      audioPlayer.Play("pieceMove");

      //If first move float before moving
      if(isFirstMove){
        SetnewTargetPosition(heightFromStart);
      }

      //Sets defending piece of landed tile to this object
      targetTile.defendingPiece=this;

      //Initiates Movement
      moveQueueIndex=0;

      //Removes this as defending piece for previous tile
      if(currentTile!=null){
        currentTile.defendingPiece=null;
      }

      //Updates Current Tile and intiates new turn
      currentTile=targetTile;
      gameState.NewTurn();
    }
    //This method checks whether piece move should be called
  public void pieceMoveQuery(){
    bool RedTurn = gameState.RedTurn;
          if(RedTurn==RedTeam){
            this.pieceMovement();
          }else{
            if(RedTeam||gameState.gameModeTwoPlayer){
              //Audio
              audioPlayer.Play("failedMove");

              Debug.Log("Not your turn.");
            }
          }
  }
}
