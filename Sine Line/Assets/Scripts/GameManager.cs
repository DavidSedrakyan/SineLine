using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool GameStarted;
    [SerializeField]
    private GameObject gameOverCanvas;
    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;
        GameStarted = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver()
    {
        GameStarted = false;
        gameOverCanvas.SetActive(true);
    }
}
