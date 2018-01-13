using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static int Score;

    [SerializeField]
    private Text ScoreTxt;
    
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private SpriteRenderer SceneLight;
    [SerializeField]
    private Color camColorSimple;
    [SerializeField]
    private Color camColorFever;

    private Animator ScoreAnim;

    // Use this for initialization
    void Start()
    {
        ScoreAnim = ScoreTxt.GetComponent<Animator>();
        Score = 0;
        ComboMultiplyer = 1;
    }

    public void resetScore()
    {
        Score = 0;
        ShowScore();
        ComboMultiplyer = 1;
        DisableFeverMode();
    }

    public void AddScore(int TargetNum, GameObject target)
    {
        int NewScore = ProcessCombo(TargetNum, target);
        if (NewScore > 8) NewScore = 8;
        Score += NewScore;

        ShowScore();
        
    }


    [SerializeField]
    private TextMesh ComboScoreObj;
    [SerializeField]
    private Animator ComboScoreAnim;

    private int ComboMultiplyer = 1;

    [SerializeField]
    private string[] CoolTexts;

    public static bool feverMode = false;

    public int ProcessCombo(int TargetNum, GameObject target)
    {
        string text;
        if (TargetNum == 1)
        {
            ComboMultiplyer += 1;

            int RandomTextIndex = Random.Range(0, CoolTexts.Length);
            text = CoolTexts[RandomTextIndex] + "\n+" + ComboMultiplyer;
        }
        else
        {
            ComboMultiplyer = 1;
            text = "+" + ComboMultiplyer;
        };

        ComboScoreObj.text = text;

        
        ComboScoreObj.transform.position = new Vector3(0, target.transform.position.y +5, ComboScoreObj.transform.position.z);
        ComboScoreAnim.Play("NewScore", -1, 0);


        if (!feverMode && ComboMultiplyer == 4)
        {
            EnableFeverMode();
        }
        else if (ComboMultiplyer < 4 && feverMode)
        {
            DisableFeverMode();
        }
        else if (ComboMultiplyer < 4)
        {
            feverMode = false;
        };

        return ComboMultiplyer;
    }

    private bool colorChangingMode = false;
    private float colorChangingT = 0;
    [SerializeField]
    private float ColorChangingSpeed = 2;

    private void DisableFeverMode()
    {
        feverMode = false;
        colorChangingMode = true;
    }

    private void EnableFeverMode()
    {
        feverMode = true;
        cam.backgroundColor = camColorFever;
        cam.GetComponent<CameraController>().vibrate();
    }

    
    
    private void ShowScore()
    {
        ScoreTxt.text = Score.ToString();       
       
        ScoreAnim.Play("ScoreAnim", 0, 0);
    }

    float HitTimer = 0;
    float timerSpeed = 0;
    private bool enabletimer = false;

    public void ResetTimer()
    {
        HitTimer = 0;
    }

    public void PauseTimer()
    {
        enabletimer = false;
    }

    public void StartTimer()
    {
        enabletimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (colorChangingMode)
        {

            if (feverMode)
            {

            }
            else
            {
                cam.backgroundColor = Color.Lerp(camColorFever, camColorSimple, colorChangingT);
            }

            colorChangingT += Time.deltaTime * ColorChangingSpeed;

            if (colorChangingT >= 1)
            {
                colorChangingMode = false;
                colorChangingT = 0;
            }

        }
    }
}
