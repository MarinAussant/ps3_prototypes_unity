using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenValise : MonoBehaviour
{

    public bool isOpen = false;

    // Detection swipe
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    public float duration;

    private void Update()
    {
        if (isOpen)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;

                if (endTouchPosition.y < startTouchPosition.y)
                {
                  
                    if (Camera.main.GetComponent<CameraMovement>().onDownValise)
                    {
                        Camera.main.GetComponent<CameraMovement>().MovementToValiseTop(duration/2);
                    }

                }

                if (endTouchPosition.y > startTouchPosition.y)
                {
                    
                    if (Camera.main.GetComponent<CameraMovement>().onDownValise)
                    {
                        Camera.main.GetComponent<CameraMovement>().MovementToInitial(duration);
                        StartCoroutine(SmoothClose(duration, new Vector3(0, -90, -90)));
                    }

                    if (Camera.main.GetComponent<CameraMovement>().onTopValise)
                    {
                        Camera.main.GetComponent<CameraMovement>().MovementToValiseDown(duration/2);
                    }

                }
            }
        }
    }
    public void OnOpenClick()
    {

        //transform.Rotate(new Vector3(-1,0,0), 90);
        if (!isOpen)
        {
            StartCoroutine(SmoothOpen(duration, new Vector3(0, -90, -90)));
            Camera.main.GetComponent<CameraMovement>().MovementToValiseDown(duration);
        }
       
    }

    

    IEnumerator SmoothOpen(float duration, Vector3 degres)
    {
        float timeStamp = 0f;

        while (timeStamp < duration)
        {

            transform.rotation = Quaternion.Lerp(
            transform.rotation,
            new Quaternion(degres.x, degres.y, degres.z, 0),
            Time.deltaTime*timeStamp / (duration*10) 
            );

            

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.rotation = new Quaternion(degres.x, degres.y, degres.z, 0);
        isOpen = true;
    }

    /*IEnumerator SmoothOpen(float duration, Vector3 degres)
    {
        float timeStamp = 0f;

        while (timeStamp < duration)
        {

            transform.rotation = Quaternion.Lerp(
            transform.rotation,
            new Quaternion(degres.x, degres.y, degres.z, 0),
            Time.deltaTime * timeStamp / (duration * 10)
            );



            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.rotation = new Quaternion(degres.x, degres.y, degres.z, 0);
        isOpen = true;
    }
    */
}
