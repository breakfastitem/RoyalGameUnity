using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public Tile[] NextTiles;

    public int movesTillFinish;

    public PlayerPiece defendingPiece;
    // Start is called before the first frame update
    void Start() {
      defendingPiece = null;
    }

    // Update is called once per frame
    void Update() {

    }
    
}
