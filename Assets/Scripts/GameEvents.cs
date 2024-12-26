using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onSecondPassed;
    public void SecondPassed()
    {
        if (onSecondPassed != null)
        {
            onSecondPassed();
        }
    }

    public event Action onCalculateScoring;

    public void CalculateScoring()
    {
        if (onCalculateScoring != null)
        {
            onCalculateScoring();
        }
    }
}
