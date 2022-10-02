using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    public static CharacterAttributes Instance;

    public float _playerSpeed = 5;
    public bool _hasKey = false;
    // Start is called before the first frame update
    void Start()
    {
        _hasKey = false;
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


}
