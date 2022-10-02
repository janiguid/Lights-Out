using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterAttributes.Instance._hasKey = true;
            gameObject.SetActive(false);
            SFXManager.Instance.PlayKeyCollect();
        }
    }
}
