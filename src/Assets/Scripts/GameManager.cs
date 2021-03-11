using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI PlayerOneLives;
    public int playerOneLives = 30;
    public TextMeshProUGUI PlayerTwoLives;
    public int playerTwoLives = 30;
    public TextMeshProUGUI rollValueOne;
    public TextMeshProUGUI rollValueTwo;
    public int rolled;
    public int rolledTwo;
    public int check;
    public bool gameOver = false;
    public bool one = true;
    public Animator GuardOne;
    public Animator GuardTwo;
    public GameObject GameOverScreen;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI TurnText;
    public GameObject names;

    private void Start()
    {
        Physics.gravity *= 2;
    }
    void Update()
    {
        if (one)
        {
            TurnText.text = "Player One's Turn";
        }
        else
        {
            TurnText.text = "Player Two's Turn";
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            restartGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (one)
            {
                rollOne();
            }
            else
            {
                rollTwo();
            }
        }
        if (playerOneLives == 0)
        {
            GameOver(false);
        }
        else if (playerTwoLives == 0)
        {
            GameOver(true);
        }
    }
    public void rollOne()
    {
        if (!gameOver && one)
        {
            rolled = Random.Range(1, 7);
            check = playerTwoLives - rolled;
            rollValueOne.text = "You Rolled: " + rolled;
            if (check < 0)
            {
                playerTwoLives = 0;
                GameOver(true);
                PlayerTwoLives.text = "Player 2 Hearts: 0";
            }
            else
            {
                playerTwoLives -= rolled;
                PlayerTwoLives.text = "Player 2 Hearts: " + playerTwoLives;
            }
            one = false;
        }
    }

    public void rollTwo()
    {
        if (!gameOver && !one)
        {
            rolledTwo = Random.Range(1, 7);
            check = playerOneLives - rolledTwo;
            rollValueTwo.text = "You Rolled: " + rolledTwo;
            if (check < 0)
            {
                playerOneLives = 0;
                GameOver(false);
                PlayerOneLives.text = "Player 1 Hearts: 0";
            }
            else
            {
                playerOneLives -= rolledTwo;
                PlayerOneLives.text = "Player 1 Hearts: " + playerOneLives;
            }
            one = true;
        }
    }

    public void GameOver(bool winOne)
    {
        gameOver = true;
        TurnText.gameObject.SetActive(false);
        GameOverScreen.SetActive(true);
        names.SetActive(false);
        if (winOne)
        {
            GameOverText.text = "Game Over, Player 1 Won!";
            GuardOne.SetBool("Won", true);
            GuardTwo.enabled = false;
        }
        else
        {
            GameOverText.text = "Game Over, Player 2 Won!";
            GuardTwo.SetBool("Won", true);
            GuardOne.enabled = false;
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}