using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    Vector2 puntoDisparo;
    [SerializeField] private float F_bala;
    [SerializeField] private float variacionDireccion = 0.5f;  // Increased factor for more noticeable variation
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Disparo();
    }

    // Método para disparar la bala
    public void Disparo()
    {
        // Calcular la dirección hacia el punto de disparo
        Vector2 dir = puntoDisparo - (Vector2)transform.position;
        dir.Normalize();

        // Calcular la dirección perpendicular a la original
        Vector2 perpendicularDir = new Vector2(-dir.y, dir.x);

        // Añadir una variación aleatoria a la dirección perpendicular
        float randomFactor = Random.Range(-variacionDireccion, variacionDireccion);
        Vector2 finalDir = dir + perpendicularDir * randomFactor;

        // Output the direction for debugging purposes
        Debug.Log($"Direction: {finalDir.x}, {finalDir.y}");

        // Aplicar la fuerza en la dirección final
        rb.AddForce(finalDir * F_bala, ForceMode2D.Impulse);

        // Destruir la bala después de 1 segundo
        Destroy(this.gameObject, 1f);
    }

    // Método para establecer el punto de disparo
    public void SetPuntoDisparo(Vector2 puntoDisparo)
    {
        this.puntoDisparo = puntoDisparo;
    }

    // Método para detectar colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);  // Destruye el enemigo
        }

        Destroy(this.gameObject);  // Destruye la bala
    }
}