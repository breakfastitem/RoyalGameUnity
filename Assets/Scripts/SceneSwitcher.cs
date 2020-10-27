using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    AudioManager audioPlayer;

    

    // Start is called before the first frame update
    void Start()
    {
      
        //Initilize audio
        audioPlayer = GameObject.FindObjectOfType<AudioManager>();
    } 
    //update every frame
    void Update(){
    }  
    //Transistions form Menu to main game scene
    public void GameStart(){
        //Audio 
        audioPlayer.Play("gameStart");

        SceneManager.LoadScene("UrScene");
    }
    //Ends the Application
    public void ExitGame(){

        Application.Quit();
    }
    
}
