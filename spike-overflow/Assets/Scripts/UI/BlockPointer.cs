using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockPointer : MonoBehaviour
{
    public Slider blockSlider;
    public Transform serveSliderTrans;
    public float rate;
    public Trajectory trajectory;
    public GameObject red;
    public GameObject yellow;
    public GameObject green;

    bool isStopped = false;

    public void GameStart()
    {
        isStopped = false;
        blockSlider.value = 0;
    }

    private void Update()
    {
        //movement of slider
        if (!isStopped)
        {
            if (blockSlider.value >= 0)
            {
                blockSlider.value += rate * Time.deltaTime;
            }


            if (blockSlider.value == 1)
            {
                //Block Failed.
                isStopped = true;
                trajectory.correctPlayer = false;
            }
        }

        //gets input from user and stops the pointer when user takes the finger off
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                isStopped = true;

                RaycastHit hit;

                if (Physics.Raycast(serveSliderTrans.position, Vector3.forward, out hit, 1))
                {
                    if (hit.collider.gameObject.name == green.name)
                    {
                        //successful block sends the ball to opp team
                        trajectory.Serve(0f, 8f);
                    }

                    if (hit.collider.gameObject.name == yellow.name)
                    {
                        //yellow block sends the ball to blocking team
                        trajectory.correctPlayer = true;
                    }

                    if (hit.collider.gameObject.name == red.name)
                    {
                        trajectory.correctPlayer = false;
                    }
                }
            }
        }

        //for testing in pc
        if (Input.GetMouseButtonUp(0))
        {
            isStopped = true;

            RaycastHit hit;

            if (Physics.Raycast(serveSliderTrans.position, Vector3.forward, out hit, 1))
            {
                if (hit.collider.gameObject.name == green.name)
                {
                    //successful block sends the ball to opp team
                    trajectory.Serve(0f, 8f);
                }

                if (hit.collider.gameObject.name == yellow.name)
                {
                    //yellow block sends the ball to blocking team
                    trajectory.correctPlayer = true;
                }

                if (hit.collider.gameObject.name == red.name)
                {
                    trajectory.correctPlayer = false;
                }
            }
        }
    }
}