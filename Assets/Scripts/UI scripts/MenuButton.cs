using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text textComponent;
    public Image imageComponent; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        textComponent.color = Color.white;
        imageComponent.enabled = true;
    } 

    public void OnPointerExit(PointerEventData eventData)
    {
        textComponent.color = Color.black;
        imageComponent.enabled = false;
    }
}
