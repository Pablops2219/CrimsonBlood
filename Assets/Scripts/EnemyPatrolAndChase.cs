using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolAndChase : MonoBehaviour
{
    public Transform[] patrolPoints; // Puntos de patrulla
    public float patrolSpeed = 2f; // Velocidad de patrulla
    public float chaseSpeed = 3.5f; // Velocidad de persecución
    public Vector2 detectionAreaSize = new Vector2(5f, 5f); // Tamaño del área de detección (ancho y alto)
    public Vector2 detectionAreaOffset = Vector2.zero; // Desplazamiento del área de detección
    public Transform player; // El jugador
    public float chaseTime = 2f; // Tiempo de persecución después de perder de vista al jugador

    private int currentPatrolIndex;
    private bool isChasing;
    private float chaseTimer;
    private Vector3 initialPosition;

    void Start()
    {
        currentPatrolIndex = 0;
        isChasing = false;
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        DetectPlayer();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        MoveTowards(targetPatrolPoint.position, patrolSpeed);

        if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            MoveTowards(player.position, chaseSpeed);
        }

        chaseTimer -= Time.deltaTime;
        if (chaseTimer <= 0)
        {
            isChasing = false;
        }
    }

    void MoveTowards(Vector3 targetPosition, float speed)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }
    }

    void DetectPlayer()
    {
        if (player != null)
        {
            Vector2 detectionCenter = (Vector2)transform.position + detectionAreaOffset;
            Rect detectionRect = new Rect(detectionCenter - detectionAreaSize / 2, detectionAreaSize);

            if (detectionRect.Contains(player.position))
            {
                isChasing = true;
                chaseTimer = chaseTime;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 detectionCenter = (Vector2)transform.position + detectionAreaOffset;
        Gizmos.DrawWireCube(detectionCenter, detectionAreaSize);
    }
}
