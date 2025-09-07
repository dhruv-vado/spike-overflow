using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpikePointer : MonoBehaviour
{
    public Slider spikeSlider;
    public Transform spikeSliderTrans;
    public float rate;
    public Trajectory trajectory;
    public GameObject red;
    public GameObject green;

    bool isStopped = false;

    public void GameStart()
    {
        isStopped = false;
        spikeSlider.value = 0;
    }

    private void Update()
    {
        //movement of the slider
        if (!isStopped)
        {
            if (spikeSlider.value >= 0)
            {
                spikeSlider.value += rate * Time.deltaTime;
            }


            if (spikeSlider.value == 1)
            {
                //Spike Failed.
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

                if (Physics.Raycast(spikeSliderTrans.position, Vector3.forward, out hit, 1))
                {
                    if (hit.collider.gameObject.name == green.name)
                    {
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

            if (Physics.Raycast(spikeSliderTrans.position, Vector3.forward, out hit, 1))
            {
                if (hit.collider.gameObject.name == green.name)
                {
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
