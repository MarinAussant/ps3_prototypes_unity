using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenValise : MonoBehaviour
{

    public bool isOpen = false;
    public float duration;
    
    public void OnOpenClick()
    {
        Debug.Log("yoyo");

        //transform.Rotate(new Vector3(-1,0,0), 90);
        if (!isOpen)
        {
            StartCoroutine(SmoothOpen(duration, new Vector3(0, -90, -90)));
            Camera.main.GetComponent<CameraMovement>().MovementToValise(duration);
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

}
