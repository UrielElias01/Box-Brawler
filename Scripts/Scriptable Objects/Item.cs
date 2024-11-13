using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item")] //Opci�n m�s en el men�
public class Item : ScriptableObject
{
    public string objectName; //Nombre del personaje
    public Sprite sprite; //Referencia a un Item Sprite
    public int quantity; //Cantidad de un Item espec�fico
    public bool stackable; //M�ltiples copias
    public ItemType itemType; //Tipo de un elemento
    public enum ItemType //Identifica el tipo objeto consumible
    {
        COIN,
        HEALTH
    }
}
