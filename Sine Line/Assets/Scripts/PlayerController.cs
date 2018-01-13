using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float SineAngle = 0;
    [SerializeField]
    private float XSpeed;
    [SerializeField]
    private float YSpeedDelta;
    [SerializeField]
    private float YSpeedStopDelta;
    [SerializeField]
    private float XAmplitude;
    [SerializeField]
    private float YSpeed;
    [SerializeField]
    private float YminSpeed;

    [SerializeField]
    private GameObject camWrapper;
    [SerializeField]
    private float CamDistance;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private Rigidbody2D playerRigid;


    [SerializeField]
    private GameObject simpleEffects;
    [SerializeField]
    private GameObject feverEffects;


    // Use this for initialization
    void Start () {
        ButtonPressed = false;
    }

    // Update is called once per frame
    private float newYSpeed = 0;
    private bool feverMode;

    private Vector3 lastPos; 
    void Update () {
        if(GameManager.GameStarted) {
            
            SineAngle += Time.deltaTime * XSpeed;
            float newY;
            if (ButtonPressed)
            {
                //increase
                newYSpeed = newYSpeed + YSpeedDelta * Time.deltaTime;
                if (newYSpeed > YSpeed) {
                    newYSpeed = YSpeed;
                }
            }
            else {

                //decrease
                newYSpeed = newYSpeed - YSpeedStopDelta * Time.deltaTime;
                if (newYSpeed <= YminSpeed)
                {
                    newYSpeed = YminSpeed;
                }
            }

            newY = transform.position.y + newYSpeed * Time.deltaTime;
        

            transform.position = new Vector3(Mathf.Sin(SineAngle) * XAmplitude, newY, transform.position.z);
            camWrapper.transform.position = new Vector3(camWrapper.transform.position.x, newY + CamDistance, camWrapper.transform.position.z);

           

            if (ScoreManager.feverMode != feverMode && !ScoreManager.feverMode)
            {
                simpleEffects.SetActive(true);
                feverEffects.SetActive(false);
            }
            else if (ScoreManager.feverMode != feverMode && ScoreManager.feverMode)
            {
                simpleEffects.SetActive(false);
                feverEffects.SetActive(true);
            }
            feverMode = ScoreManager.feverMode;


            lastPos = transform.position;
        }
    }
    private bool ButtonPressed;
    public void ButtonDown()
    {
        ButtonPressed = true;
    }

    public void ButtonUp()
    {
        ButtonPressed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "freespacecollider")
        {
            gameManager.GameOver();               
        }

        if (collision.tag == "targetcollider")
        {
            scoreManager.AddScore(2, gameObject);
        }

        if (collision.tag == "perfectcollider")
        {
            scoreManager.AddScore(1,gameObject);
        }
    }
}
