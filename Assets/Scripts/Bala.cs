using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    Vector2 puntoDisparo;
    [SerializeField] private float F_bala;
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
        Vector2 dir = new Vector2(puntoDisparo.x - transform.position.x, puntoDisparo.y - transform.position.y);
        dir.Normalize();
        rb.AddForce(dir * F_bala, ForceMode2D.Impulse);
        Destroy(this.gameObject, 1f);  // Destruye la bala después de 1 segundo
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