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

                if (endTouchPosition.y < startTouchPosition.y - 500 && endTouchPosition.x < startTouchPosition.x +250 && endTouchPosition.x > startTouchPosition.x - 250)
                {
                
                    if (Camera.main.GetComponent<CameraMovement>().onDownValise)
                    {
                        Camera.main.GetComponent<CameraMovement>().MovementToValiseTop(duration/2);
                    }

                }

                if (endTouchPosition.y > startTouchPosition.y + 500 && endTouchPosition.x < startTouchPosition.x + 250 && endTouchPosition.x > startTouchPosition.x - 250)
                {
                    
                    if (Camera.main.GetComponent<CameraMovement>().onDownValise)
                    {
                        Camera.main.GetComponent<CameraMovement>().MovementToInitial(duration);
                        StartCoroutine(SmoothOpen(duration, new Vector3(0, 180, 0)));
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
        if (!isOpen)
        {
            StartCoroutine(SmoothOpen(duration, new Vector3(-90, 180, 0)));
            Camera.main.GetComponent<CameraMovement>().MovementToValiseDown(duration);
        }
       
    }

    

    IEnumerator SmoothOpen(float duration, Vector3 degres)
    {
        float timeStamp = 0f;
        Quaternion rotationActuelle = transform.rotation;

        while (timeStamp < duration)
        {

            transform.rotation = Quaternion.Lerp(
            rotationActuelle,
            Quaternion.Euler(degres),
            timeStamp / duration
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.rotation = Quaternion.Euler(degres);
        if (isOpen) isOpen = false;
        else isOpen = true;
    }

    IEnumerator SmoothClose(float duration, Vector3 degres)
    {
        float timeStamp = 0f;
        Quaternion rotationActuelle = transform.rotation;

        while (timeStamp < duration)
        {

            transform.rotation = Quaternion.Lerp(
            rotationActuelle,
            Quaternion.Euler(degres),
            timeStamp / duration
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        transform.rotation = Quaternion.Euler(degres);
        isOpen = false;
    }
}
