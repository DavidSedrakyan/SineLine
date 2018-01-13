using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {


    [SerializeField]
    private SpriteRenderer torusRenderer;
    [SerializeField]
    private Sprite simpleTorus;
    [SerializeField]
    private Sprite feverTorus;


    void Start () {
        feverMode = false;
	}

    private bool feverMode;
	void Update () {
        if (ScoreManager.feverMode != feverMode && !ScoreManager.feverMode)
        {
            torusRenderer.sprite = simpleTorus;
        }
        else if (ScoreManager.feverMode != feverMode && ScoreManager.feverMode)
        {
            torusRenderer.sprite = feverTorus;
        }
        feverMode = ScoreManager.feverMode;
	}
    

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
