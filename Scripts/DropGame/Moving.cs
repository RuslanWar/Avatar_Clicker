using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
public class Moving : MonoBehaviour
{
    bool isTake;
    public Camera moving;
    float zDistance = 10;
    private void Start()
    {
        moving = FindAnyObjectByType<Camera>();
        this.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void OnMouseDown()
    {
        isTake = true;
        
    }
    private void OnMouseUp()
    {
        isTake = false;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
    private void Update()
    {
        if (isTake == true)
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = zDistance;
            Vector3 MouseWorld = moving.ScreenToWorldPoint(mouse);
            MouseWorld.y = 5.2f;
            this.transform.position = MouseWorld;
        }
    }
}
