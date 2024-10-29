using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;          // Velocidad de movimiento hacia el jugador
    public float attackRange = 1f;    // Rango de ataque
    public int damage = 30;           // Da�o que el enemigo inflige al jugador
    private Transform player;         // Referencia al jugador
    private bool isAttacking = false; // Controla si el enemigo est� atacando
    private bool playerInDetectionArea = false; // Controla si el jugador est� en el �rea de detecci�n

    void Start()
    {
        // Buscar al jugador en la escena mediante su tag (Aseg�rate de que el jugador tenga el tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Solo seguir y atacar al jugador si est� en el �rea de detecci�n
        if (playerInDetectionArea && player != null && !isAttacking)
        {
            // Calcular la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Si el jugador est� dentro del rango de ataque, iniciar ataque
            if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(Attack());
            }
            else
            {
                // Mover al enemigo hacia el jugador
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }

    // Corrutina para manejar el ataque del enemigo
    private IEnumerator Attack()
    {
        isAttacking = true;

        // Mensaje para debuggear el ataque
        Debug.Log("Enemigo atacando al jugador");

        // Comprobar si el jugador est� en rango para recibir el da�o
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            // Acceder al script del jugador y reducir sus puntos de vida
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.AdjustHitPoints(-damage);
            }
        }

        // Tiempo de espera antes de permitir otro ataque (cooldown)
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    // M�todo que se activa al entrar en el �rea de detecci�n
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDetectionArea = true;
        }
    }

    // M�todo que se activa al salir del �rea de detecci�n
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDetectionArea = false;
        }
    }
}
