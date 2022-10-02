using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectEnemy : MonoBehaviour
{
    public LayerMask layerToCheck;
    Collider[] colliders;

    private void Start()
    {
        colliders = new Collider[5];
    }
    private void OnEnable()
    {
        SceneInfo.OnLightsChange += CheckForEnemies;
    }

    private void OnDisable()
    {
        SceneInfo.OnLightsChange -= CheckForEnemies;
    }

    public void CheckForEnemies(bool lightState)
    {
        if (lightState)
        {
            if (Physics.OverlapSphereNonAlloc(transform.position, 3f, colliders, layerToCheck) > 0)
            {
                Debug.Log("Enemy Detected");
                SFXManager.Instance.PlaySoftJump();
            }
        }

    }
}
