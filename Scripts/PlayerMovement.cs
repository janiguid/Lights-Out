using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _main;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    Vector3 dir;


    private void Start()
    {
        CharacterAttributes.Instance._hasKey = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneInfo.Instance.GameOver)
        {
            SFXManager.Instance.PlayWalk(false);
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir = Vector3.left;

            if (!_spriteRenderer.flipX)
            {
                _spriteRenderer.flipX = true;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir = Vector3.right;

            if (_spriteRenderer.flipX)
            {
                _spriteRenderer.flipX = false;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir = Vector3.back;
        }
        else
        {
            dir = Vector3.zero;
        }

        if (dir != Vector3.zero)
        {
            SFXManager.Instance.PlayWalk(true);
            _animator.SetBool("Walking", true);
        }
        else
        {
            SFXManager.Instance.PlayWalk(false);
            _animator.SetBool("Walking", false);
        }
    }

    private void FixedUpdate()
    {
        if (SceneInfo.Instance.GameOver) return;
        _rigidBody.MovePosition(transform.position + dir * Time.deltaTime * CharacterAttributes.Instance._playerSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (SceneInfo.Instance._dirLight.activeSelf) return;
            _animator.SetBool("End", true);
            SceneInfo.Instance.EndGame(false);
        }
    }
}
