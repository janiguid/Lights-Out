using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInfo : MonoBehaviour
{
    public static Action<bool> OnLightsChange;
    public static SceneInfo Instance;


    public GameObject Player;
    public GameObject EnemyObject;
    public GameObject KeyPrefab;

    public SceneSetUp[] _possibleSceneSetUps;

    public GameObject _jumpScareObject;
    public GameObject _canvasWarning;
    public GameObject _dirLight;
    public bool GameOver = false;
    public bool LightsOff = true;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        print("Setting Level Up");
        StartCoroutine(PeriodicalLightSwitch());

        int randSetup = UnityEngine.Random.Range(0, _possibleSceneSetUps.Length);
        if(GameManager.Instance != null)
        {
            if(GameManager.Instance.PreviousStartingPoint == randSetup)
            {
                randSetup += 1;
                if(randSetup == _possibleSceneSetUps.Length)
                {
                    randSetup = 0;
                }
            }

            GameManager.Instance.PreviousStartingPoint = randSetup;
        }

        
        print("Chosen setup: " + randSetup);
        Instantiate(Player, _possibleSceneSetUps[randSetup].PlayerSpawnPoint.position, Quaternion.identity);

        for (int i = 0; i < _possibleSceneSetUps[randSetup].EnemySpawnPoint.Length; ++i)
        {
            Instantiate(EnemyObject, _possibleSceneSetUps[randSetup].EnemySpawnPoint[i].position, Quaternion.identity);
        }

        int randExit = UnityEngine.Random.Range(0, _possibleSceneSetUps[randSetup].PossibleExits.Length);
        _possibleSceneSetUps[randSetup].PossibleExits[randExit].SetActive(true);

        if (_possibleSceneSetUps[randSetup].KeyPositions.Length > 0)
        {
            int randKey = UnityEngine.Random.Range(0, _possibleSceneSetUps[randSetup].KeyPositions.Length);
            Instantiate(KeyPrefab, _possibleSceneSetUps[randSetup].KeyPositions[randKey].position, Quaternion.identity);
        }
    }

    public void EndGame(bool success)
    {
        GameOver = true;
        if (success)
        {
            print("Player finished level");

            //load transition level
            SceneManager.LoadScene(1);
        }
        else
        {
            if(GameManager.Instance)
            GameManager.Instance.CurrentLevel = 0;

            if (!_dirLight.activeSelf)
            {
                StartCoroutine(BeginFinalJumpScare());
            }
        }
    }

    public IEnumerator BeginFinalJumpScare()
    {
        //ensure all moving objects are off
        //turn lights back on
        SFXManager.Instance.StopAllSounds();
        yield return new WaitForSeconds(3);
        
        LightsOff = false;
        Debug.Log("Lights Off: " + LightsOff);
        OnLightsChange?.Invoke(LightsOff);
        _dirLight.SetActive(LightsOff);
        yield return new WaitForSeconds(2);
        //wait for 2 seconds, turn lights off
        LightsOff = true;
        Debug.Log("Lights Off: " + LightsOff);
        OnLightsChange?.Invoke(LightsOff);
        _dirLight.SetActive(LightsOff);
        _jumpScareObject.SetActive(true);
        SFXManager.Instance.PlayFinalJump();
        //when lights come back on, show jump scare
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }


    IEnumerator PeriodicalLightSwitch()
    {
        while(GameOver == false)
        {
            yield return new WaitForSeconds(2);

            
            LightsOff = !LightsOff;
            Debug.Log("Lights Off: " + LightsOff);
            OnLightsChange?.Invoke(LightsOff);
            _dirLight.SetActive(LightsOff);
            
        }
    }

    public void ShowWarning()
    {
        if (_canvasWarning.activeSelf) return;
        _canvasWarning.SetActive(true);
        Invoke("DeactivateWarning", 1f);
    }

    public void DeactivateWarning()
    {
        _canvasWarning.SetActive(false);
    }
}

[System.Serializable]
public class SceneSetUp
{
    public Transform PlayerSpawnPoint;
    public Transform[] EnemySpawnPoint;
    public GameObject[] PossibleExits;
    public Transform[] KeyPositions;
}
