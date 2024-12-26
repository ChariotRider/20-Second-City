using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public TMP_Text globalTimer;
    public TMP_Text woodCountText;
    public TMP_Text StoneCountText;

    public TMP_Text endgameScore;

    public float timeLeft;
    public float secondTimer;

    public bool roundOver = false;
    public bool buildingsScored = false;

    public GameObject endOfGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isRoundOver(timeLeft);
        if (!roundOver)
        {
            UpdateGlobalTimer();
            UpdateSecondTimer();
            updateResources();
        }
    }

    private void UpdateGlobalTimer()
    {
        if (!roundOver)
        {
            timeLeft -= Time.deltaTime;
            float printedTime = Mathf.Floor(timeLeft);
            globalTimer.text = printedTime.ToString();
        }

        if (roundOver)
        {
            globalTimer.text = 0f.ToString();
        }

    }

    private void UpdateSecondTimer()
    {
        secondTimer -= Time.deltaTime;
        if (secondTimer <= 0)
        {
            secondTimer = 1;
            Debug.Log("Second passed");
            GameEvents.current.SecondPassed();
        }
    }

    private void updateResources()
    {
        woodCountText.text = ("Wood: " + gameManager.resourceManager.woodCount);
        StoneCountText.text = ("Stone: " + gameManager.resourceManager.stoneCount);
    }

    private bool isRoundOver(float time)
    {
        if (time <= 0f)
        {
            roundOver = true;
            whenRoundOver();
            return true;
        } else
        {
            return false;
        }
    }

    private void whenRoundOver()
    {
        timeLeft = 0f;
        UpdateGlobalTimer();

        if (!buildingsScored)
        {
            GameEvents.current.CalculateScoring();
            buildingsScored = true;
            endOfGamePanel.SetActive(true);
            displayEndingScore();
        }

    }

    private void displayEndingScore()
    {
        endgameScore.text = gameManager.score.ToString();
    }

    public void setUIObjectActive(GameObject go)
    {
        go.SetActive(true);
    }

    public void setUIObjectDisabled(GameObject go)
    {
        go.SetActive(false);
    }
}
