using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navAgent;
    private Transform _target;
    
    // Start is called before the first frame update
    void Start()
    {
        _target = FindObjectOfType<PlayerMovement>().transform;   

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneInfo.Instance.GameOver)
        {
            gameObject.SetActive(false);
            return;
        }
        if (!SceneInfo.Instance.LightsOff)
        {
            _navAgent.SetDestination(_target.position);
        }
        else
        {
            _navAgent.SetDestination(transform.position);
        }
        
    }
}
