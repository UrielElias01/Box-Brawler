using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //velocidad de personajes
    public float movenmentSpeed = 3.0f;

    //representa la ubicacion del player o enemy
    Vector2 movenment = new Vector2();

    //referencia a RigidBpdy2D
    Rigidbody2D rb2D;

    Animator animator; //referencia a componente animator
    string animationState = "AnimationState"; //variable de Animator

    //enumeracion de los estados
    enum CharStates
    {
        walkEast =1,
        walkWest =3,
        walkSouth =2,
        walkNorth = 4,
        idleSouth =5
    }


    void Start()
    {
        //establece el componente rigibody2D enlazado
        rb2D = GetComponent<Rigidbody2D>();

        //Establece valor de componete Animator el objeto ligado
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        this.UpdateState();  //invoca al meto

    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        //captura los datos de entrada del usuario
        movenment.x = Input.GetAxisRaw("Horizontal");
        movenment.y = Input.GetAxisRaw("Vertical");

        //conserva el rango de velocidad
        movenment.Normalize();
        rb2D.velocity = movenment * movenmentSpeed;
    }

    private void UpdateState()
    {
        if (movenment.x > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkEast);
        } else if (movenment.x < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkWest);
        }
        else if (movenment.y > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkNorth);
        }
        else if (movenment.y < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkSouth);
        } else
        {
            animator.SetInteger(animationState, (int)CharStates.idleSouth);
        }
    }
}
