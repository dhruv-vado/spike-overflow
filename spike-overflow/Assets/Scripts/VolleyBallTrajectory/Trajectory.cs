using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trajectory : MonoBehaviour
{
    [Header("Mechanism")]
    public Transform _ball;
    public Transform _target;
    public Transform _servePos;
    int passNo = 0;              //0 is serve, 1 is first pass to netter, 2 is set , 3 is spike, 4 is save
    public int turn = 1;         //to determine which team has the ball
    public int score1 = 0;
    public int score2 = 0;
    public float _speedServeFast;
    public float _angleServeFast;
    public float _speedServeSlow;
    public float _angleServeSlow;
    public float _speedFirstPass;
    public float _angleFirstPass;
    public float _speedSet;
    public float _angleSet;
    public float _speedSetSlowMo;
    public float _speedSpike;
    public float _angleSpike;
    public float _speedSave;
    public float _angleSave;
    public GameObject mainCam;
    public GameObject spikeCam;
    public bool correctPlayer = false;
    int _indexSpikeRec;
    bool _25Pts = true;
    bool isGameOver = true;

    [Header("Team 1")]
    public Transform _netter1;
    public Transform _LF1;
    public Transform _RF1;
    public Transform _LB1;
    public Transform _RB1;
    public Transform _reciever1;

    [Header("Team 2")]
    public Transform _netter2;
    public Transform _LF2;
    public Transform _RF2;
    public Transform _LB2;
    public Transform _RB2;
    public Transform _reciever2;

    [Header("Original Positions")]
    public Transform _ognetter2;
    public Transform _ogLF2;
    public Transform _ogRF2;
    public Transform _ogLB2;
    public Transform _ogRB2;
    public Transform _ogreciever2;
    public Transform _ognetter1;
    public Transform _ogLF1;
    public Transform _ogRF1;
    public Transform _ogLB1;
    public Transform _ogRB1;
    public Transform _ogreciever1;

    [Header("Camera Angles")]
    public Transform _camLF1;
    public Transform _camRF1;
    public Transform _camLF2;
    public Transform _camRF2;

    [Header("UI Elements")]
    public GameObject startPanel;
    public GameObject scoreboard;
    public GameObject playerButtons;
    public GameObject nextButton;
    public GameObject newGameButton;
    public GameObject newGameButton1;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI score1Text;
    public TextMeshProUGUI score2Text;
    public GameObject serveSlider;
    public GameObject spikeSlider;
    public GameObject blockSlider;
    public GameObject _25Toggle;
    public GameObject _15Toggle;
    public RectTransform _spikeSliderRect;
    public RectTransform _blockSliderRect;

    public void Start()
    {
        //Setting up the scene at start
        startPanel.SetActive(true);
        scoreboard.SetActive(false);
        nextButton.SetActive(true);
        newGameButton1.SetActive(true);
        winnerText.text = "";
        playerButtons.SetActive(false);
        serveSlider.SetActive(false);
        spikeSlider.SetActive(false);
        blockSlider.SetActive(false);

        //setting original positions of players
        _ognetter2.position = _netter2.position;
        _ogLF2.position = _LF2.position;
        _ogRF2.position = _RF2.position;
        _ogLB2.position = _LB2.position;
        _ogRB2.position = _RB2.position;
        _ogreciever2.position = _reciever2.position;
        _ognetter1.position = _netter1.position;
        _ogLF1.position = _LF1.position;
        _ogRF1.position = _RF1.position;
        _ogLB1.position = _LB1.position;
        _ogRB1.position = _RB1.position;
        _ogreciever1.position = _reciever1.position;
    }

    public void ResetPlayerPositions()
    {
        _netter2.position = _ognetter2.position;
        _LF2.position = _ogLF2.position;
        _RF2.position = _ogRF2.position;
        _LB2.position = _ogLB2.position;
        _RB2.position = _ogRB2.position;
        _reciever2.position = _ogreciever2.position;
        _netter1.position = _ognetter1.position;
        _LF1.position = _ogLF1.position;
        _RF1.position = _ogRF1.position;
        _LB1.position = _ogLB1.position;
        _RB1.position = _ogRB1.position;
        _reciever1.position = _ogreciever1.position;
    }

    public void GameStart()
    {
        //setting the serve position of player
        if (turn == 1)
        {
            _reciever1.position = _reciever1.position + new Vector3(-4.25f,0f,0f);
            _reciever2.position = _ogreciever2.position;
            _servePos.position = _reciever1.position + new Vector3(0.5f, 0f, 0f);
        }
        else if (turn == 2)
        {
            _reciever2.position = _reciever2.position + new Vector3(4.25f, 0f, 0f);
            _reciever1.position = _ogreciever1.position;
            _servePos.position = _reciever2.position + new Vector3(-0.5f, 0f, 0f);
        }
        _ball.position = _servePos.position;
        serveSlider.SetActive(true);
        spikeSlider.SetActive(false);
        blockSlider.SetActive(false);
        startPanel.SetActive(false);
        scoreboard.SetActive(false);
        playerButtons.SetActive(false);
        mainCam.SetActive(true);
        spikeCam.SetActive(false);
        correctPlayer = false;

        //if one of the teams have enough points to win then restart the game
        if(isGameOver)
        {
            score1 = 0;
            score2 = 0;
            isGameOver = false;
            newGameButton.SetActive(false);
            nextButton.SetActive(true);
            newGameButton1.SetActive(true);
            winnerText.text = "";
        }

        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
    }

    public void NewGame()
    {
        isGameOver = true;
        startPanel.SetActive(true);
        scoreboard.SetActive(false);
        nextButton.SetActive(true);
        newGameButton1.SetActive(true);
        winnerText.text = "";
        playerButtons.SetActive(false);
        serveSlider.SetActive(false);
        spikeSlider.SetActive(false);
        blockSlider.SetActive(false);
    }
     
    public void ButtenPressed(Transform _player) //transform assigned in editor
    {
        //comparing if the button our user pressed is on the correct player
        if ((turn == 1 && _target.position == _player.position + new Vector3(0.5f, 0f, 0f)) || (turn == 2 && _target.position == _player.position + new Vector3(-0.5f, 0f, 0f)))
        {
            correctPlayer = true;
            playerButtons.SetActive(false);
        }
        else
        {
            correctPlayer = false;
            playerButtons.SetActive(false);
        }
    }

    public void Serve(float angle, float _speedFactor)
    {
        angle = angle * Mathf.Deg2Rad;

        mainCam.SetActive(true);
        spikeCam.SetActive(false);
        blockSlider.SetActive(false);

        passNo = 0;
        correctPlayer = false;

        playerButtons.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Coroutine_Movement(angle, Random.Range(0, 3), Random.Range(3, 5), _speedFactor, passNo));
    }

    public void FirstPass(float _speedFactor)
    {
        float angle = _angleFirstPass * Mathf.Deg2Rad;

        passNo = 1;
        correctPlayer = false;

        StopAllCoroutines();
        StartCoroutine(Coroutine_Movement(angle, Random.Range(0, 3), Random.Range(3, 5), _speedFactor, passNo));
    }

    public void Set(float _speedFactor)
    {
        float angle = _angleSet * Mathf.Deg2Rad;

        passNo = 2;
        correctPlayer = false;

        StopAllCoroutines();
        StartCoroutine(Coroutine_Movement(angle, Random.Range(0, 3), Random.Range(3, 5), _speedFactor, passNo));
    }

    public void Spike(float _speedFactor)
    {
        float angle = _angleSpike * Mathf.Deg2Rad;

        passNo = 3;
        correctPlayer = false;

        //changing the block slider position according to which team's turn it is
        if (turn == 1)
        {
            _blockSliderRect.anchoredPosition = new Vector2( 1075f, 0f);
        }
        else if (turn == 2)
        {
            _blockSliderRect.anchoredPosition = new Vector2(-1075f, 0f);
        }

        spikeSlider.SetActive(false);
        blockSlider.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Coroutine_Movement(angle, Random.Range(0, 3), Random.Range(3, 5), _speedFactor, passNo));
    }

    public void SpikeSave(float _speedFactor)
    {
        float angle = _angleSave * Mathf.Deg2Rad;

        passNo = 4;
        correctPlayer = false;

        StopAllCoroutines();
        StartCoroutine(Coroutine_Movement(angle, Random.Range(0, 3), Random.Range(3, 5), _speedFactor, passNo));
    }

    public IEnumerator Coroutine_Movement(float angle, int _indexRec,int _indexSpike, float _speedFactor, int pass)
    {
        //Calculation of velocity and time of flight of the ball
        float t = 0f;
        float t_flight = 0f;
        float slope = 0f;
        float phi = 0f;
        float v0 = 0f;

        Transform[] team1Transforms = { _LB1, _RB1, _reciever1, _LF1, _RF1, _netter1 };
        Transform[] team2Transforms = { _RB2, _LB2, _reciever2, _RF2, _LF2, _netter2 };
        Transform[] ogteam1Transforms = { _ogLB1, _ogRB1, _ogreciever1, _ogLF1, _ogRF1, _ognetter1 };
        Transform[] ogteam2Transforms = { _ogRB2, _ogLB2, _ogreciever2, _ogRF2, _ogLF2, _ognetter2 };

        switch (pass)
        {
            case 0:
                if (turn == 1)
                {
                    _target.position = team2Transforms[_indexRec].position + new Vector3(-0.5f, 0f, 0f);  //so that ball doesnt go inside the player
                    turn = 2;
                }
                else if (turn == 2)
                {
                    _target.position = team1Transforms[_indexRec].position + new Vector3(0.5f, 0f, 0f);  //so that ball doesnt go inside the player
                    turn = 1;
                }

                _ball.position = _servePos.position;

                slope = (_target.position.z - _servePos.position.z) / (_target.position.x - _servePos.position.x);
                phi = Mathf.Atan(slope);
                float d1 = Mathf.Sqrt(Mathf.Pow(_target.position.x - _servePos.position.x, 2) + Mathf.Pow(_target.position.z - _servePos.position.z, 2));

                v0 = Mathf.Sqrt(Mathf.Pow(d1, 2) * _speedFactor / (2 * Mathf.Abs(_target.position.y - _servePos.position.y - (Mathf.Tan(angle) * d1)) * Mathf.Pow(Mathf.Cos(angle), 2)));

                t_flight = d1 / (v0 * Mathf.Cos(angle));

                break;

            case 1:
                _servePos.position = _target.position;
                _ball.position = _servePos.position;

                if (turn == 1)
                {
                    _target.position = team1Transforms[5].position + new Vector3(0.5f, 0f, 0f);
                }
                else if (turn == 2)
                {
                    _target.position = team2Transforms[5].position + new Vector3(-0.5f, 0f, 0f);
                }

                slope = (_target.position.z - _servePos.position.z) / (_target.position.x - _servePos.position.x);
                phi = Mathf.Atan(slope);
                float d2 = Mathf.Sqrt(Mathf.Pow(_target.position.x - _servePos.position.x, 2) + Mathf.Pow(_target.position.z - _servePos.position.z, 2));

                v0 = Mathf.Sqrt(Mathf.Pow(d2, 2) * _speedFactor / (2 * Mathf.Abs(_target.position.y - _servePos.position.y - (Mathf.Tan(angle) * d2)) * Mathf.Pow(Mathf.Cos(angle), 2)));

                t_flight = d2 / (v0 * Mathf.Cos(angle));

                break;

            case 2:
                _servePos.position = _target.position;
                _ball.position = _servePos.position;
                _indexSpikeRec = _indexSpike;

                //setting new camera for spike
                if (turn == 1)
                {
                    _target.position = ogteam1Transforms[_indexSpike].position + new Vector3(0.5f, 2.75f, 0f);
                    if (_indexSpike == 3)
                    {
                        spikeCam.transform.position = _camLF1.position;
                        spikeCam.transform.rotation = _camLF1.rotation;
                    }
                    else if(_indexSpike == 4)
                    {
                        spikeCam.transform.position = _camRF1.position;
                        spikeCam.transform.rotation = _camRF1.rotation;
                    }
                }
                else if (turn == 2)
                {
                    _target.position = ogteam2Transforms[_indexSpike].position + new Vector3(-0.5f, 2.75f, 0f);
                    if (_indexSpike == 3)
                    {
                        spikeCam.transform.position = _camRF2.position;
                        spikeCam.transform.rotation = _camRF2.rotation;
                    }
                    else if (_indexSpike == 4)
                    {
                        spikeCam.transform.position = _camLF2.position;
                        spikeCam.transform.rotation = _camLF2.rotation;
                    }
                }

                v0 = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(_target.position.z - _servePos.position.z),2) * _speedSetSlowMo / (2* Mathf.Abs(Mathf.Abs(_target.position.y - _servePos.position.y) - Mathf.Abs(Mathf.Tan(angle) * (_target.position.z - _servePos.position.z))) *Mathf.Pow(Mathf.Cos(angle),2)));

                t_flight = Mathf.Abs(_target.position.z - _servePos.position.z)/(v0*Mathf.Cos(angle));
                
                break;

            case 3:
                _servePos.position = _target.position;
                _ball.position = _servePos.position;

                if (turn == 1)
                {
                    _target.position = team2Transforms[_indexSpikeRec].position + new Vector3(-0.5f, 0f, 0f);
                    turn = 2;
                }
                else if (turn == 2)
                {
                    _target.position = team1Transforms[_indexSpikeRec].position + new Vector3(0.5f, 0f, 0f);
                    turn = 1;
                }

                slope = (_target.position.z - _servePos.position.z) / (_target.position.x - _servePos.position.x);
                phi = Mathf.Atan(slope);

                v0 = Mathf.Sqrt(Mathf.Abs(_target.position.x - _servePos.position.x) * _speedFactor / (Mathf.Sin(2 * angle) * Mathf.Pow(Mathf.Cos(phi), 2)));

                t_flight = 2 * v0 * Mathf.Sin(angle) * Mathf.Cos(phi) / _speedFactor;

                break;

            case 4:
                _servePos.position = _target.position;
                _ball.position = _servePos.position;

                if (turn == 1)
                {
                    _target.position = ogteam1Transforms[_indexRec].position + new Vector3(0.5f, 0f, 0f);
                }
                else if (turn == 2)
                {
                    _target.position = ogteam2Transforms[_indexRec].position + new Vector3(-0.5f, 0f, 0f);  
                }

                _ball.position = _servePos.position;

                slope = (_target.position.z - _servePos.position.z) / (_target.position.x - _servePos.position.x);
                phi = Mathf.Atan(slope);
                float d = Mathf.Sqrt(Mathf.Pow(_target.position.x - _servePos.position.x, 2) + Mathf.Pow(_target.position.z - _servePos.position.z, 2));

                v0 = Mathf.Sqrt(Mathf.Pow(d, 2) * _speedFactor / (2 * Mathf.Abs(_target.position.y - _servePos.position.y - (Mathf.Tan(angle) * d)) * Mathf.Pow(Mathf.Cos(angle), 2)));

                t_flight = d / (v0 * Mathf.Cos(angle));

                break;

            default:
                Debug.Log("Error in pass no.");
                break;
        }

        //Movement of the ball
        while (t <= t_flight)
        {
            float x = 0f;
            float y = 0f;
            float z = 0f;

            switch(pass)
            {
                case 0:
                    z = v0 * Mathf.Sin(phi) * t * Mathf.Cos(angle);
                    x = v0 * Mathf.Cos(angle) * t * Mathf.Cos(phi);
                    y = v0 * Mathf.Sin(angle) * t * Mathf.Cos(phi) - (1f / 2f) * _speedFactor * Mathf.Pow(t, 2);
                    if (turn == 1)
                    {
                        _ball.position = _servePos.position + new Vector3(-x, y, -z);
                    }
                    else if (turn == 2)
                    {
                        _ball.position = _servePos.position + new Vector3(x, y, z);
                    }

                    //ball rotates according to its y coordinate (slower when higher)
                    _ball.transform.Rotate(new Vector3((2 / (y + 0.1f)), (1 / (4 * y + 0.1f)), (2 / (y + 0.1f))));

                    break;

                case 1:
                    z = v0 * Mathf.Sin(phi) * t * Mathf.Cos(angle);
                    x = v0 * Mathf.Cos(angle) * t * Mathf.Cos(phi);
                    y = (v0 * Mathf.Sin(angle) * t - (1f / 2f) * _speedFactor * Mathf.Pow(t, 2));
                    if (turn == 1)
                    {
                        _ball.position = _servePos.position + new Vector3(x, y, z);
                    }
                    else if (turn == 2)
                    {
                        _ball.position = _servePos.position + new Vector3(-x, y, -z);
                    }

                    _ball.transform.Rotate(new Vector3((2 / (y + 1f)), (1 / (4 * y + 1f)), (2 / (y + 1f))));

                    break;

                case 2:
                    //when the ball completes 65% of its flight time, the camera angle changes
                    if (t <= 0.65*t_flight)
                    {
                        Time.timeScale = _speedSet;
                    }
                    else
                    {
                        Time.timeScale = 1f;
                        if (turn == 1)
                        {
                            team1Transforms[_indexSpike].position = ogteam1Transforms[_indexSpike].position + new Vector3(0f, 2.75f, 0f);
                            team2Transforms[_indexSpike].position = ogteam2Transforms[_indexSpike].position + new Vector3(0f, 2.75f, 0f);
                            _spikeSliderRect.anchoredPosition = new Vector2(-1075f, 0f);
                        }
                        else if (turn == 2)
                        {
                            team1Transforms[_indexSpike].position = ogteam1Transforms[_indexSpike].position + new Vector3(0f, 2.75f, 0f);
                            team2Transforms[_indexSpike].position = ogteam2Transforms[_indexSpike].position + new Vector3(0f, 2.75f, 0f);
                            _spikeSliderRect.anchoredPosition = new Vector2(1075f, 0f);
                        }
                        mainCam.SetActive(false);
                        spikeCam.SetActive(true);
                        playerButtons.SetActive(false);
                        spikeSlider.SetActive(true);
                    }
                    z = v0 * t * Mathf.Cos(angle) * Mathf.Sign(_target.position.z - _servePos.position.z);
                    y = v0 * Mathf.Sin(angle) * t * Mathf.Cos(phi) - (1f / 2f) * _speedSetSlowMo * Mathf.Pow(t, 2);
                    if (turn == 1)
                    {
                        _ball.position = _servePos.position + new Vector3(0f, y, z);
                    }
                    else if (turn == 2)
                    {
                        _ball.position = _servePos.position + new Vector3(0f, y, z);
                    }

                    _ball.transform.Rotate(new Vector3((2 / (y + 1f)), (4 / (y + 1f)), (2 / (y + 1f))));

                    break;

                case 3:
                    z = v0 * Mathf.Sin(phi) * t * Mathf.Cos(angle);
                    x = v0 * Mathf.Cos(angle) * t * Mathf.Cos(phi);
                    y = (v0 * Mathf.Sin(angle) * t * Mathf.Cos(phi) - (1f / 2f) * _speedFactor * Mathf.Pow(t, 2)) * Mathf.Sign(_target.position.y - _servePos.position.y);
                    if (turn == 1)
                    {
                        _ball.position = _servePos.position + new Vector3(-x, y, -z);
                    }
                    else if (turn == 2)
                    {
                        _ball.position = _servePos.position + new Vector3(x, y, z);
                    }

                    _ball.transform.Rotate(new Vector3((1 / (6 * y + 1f)), (1 / (8 * y + 1f)), (1 / (6 * y + 1f))));

                    break;

                case 4:
                    z = v0 * Mathf.Sin(phi) * t * Mathf.Cos(angle);
                    x = v0 * Mathf.Cos(angle) * t * Mathf.Cos(phi);
                    y = (v0 * Mathf.Sin(angle) * t - (1f / 2f) * _speedFactor * Mathf.Pow(t, 2));
                    if (turn == 1)
                    {
                        _ball.position = _servePos.position + new Vector3(-x, y, -z);
                    }
                    else if (turn == 2)
                    {
                        _ball.position = _servePos.position + new Vector3(x, y, z);
                    }

                    _ball.transform.Rotate(new Vector3((1 / (2 * y + 1f)), (1 / (8 * y + 1f)), (1 / (2 * y + 1f))));

                    break;

                default:
                    Debug.Log("Error in pass no.");
                    break;
            }

            t += Time.deltaTime;
            yield return null;
        }

        if(correctPlayer)
        {
            //if user clicked the correct player then call the function for next pass
            switch (passNo)
            {
                case 0:
                    FirstPass(_speedFirstPass);
                    ResetPlayerPositions();
                    playerButtons.SetActive(true);

                    break;
                case 1:
                    spikeSlider.GetComponent<SpikePointer>().GameStart();
                    blockSlider.GetComponent<BlockPointer>().GameStart();
                    Set(_speedSet);
                    ResetPlayerPositions();
                    playerButtons.SetActive(true);
                     
                    break;
                case 2:
                    Spike(_speedSpike);
                    break;
                case 3:
                    mainCam.SetActive(true);
                    spikeCam.SetActive(false);
                    blockSlider.SetActive(false);

                    SpikeSave(_speedSave);
                    ResetPlayerPositions();
                    playerButtons.SetActive(true);

                    break;
                case 4:
                    FirstPass(_speedFirstPass);
                    ResetPlayerPositions();
                    playerButtons.SetActive(true);

                    break;
                default:
                    Debug.Log("error in pass no.");
                    break;
            }
        }
        else
        {
            //increase score when player messed up
            ResetPlayerPositions();
            if (turn == 1)
            {
                score2++;
                score2Text.text = score2.ToString();
                turn = 2;
            }
            else if (turn == 2)
            {
                score1++;
                score1Text.text = score1.ToString();
                turn = 1;
            }
            if((score1 == 25 && _25Pts) || (score1 == 15 && !_25Pts))
            {
                nextButton.SetActive(false);
                newGameButton1.SetActive(false);
                newGameButton.SetActive(true);
                winnerText.text = "Team Red Won the Match!";
                isGameOver = true;
            }
            else if ((score2 == 25 && _25Pts) || (score2 == 15 && !_25Pts))
            {
                nextButton.SetActive(false);
                newGameButton1.SetActive(false);
                newGameButton.SetActive(true);
                winnerText.text = "Team Blue Won the Match!";
                isGameOver = true;
            }
            else
            {
                isGameOver = false;
            }
            scoreboard.SetActive(true);
            playerButtons.SetActive(true);
        }
    }
}
