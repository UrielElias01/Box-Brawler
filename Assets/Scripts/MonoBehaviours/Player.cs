using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
Clase Player que hereda de Character
*/
public class Player : Character
{
    public HealthBar healthBarPrefab; //Referencia HealthBar Prefab
    private HealthBar healthBar; //Copia de referencia de HealthBar Prefab
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform launchPoint; // Punto desde donde se lanzará el proyectil
    void Start()
    {
        healthBar = Instantiate(healthBarPrefab); //Instanciar HealthBar
        healthBar.character = this; //Referencia del Player en HealthBar
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Cambia "Space" por la tecla o botón que prefieras
        {
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        Debug.Log("Se lanzó un proyectil"); // Mensaje en la consola
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
        if (hitPoints.value == 0)
        {
            print("Juego Terminado. Cargando la escena GameOver.");
            SceneManager.LoadScene("GamerOver"); // Carga la escena llamada "GameOver"
            return true;
        }

        return amount > 0 && newHitPoints <= maxHitPoints; // Solo desaparece si aumenta vida y no sobrepasa el máximo
    }
}