using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MouseDrag : MonoBehaviour
{
    public Animator drag;

    FluteSay fs;

    void Start()
    {
        fs = FluteSay.instance;
    }

    private void OnMouseDrag()
    {
        fs.grabeddrill = true;
        drag.SetFloat("Drillspeed", 1f);
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = gameObject.transform.position.z;
        gameObject.transform.position = point;
        Cursor.visible = false;
    }

    private void OnMouseUp()
    {
        fs.grabeddrill = false;
        drag.SetFloat("Drillspeed", -1f);
        Cursor.visible = true;
    }
}
