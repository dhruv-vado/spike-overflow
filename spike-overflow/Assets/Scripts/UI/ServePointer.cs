using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServePointer : MonoBehaviour
{
    public Slider serveSlider;
    public Transform serveSliderTrans;
    public float rate;
    public GameObject red;
    public GameObject yellow;
    public GameObject green;
    public Trajectory trajectory;
    public GameObject serveSliderObj;
    public GameObject playerButtons;

    bool toggle;
    bool isTouched = false;
    bool isStopped;

    public void GameStart()
    {
        isTouched = false;
        isStopped = false;
        serveSlider.value = 0;
    }

    private void Update()
    {
        //movement of slider
        if (!isStopped)
        {
            if (toggle)
            {
                if (serveSlider.value >= 0)
                {
                    serveSlider.value += rate * Time.deltaTime;
                    toggle = true;
                }

                if (serveSlider.value == 1)
                {
                    toggle = false;
                }
            }

            if (!toggle)
            {
                if (serveSlider.value >= 0)
                {
                    serveSlider.value -= rate * Time.deltaTime;
                    toggle = false;
                }

                if (serveSlider.value == 0)
                {
                    toggle = true;
                }
            }
        }

        //gets input from user and stops the pointer when user taps 
        if (!isTouched)
        {
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
                            trajectory.Serve(trajectory._angleServeFast, trajectory._speedServeFast);
                            isTouched = true;
                            playerButtons.SetActive(true);
                            serveSliderObj.SetActive(false);
                        }
                        else if (hit.collider.gameObject.name == yellow.name)
                        {
                            trajectory.Serve(trajectory._angleServeSlow, trajectory._speedServeSlow);
                            isTouched = true;
                            playerButtons.SetActive(true);
                            serveSliderObj.SetActive(false);
                        }
                        else if (hit.collider.gameObject.name == red.name)
                        {
                            trajectory.serveSlider.SetActive(false);
                            Time.timeScale = 1f;
                            if (trajectory.turn == 1)
                            {
                                trajectory.score2++;
                                trajectory.score2Text.text = trajectory.score2.ToString();
                                trajectory.turn = 2;
                            }
                            else if (trajectory.turn == 2)
                            {
                                trajectory.score1++;
                                trajectory.score1Text.text = trajectory.score1.ToString();
                                trajectory.turn = 1;
                            }
                            trajectory.scoreboard.SetActive(true);
                            isTouched = true;
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
                        trajectory.Serve(trajectory._angleServeFast, trajectory._speedServeFast);
                        isTouched = true;
                        playerButtons.SetActive(true);
                        serveSliderObj.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == yellow.name)
                    {
                        trajectory.Serve(trajectory._angleServeSlow, trajectory._speedServeSlow);
                        isTouched = true;
                        playerButtons.SetActive(true);
                        serveSliderObj.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == red.name)
                    {
                        trajectory.serveSlider.SetActive(false);
                        Time.timeScale = 1f;
                        if (trajectory.turn == 1)
                        {
                            trajectory.score2++;
                            trajectory.score2Text.text = trajectory.score2.ToString();
                            trajectory.turn = 2;
                        }
                        else if (trajectory.turn == 2)
                        {
                            trajectory.score1++;
                            trajectory.score1Text.text = trajectory.score1.ToString();
                            trajectory.turn = 1;
                        }
                        trajectory.scoreboard.SetActive(true);
                        isTouched = true;
                    }
                }
            }
        }
    }
}
