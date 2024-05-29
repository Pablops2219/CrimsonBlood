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
    
    public GameObject bala; // Prefab de la bala
    public Transform puntoDisparo; // Punto de disparo como Transform

    public float disparoCooldown = 0.7f; // Tiempo de enfriamiento entre disparos
    private bool cooldownReady = true; // Indica si el enfriamiento está listo


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
            LookTowards(player.position);
            if (cooldownReady)
            {
                DispararPistola();
                StartCoroutine(StartCooldown(disparoCooldown));
            }
        }

        chaseTimer -= Time.deltaTime;
        if (chaseTimer <= 0)
        {
            isChasing = false;
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

    void LookTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90)); // +90 para que la rotación sea correcta
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
