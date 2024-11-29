using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
 * Nombre: Ángel Uriel Elías Velázquez
 * Descripción: Clase para ajustar la posición de la cámara en una cuadrícula basada en píxeles,
 *              utilizando el sistema Cinemachine en Unity.
 * Fecha: 14/10/2024
 */

public class RoundCameraPos : CinemachineExtension
{
    // Define cuántos píxeles hay por unidad del mundo (ajustable desde el editor)
    public float PixelsPerUnit = 32;

    // Función que se ejecuta después de que Cinemachine calcula la posición de la cámara
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,  // Cámara virtual de Cinemachine
        CinemachineCore.Stage stage,        // Etapa del pipeline de Cinemachine
        ref CameraState state,              // Estado actual de la cámara
        float deltaTime)                    // Tiempo transcurrido desde el último frame
    {
        // Verifica si estamos en la etapa de procesamiento de posición de la cámara (Body)
        if (stage == CinemachineCore.Stage.Body)
        {
            // Obtiene la posición final calculada por Cinemachine
            Vector3 finalPos = state.FinalPosition;

            // Redondea la posición en los ejes X e Y para ajustarla a la cuadrícula
            Vector3 newPos = new Vector3(Round(finalPos.x), Round(finalPos.y), finalPos.z);

            // Corrige la posición de la cámara para que se ajuste a los píxeles de la cuadrícula
            state.PositionCorrection += newPos - finalPos;
        }
    }

    // Función que redondea una coordenada para alinearla con la cuadrícula de píxeles
    float Round(float x)
    {
        // Redondea el valor a la cantidad más cercana de píxeles por unidad
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}
