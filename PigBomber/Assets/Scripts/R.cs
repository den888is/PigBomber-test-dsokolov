using UnityEngine;
using UnityEngine.EventSystems;

public class R : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool active;
    void Update()
    {
        if (active)
            transform.parent.gameObject.GetComponent<MooveArrows>().Go(Direction.RIGHT);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        active = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        active = false;
    }
    public void Active(bool active)
    {
        this.active = active;
    }
}
