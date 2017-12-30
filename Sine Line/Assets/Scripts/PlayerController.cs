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
    private Camera cam;
    [SerializeField]
    private float CamDistance;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private Rigidbody2D playerRigid;


    // Use this for initialization
    void Start () {
        ButtonPressed = false;
    }

    // Update is called once per frame
    private float newYSpeed = 0;

    void Update () {
        if(GameManager.GameStarted) {
            SineAngle += Time.deltaTime * XSpeed;
            float newY;
            if (ButtonPressed)
            {
                newYSpeed = newYSpeed + YSpeedDelta * Time.deltaTime;
                if (newYSpeed > YSpeed) {
                    newYSpeed = YSpeed;
                }

                newY = transform.position.y + newYSpeed * Time.deltaTime;
            }
            else {

                newYSpeed = newYSpeed - YSpeedStopDelta * Time.deltaTime;
                if (newYSpeed < 0)
                {
                    newYSpeed = 0;
                }

                newY = transform.position.y + newYSpeed * Time.deltaTime;
            }

            transform.position = new Vector3(Mathf.Sin(SineAngle) * XAmplitude, newY, transform.position.z);
            cam.transform.position = new Vector3(cam.transform.position.x, newY + CamDistance, cam.transform.position.z);


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

        if(collision.tag == "targetcollider")
        {
            scoreManager.addScore(5);
        }
    }
}
