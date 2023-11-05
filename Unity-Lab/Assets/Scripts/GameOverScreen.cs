using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverScreen : MonoBehaviour
{
    public TMP_Text gameResultTxt; //Display result right under 'Game Over'

    /// <summary>
    /// Sets the game result text and text color based on the game outcome.
    /// </summary>
    /// <param name="didPlayerWin">Whether the player won.</param>
    public void Setup(bool didPlayerWin)
    {

        if (didPlayerWin) 
        {
            gameResultTxt.text = "You won!";
            gameResultTxt.color = Color.green;
        }
        else
        {
            gameResultTxt.text = "You lost!";
            gameResultTxt.color = Color.red;
        }
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets the game result based on the number of points.
    /// </summary>
    /// <param name="points">The player's score.</param>
    public void Setup(int points)
    {
        gameObject.SetActive(true);
        gameResultTxt.text = CalcGameResult(points);

    }

    /// <summary>
    /// Calculates the game result based on the number of points.
    /// </summary>
    /// <param name="points">The player's score.</param>
    /// <returns>The calculated game result as a string.</returns>
    string CalcGameResult(int points)
    {
        //some logic
        return "idk";
    }


    /// <summary>
    /// Starts a new game ('SampleScene') when the "Restart" button is pressed.
    /// </summary>
    public void RestartBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Returns to the main menu when the "Exit" button is pressed.
    /// </summary>
    public void ExitBtn()
    {
        //load main menu
        //SceneManager.LoadScene("MainMenu")
    }

}
