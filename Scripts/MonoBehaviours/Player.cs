using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Clase Player que hereda de Character
*/
public class Player : Character
{
    public HealthBar healthBarPrefab; //Referencia HealthBar Prefab
    private HealthBar healthBar; //Copia de referencia de HealthBar Prefab
    void Start()
    {
        healthBar = Instantiate(healthBarPrefab); //Instanciar HealthBar
        healthBar.character = this; //Referencia del Player en HealthBar
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            if (hitObject != null)
            {
                Debug.Log("Nombre: " + hitObject.objectName);
                bool shouldDisappear = false;
                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN: //Moneda
                        shouldDisappear = true;
                        break;
                    case Item.ItemType.HEALTH://Barra de Salud
                        Debug.Log("Cantidad a Incrementar: " + hitObject.quantity);
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                }
                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false); //Desaparecer
                }
            }
        }
    }
    public bool AdjustHitPoints(int amount)
    {
        // Permitir que el ajuste de puntos de vida sea negativo o positivo según corresponda
        int newHitPoints = (int)(hitPoints.value + amount);
        hitPoints.value = Mathf.Clamp(newHitPoints, 0, maxHitPoints); // Restringe entre 0 y máximo

        print("Ajustando Puntos: " + amount + ". Nuevo Valor: " + hitPoints.value);
        
        return amount > 0 && newHitPoints <= maxHitPoints; // Solo desaparece si aumenta vida y no sobrepasa el máximo
    }
}