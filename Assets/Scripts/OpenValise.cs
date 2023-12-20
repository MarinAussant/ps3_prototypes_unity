using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenValise : MonoBehaviour
{

    public bool isOpen = false;

    private TouchManager touchManager;

    public GameObject locketHaut;
    public GameObject locketBat;

    public float duration;

    private void Start()
    {
        touchManager = FindAnyObjectByType<TouchManager>();
    }

    private void Update()
    {
        if (isOpen)
        {
            if (touchManager.isSwipeDown)
            {
                if (Camera.main.GetComponent<CameraMovement>().onDownValise)
                {
                    Camera.main.GetComponent<CameraMovement>().MovementToValiseTop(duration / 2);
                }
            }

            if (touchManager.isSwipeUp) 
            {
                if (Camera.main.GetComponent<CameraMovement>().onDownValise)
                {
                    Camera.main.GetComponent<CameraMovement>().MovementToInitial(duration);
                    //StartCoroutine(SmoothOpen(duration, new Vector3(-90, 180, 0)));
                    StartCoroutine(SmoothClose(duration, new Vector3(-90, 180, 0)));
                }

                if (Camera.main.GetComponent<CameraMovement>().onTopValise)
                {
                    Camera.main.GetComponent<CameraMovement>().MovementToValiseDown(duration / 2);
                }
            }
        }
    }
    public void OnOpenClick()
    {
        if (!isOpen)
        {
            StartCoroutine(LockOpenHaut(duration / 2, new Vector3(-210, 180, 0)));
            StartCoroutine(LockOpenBas(duration / 2, new Vector3(0, 180, 0)));
            Camera.main.GetComponent<CameraMovement>().MovementToValiseDown(duration*1.5f);

        }
       
    }

    IEnumerator LockOpenHaut(float duration, Vector3 degres)
    {


        float timeStamp = 0f;
        Quaternion rotationActuelle = locketHaut.transform.rotation;

        while (timeStamp < duration)
        {

            locketHaut.transform.rotation = Quaternion.Lerp(
            rotationActuelle,
            Quaternion.Euler(degres),
            timeStamp / duration
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        locketHaut.transform.rotation = Quaternion.Euler(degres);
        if (isOpen) isOpen = false;
        else isOpen = true;

        StartCoroutine(SmoothOpen(duration*2, new Vector3(-180, 180, 0)));
        //Camera.main.GetComponent<CameraMovement>().MovementToValiseDown(duration*2);
    }

    IEnumerator LockOpenBas(float duration, Vector3 degres)
    {


        float timeStamp = 0f;
        Quaternion rotationActuelle = locketBat.transform.rotation;

        while (timeStamp < duration)
        {

            locketBat.transform.rotation = Quaternion.Lerp(
            rotationActuelle,
            Quaternion.Euler(degres),
            timeStamp / duration
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        locketBat.transform.rotation = Quaternion.Euler(degres);
        if (isOpen) isOpen = false;
        else isOpen = true;
    }

    IEnumerator LockCloseHaut(float duration, Vector3 degres)
    {


        float timeStamp = 0f;
        Quaternion rotationActuelle = locketHaut.transform.rotation;

        while (timeStamp < duration)
        {

            locketHaut.transform.rotation = Quaternion.Lerp(
            rotationActuelle,
            Quaternion.Euler(degres),
            timeStamp / duration
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        locketHaut.transform.rotation = Quaternion.Euler(degres);
        if (isOpen) isOpen = false;
        else isOpen = true;

    }

    IEnumerator LockCloseBas(float duration, Vector3 degres)
    {


        float timeStamp = 0f;
        Quaternion rotationActuelle = locketBat.transform.rotation;

        while (timeStamp < duration)
        {

            locketBat.transform.rotation = Quaternion.Lerp(
            rotationActuelle,
            Quaternion.Euler(degres),
            timeStamp / duration
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        locketBat.transform.rotation = Quaternion.Euler(degres);
        if (isOpen) isOpen = false;
        else isOpen = true;

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

        StartCoroutine(LockCloseBas(duration / 2, new Vector3(-90, 180, 0)));
        StartCoroutine(LockCloseHaut(duration / 2, new Vector3(-90, 180, 0)));
        transform.rotation = Quaternion.Euler(degres);
        isOpen = false;
    }
}
