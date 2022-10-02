using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void EnterGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(GameManager.Instance.AvailableLevels[GameManager.Instance.CurrentLevel].SceneNumber);
        ++GameManager.Instance.CurrentLevel;
    }
}
