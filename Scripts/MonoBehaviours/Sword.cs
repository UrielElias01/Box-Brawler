using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;
    string animationState = "Estado"; // Nombre del parámetro de estado en el Animator
    int currentDirectionState = (int)CharStates.derecha; // Variable para almacenar el estado de dirección actual

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
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            // Para los movimientos hacia arriba (W) y abajo (S), establecer dirección en derecha por defecto
            currentDirectionState = (int)CharStates.derecha;
            animator.SetInteger(animationState, currentDirectionState);
            Debug.Log("Moviendo hacia arriba o abajo. Dirección cambiada a derecha por defecto.");
        }
    }


    private void PerformAttack()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Restablece triggers para evitar conflictos
            animator.SetTrigger("Attack");
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Attack2");

            // Verifica la dirección actual y establece la animación adecuada
            if (currentDirectionState == (int)CharStates.izquierda)
            {
                Debug.Log("Atacando hacia la izquierda con Trigger 'Attack2'.");
                animator.SetTrigger("Attack2");
            }
            else
            {
                // Si la dirección es 'derecha' o no ha sido definida, ataca hacia la derecha por defecto
                Debug.Log("Atacando hacia la derecha con Trigger 'Attack'.");
                animator.SetTrigger("Attack");
            }
        }
    }



    // Este método se activará cuando la espada colisione con otro objeto
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el collider es de tipo BoxCollider2D y tiene el tag "Enemy"
        if (collision.CompareTag("Enemy") && collision is BoxCollider2D)
        {
            Enemy enemyScript = collision.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(swordDamage);
                Debug.Log("Enemigo golpeado por la espada. Vida restante: " + enemyScript.currentHealth);
            }
        }
    }

}
