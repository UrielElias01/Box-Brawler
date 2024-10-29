using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Clase genérica para los personajes del Juego.
*/
public abstract class Character : MonoBehaviour
{
    public HitPoints hitPoints; //Puntos actuales de jugador
    public float maxHitPoints; //Máximos puntos a obtener
}