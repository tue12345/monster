using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool isDragging;
    private Vector3 screenPoint;
    private Vector3 offset;
    
    GameObject go;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the mouse position
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction); // Cast the ray and get the object it hits

            if (hit.collider != null) // Check if the ray hits an object
            {
                go = hit.collider.gameObject;
            }
            else
            {
                go = null;
            }
            if (go&&GameManager.instance.selectedObject && GameManager.instance.ObjectSelected() == go)
            {
                
                isDragging = true;
                screenPoint = Camera.main.WorldToScreenPoint(transform.position);
                offset = go.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (isDragging)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                go.transform.position = curPosition;
            }
            
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            go = null;
        }
    }
    
   

  

}

