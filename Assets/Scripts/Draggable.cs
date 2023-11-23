using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Draggable : MonoBehaviour
{

    public bool isDragging = false;
    public bool isHovering = false;

    private Vector3 touchPosition;

    public GameObject himSelfPrevizualisation;
    private GameObject tempPreview;
    private GameObject receptacle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        StartDrag();
        EndDrag();

        if (isDragging)
        {
            touchPosition = TouchPosition();
            transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z); 
        }

    }

    public Vector3 TouchPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, Camera.main.nearClipPlane + 2.5f));
    }

    public void StartDrag()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, Camera.main.farClipPlane)), out var info))
                {
                    if(info.collider.gameObject == gameObject)
                    {
                        isDragging = true;

                        if (receptacle)
                        {
                            if (receptacle.GetComponent<ObjectContainer>().objects[0] == gameObject)
                            {
                                receptacle.GetComponent<ObjectContainer>().objects.Clear();
                                receptacle = null;
                            }
                        }
                    }
                }
                
            }
        }
    }

    public void EndDrag()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                isDragging = false;

                if (receptacle)
                {
                    if(isHovering && receptacle.GetComponent<ObjectContainer>().objects.Count == 0)
                    {
                        transform.position = new Vector3(tempPreview.transform.position.x, tempPreview.transform.position.y, tempPreview.transform.position.z - 0.1f);
                        transform.rotation = tempPreview.transform.rotation;
                        receptacle.GetComponent<ObjectContainer>().objects.Add(gameObject);
                        Debug.Log(receptacle.GetComponent<ObjectContainer>().Verify());
                        Destroy(tempPreview);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider objects)
    {
        if(objects.gameObject.tag == "TriggerHoverReceptacle")
        {
            isHovering = true;
            tempPreview = Instantiate(himSelfPrevizualisation, objects.transform.position, Quaternion.Euler(0, 0, 0));
            tempPreview.transform.position = objects.transform.parent.position;
            tempPreview.transform.rotation = objects.transform.rotation;
            receptacle = objects.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit(Collider objects)
    {
        if (objects.gameObject.tag == "TriggerHoverReceptacle")
        {
            isHovering = false;
            Destroy(tempPreview);
        }
    }
}
