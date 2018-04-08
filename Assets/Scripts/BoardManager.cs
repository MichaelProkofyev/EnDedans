using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : SingletonComponent<BoardManager> {

    public Vector2Int bottomLeftCorner = new Vector2Int(0, 0);
    public Vector2Int topRightCorner = new Vector2Int(5, 5);

    
	
	// Update is called once per frame
	void Update () {
		
	}
}
