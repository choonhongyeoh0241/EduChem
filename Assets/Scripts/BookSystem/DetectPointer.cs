using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class DetectPointer : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Canvas bookCanvas;
    private bool mouseIsOver = false;

    private void OnEnable() 
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnDeselect(BaseEventData eventData) 
    {
        if (!mouseIsOver) bookCanvas.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData) {
        mouseIsOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseIsOver = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}