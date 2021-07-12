using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public static bool isPaused = false;

    public static void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1;
    }

    public static void PauseGame()
    {
        isPaused = true;

        Time.timeScale = 0;
    }
}
