using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;
    string animationState = "Estado"; // Nombre del parámetro de estado en el Animator
    int currentDirectionState; // Variable para almacenar el estado de dirección actual

    public int swordDamage = 10; // Daño que la espada inflige al enemigo

    enum CharStates
    {
        izquierda = 1,
        derecha = 2
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator no encontrado en el objeto Sword.");
            return;
        }

        // Configurar el estado inicial a "derecha" y establecer en el Animator
        currentDirectionState = (int)CharStates.derecha;
        animator.SetInteger(animationState, currentDirectionState);
    }

    void Update()
    {
        if (animator == null)
        {
            return; // Salir si falta el Animator
        }

        // Verificar entrada del teclado para cambiar el estado de dirección
        UpdateDirection();

        // Verificar entrada del teclado para el ataque
        PerformAttack();
    }

    private void UpdateDirection()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Cambiar estado a derecha
            currentDirectionState = (int)CharStates.derecha;
            animator.SetInteger(animationState, currentDirectionState);
            Debug.Log("Estado cambiado a derecha.");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            // Cambiar estado a izquierda
            currentDirectionState = (int)CharStates.izquierda;
            animator.SetInteger(animationState, currentDirectionState);
            Debug.Log("Estado cambiado a izquierda.");
        }
    }

    private void PerformAttack()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Realizar el ataque según la última dirección conocida, que por defecto es derecha
            if (currentDirectionState == (int)CharStates.derecha)
            {
                Debug.Log("Atacando hacia la derecha con Trigger 'Attack'.");
                animator.ResetTrigger("Attack2"); // Asegurarse de que 'Attack2' no esté activado
                animator.SetTrigger("Attack");
            }
            else if (currentDirectionState == (int)CharStates.izquierda)
            {
                Debug.Log("Atacando hacia la izquierda con Trigger 'Attack2'.");
                animator.ResetTrigger("Attack"); // Asegurarse de que 'Attack' no esté activado
                animator.SetTrigger("Attack2");
            }
        }
    }

    // Este método se activará cuando la espada colisione con otro objeto
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Verifica si el objeto tiene el tag "Enemy"
        {
            // Obtener el componente Enemy del objeto colisionado
            Enemy enemyScript = collision.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                // Reducir la vida del enemigo
                enemyScript.TakeDamage(swordDamage);
                Debug.Log("Enemigo golpeado por la espada. Vida restante: " + enemyScript.currentHealth);
            }
        }
    }
}
