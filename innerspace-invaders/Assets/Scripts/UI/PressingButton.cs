using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressingButton : Button, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsBeingPressed { get; private set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsBeingPressed = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsBeingPressed = false;
    }

}
