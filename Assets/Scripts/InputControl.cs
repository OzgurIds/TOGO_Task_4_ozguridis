using UnityEngine;
using UnityEngine.EventSystems;

public class InputControl : MonoBehaviour, IPointerDownHandler, IDragHandler

{
    public GameObject Player;
    public float speed;
    bool start = false;
    float xclamp;

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //Left-Right Movement
        Player.transform.position += new Vector3(eventData.delta.x * speed / 2 * Time.deltaTime, 0, 0);
        xclamp = Mathf.Clamp(Player.transform.position.x,1.36f, 8.7f);
        Player.transform.position = new Vector3(xclamp, Player.transform.position.y, Player.transform.position.z);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        start = true;
    }

    void Update()
    {
        if (start)
        {
            //Forward
            Player.transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
        }
    }
}
