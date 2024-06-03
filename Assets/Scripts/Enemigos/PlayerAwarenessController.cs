using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;

    public Transform _player;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance && IsPlayerInFieldOfView(enemyToPlayerVector))
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }

        }

    private bool IsPlayerInFieldOfView(Vector2 enemyToPlayerVector)
    {
        // Dirección en la que el enemigo está mirando
        Vector2 enemyForward = transform.up;

        // Ángulo entre la dirección del enemigo y la dirección al jugador
        float angleToPlayer = Vector2.Angle(transform.up, enemyToPlayerVector);

        return angleToPlayer < 90f ;
    }
}