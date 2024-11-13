using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
 * Nombre: �ngel Uriel El�as Vel�zquez
 * Descripci�n: Clase para ajustar la posici�n de la c�mara en una cuadr�cula basada en p�xeles,
 *              utilizando el sistema Cinemachine en Unity.
 * Fecha: 14/10/2024
 */

public class RoundCameraPos : CinemachineExtension
{
    // Define cu�ntos p�xeles hay por unidad del mundo (ajustable desde el editor)
    public float PixelsPerUnit = 32;

    // Funci�n que se ejecuta despu�s de que Cinemachine calcula la posici�n de la c�mara
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,  // C�mara virtual de Cinemachine
        CinemachineCore.Stage stage,        // Etapa del pipeline de Cinemachine
        ref CameraState state,              // Estado actual de la c�mara
        float deltaTime)                    // Tiempo transcurrido desde el �ltimo frame
    {
        // Verifica si estamos en la etapa de procesamiento de posici�n de la c�mara (Body)
        if (stage == CinemachineCore.Stage.Body)
        {
            // Obtiene la posici�n final calculada por Cinemachine
            Vector3 finalPos = state.FinalPosition;

            // Redondea la posici�n en los ejes X e Y para ajustarla a la cuadr�cula
            Vector3 newPos = new Vector3(Round(finalPos.x), Round(finalPos.y), finalPos.z);

            // Corrige la posici�n de la c�mara para que se ajuste a los p�xeles de la cuadr�cula
            state.PositionCorrection += newPos - finalPos;
        }
    }

    // Funci�n que redondea una coordenada para alinearla con la cuadr�cula de p�xeles
    float Round(float x)
    {
        // Redondea el valor a la cantidad m�s cercana de p�xeles por unidad
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}
