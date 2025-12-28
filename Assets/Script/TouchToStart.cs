using UnityEngine;
using UnityEngine.EventSystems;

public class TouchToStart : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.GameStartEvent();
        gameObject.SetActive(false);
    }
}
