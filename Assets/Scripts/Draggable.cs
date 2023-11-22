using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour, ITouchable
{
    public void OnTouchedDown(Vector3 touchPosition)
    {
        //throw new System.NotImplementedException();
    }

    public void OnTouchedStay(Vector3 touchPosition)
    {
        //throw new System.NotImplementedException();
        Debug.Log(touchPosition);
        Debug.Log(transform.position);
        transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);
    }

    public void OnTouchedUp()
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.DrawLine(_mainCamera.transform.position, touchePosInWorld, Color.red);


        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out var info, 15)) {
                Debug.Log("yo");
            }
        }
    }
}
