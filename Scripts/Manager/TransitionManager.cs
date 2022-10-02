using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class TransitionManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public GameObject BlackScreen;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TurnLightsOn", 1);
    }

    void TurnLightsOn()
    {
        if (GameManager.Instance != null)
        text.text = "level 0" + GameManager.Instance.CurrentLevel.ToString();
        BlackScreen.SetActive(false);
        AudioSource.Play();
    }

    public void LoadNextLevel()
    {
        print("press");
        //replace this 1 with the next level from the game manager
        SceneManager.LoadScene(GameManager.Instance.GetSceneNumber());
        ++GameManager.Instance.CurrentLevel;
    }
}
