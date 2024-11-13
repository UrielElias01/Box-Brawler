using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Da�o que har� el proyectil

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Entrando");
                enemy.TakeDamage(damage); // Llama al m�todo para hacer da�o
            }
           // Destroy(gameObject); // Destruye el proyectil despu�s de impactar
        }
    }
}
