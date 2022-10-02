using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool needKey = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(needKey)
            {
                if (CharacterAttributes.Instance._hasKey)
                {
                    SceneInfo.Instance.EndGame(true);
                }
                else
                {
                    SceneInfo.Instance.ShowWarning();
                    SFXManager.Instance.PlayLockedDoor();
                    print("Need key to unlock");
                }
                

            }
            else
            {
                SceneInfo.Instance.EndGame(true);
            }
        }
    }
}
