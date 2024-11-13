using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Daño que hará el proyectil

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Entrando");
                enemy.TakeDamage(damage); // Llama al método para hacer daño
            }
           // Destroy(gameObject); // Destruye el proyectil después de impactar
        }
    }
}
