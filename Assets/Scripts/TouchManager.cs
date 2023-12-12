using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{

    public bool isTouching;
    public bool isJustClicking;

    public bool isSwipeRight;
    public bool isSwipeLeft;
    public bool isSwipeUp;
    public bool isSwipeDown;

    public bool isDragging;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    public float timeToSwipe = 0.7f;
    private float timer = 0f;


    // Update is called once per frame
    void Update()
    {

        // Start Touching
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            isTouching = true;
            startTouchPosition = Input.GetTouch(0).position;




        }

        if (isTouching)
        {
            timer += Time.deltaTime; 
        }
        else
        {
            isSwipeDown = false;
            isSwipeUp = false;
            isSwipeLeft = false;
            isSwipeRight = false;
            isJustClicking = false;
        }


        // End Touching
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isTouching = false;
            endTouchPosition = Input.GetTouch(0).position;

            if(timer <= timeToSwipe)
            {
                // Verif Swipe Down
                if (endTouchPosition.y < startTouchPosition.y - 500 && endTouchPosition.x < startTouchPosition.x + 250 && endTouchPosition.x > startTouchPosition.x - 250)
                {

                    isSwipeDown = true;
                    Debug.Log("SwipeD");

                }
                // Verif Swipe Up
                if (endTouchPosition.y > startTouchPosition.y + 500 && endTouchPosition.x < startTouchPosition.x + 250 && endTouchPosition.x > startTouchPosition.x - 250)
                {

                    isSwipeUp = true;
                    Debug.Log("SwipeU");

                }
                // Verif Swipe Left
                if (endTouchPosition.x < startTouchPosition.x - 500 && endTouchPosition.y < startTouchPosition.y + 250 && endTouchPosition.y > startTouchPosition.y - 250)
                {

                    isSwipeLeft = true;
                    Debug.Log("SwipeL");

                }
                // Verif Swipe Right
                if (endTouchPosition.x > startTouchPosition.x + 500 && endTouchPosition.y < startTouchPosition.y + 250 && endTouchPosition.y > startTouchPosition.y - 250)
                {

                    isSwipeRight = true;
                    Debug.Log("SwipeR");

                }

            }

            if (endTouchPosition.y < startTouchPosition.y + 100 && endTouchPosition.y > startTouchPosition.y - 100 && endTouchPosition.x < startTouchPosition.x + 100 && endTouchPosition.x > startTouchPosition.x - 100)
            {
                //isJustClicking = true;
                Debug.Log("JusteClick");
            }

            timer = 0f;

        }

    }

    public bool lastTouchIsClick()
    {
        if (endTouchPosition.y < startTouchPosition.y + 100 && endTouchPosition.y > startTouchPosition.y - 100 && endTouchPosition.x < startTouchPosition.x + 100 && endTouchPosition.x > startTouchPosition.x - 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 

    public void StartDragging()
    {
        
    }

}
