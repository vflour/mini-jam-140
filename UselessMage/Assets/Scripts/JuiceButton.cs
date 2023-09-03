using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class JuiceButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [HideInInspector]
    public UnityEvent onButtonClick = new UnityEvent();
    [HideInInspector]
    public UnityEvent onButtonEnter = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        onButtonClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onButtonEnter.Invoke();
    }
}
