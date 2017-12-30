using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    static int BestScore = -1;

    public void Awake()
    {
        BestScore = InitInteger("BestScore", BestScore);
    }




    //BestScore
    public static void SetBestScore(int NewBestScore)
    {
        BestScore = NewBestScore;
        PlayerPrefs.SetInt("BestScore", BestScore);
        Debug.Log(BestScore);
    }

    public static int GetBestScore()
    {
        return BestScore;
    }


    private int InitInteger(string Name, int CurrentValue)
    {
        if (CurrentValue == -1)
        {
            CurrentValue = PlayerPrefs.GetInt(Name);
            if (CurrentValue == null)
            {
                CurrentValue = 0;
                PlayerPrefs.SetInt(Name, CurrentValue);
            }
        };
        return CurrentValue;
    }


}
