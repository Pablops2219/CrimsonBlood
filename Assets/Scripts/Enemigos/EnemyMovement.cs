
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _stoppingDistance; // New serialized field for stopping distance
    
    [SerializeField]
    private float _shootingRange; // Nuevo campo serializado para el rango de disparo

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    public GameObject bala; // Prefab de la bala
    public Transform puntoDisparo; // Punto de disparo como Transform
    public Transform player; // El jugador
    public float disparoCooldown = 0.7f; // Tiempo de enfriamiento entre disparos
    private bool cooldownReady = true; // Indica si el enfriamiento está listo

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
    }

    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_playerAwarenessController.AwareOfPlayer && Vector2.Distance(transform.position, _playerAwarenessController._player.position) <= _shootingRange)
        {
            _rigidbody.velocity = Vector2.zero;
            if (cooldownReady)
            {
                DispararPistola();
                StartCoroutine(StartCooldown(disparoCooldown));
            }
        }
        else
        {
            _rigidbody.velocity = transform.up * _speed;
        }
    }
    
    private void DispararPistola()
    {
        // Instancia la bala y establece su dirección
        GameObject balaInst = Instantiate(bala, puntoDisparo.position, puntoDisparo.rotation);
        balaInst.GetComponent<Bala>().SetPuntoDisparo(player.position);
    }
    
    IEnumerator StartCooldown(float cooldown)
    {
        // Establece el indicador de enfriamiento en falso
        cooldownReady = false;
        // Espera la duración del enfriamiento
        yield return new WaitForSeconds(cooldown);
        // Establece el indicador de enfriamiento en verdadero después de la duración
        cooldownReady = true;
    }
    
}