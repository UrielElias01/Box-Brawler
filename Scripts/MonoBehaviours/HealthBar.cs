using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [HideInInspector]
    public Player character; //Referencia al jugador
    public Image meterImage; //Medidor de salud
    public Text hpText; //Texto en barra de salud

    void Start()
    {
        character.hitPoints.value = character.maxHitPoints; // Iniciar en el máximo de vida
    }

    void Update()
    {
        if (character != null)
        {
            // Clamping para evitar que el valor de vida exceda el máximo
            character.hitPoints.value = Mathf.Clamp(character.hitPoints.value, 0, character.maxHitPoints);

            // Modifica barra de salud
            meterImage.fillAmount = character.hitPoints.value / character.maxHitPoints;

            // Texto en barra de salud
            hpText.text = "HP: " + (meterImage.fillAmount * 100).ToString("F0");
        }
    }
}
