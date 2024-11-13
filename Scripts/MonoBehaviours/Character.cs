using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Clase gen�rica para los personajes del Juego.
*/
public abstract class Character : MonoBehaviour
{
    public HitPoints hitPoints; //Puntos actuales de jugador
    public float maxHitPoints; //M�ximos puntos a obtener
}