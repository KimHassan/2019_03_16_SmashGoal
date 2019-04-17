using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsGameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject ball;
    private float GoalTimer;

    Vector2 firstFingerPosition;

    Vector2 lastFingerPosition;

    float angle;

    float swipeDistanceX;
    float swipeDistanceY;

    float SWIPE_DISTANCE_X_CONST = 60;
    float SWIPT_DISTANCE_Y_CONST = 150;

    int touchFingerId = -1;

    public bool isRespawn = false;

    bool isMoved = false;

    private void Start()
    {
        GoalTimer = 0;
        isRespawn = false;
    }

    private void Update()
    {
        RespawnUpdate();
        if (isMoved == true)
            return;

#if (UNITY_EDITOR || UNITY_STANDALONE)
        {
            
            MouseUpdate();
            //Restart();
            
        }
#elif (UNITY_ANDROID)
        {
        TouchUpdate();
        }
#endif
        
    }



    public void RespawnUpdate()
    {
        if (isRespawn == true)
        {
            player.GetComponent<CsPlayer>().RespawnUpdate(this.gameObject);
            if(isRespawn == false)
            {
                ball.GetComponent<BallScript>().Reset();
            }
        }
        else
        {
            if (ball.GetComponent<BallScript>().IsCollide)
            {
                GoalTimer += Time.deltaTime;
                if(GoalTimer >= 2 && GoalTimer <3)
                {
                    player.GetComponent<CsPlayer>().RespawnStart(ball.GetComponent<BallScript>().IsGoal);
                    GoalTimer = 3;
                }
                if (GoalTimer >= 4)
                {
                    if (ball.GetComponent<BallScript>().IsGoal == false)
                    {
                        CsUIControll.instance.GetScore();

                        isRespawn = true;

                        player.GetComponent<CsPlayer>().RespawnStart2();

                        GoalTimer = 0;

                        isMoved = false;
                    }
                    else
                    {
                        CsUIControll.instance.Die();

                        GoalTimer = 0;

                        isMoved = false;
                    }
                    
                }

            }
        }
    }

    private void MouseUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {

            firstFingerPosition = Input.mousePosition;
            lastFingerPosition = Input.mousePosition;

        }
        if (Input.GetMouseButton(0))
        {

            lastFingerPosition = Input.mousePosition;
            swipeDistanceX = Mathf.Abs((lastFingerPosition.x - firstFingerPosition.x));
            swipeDistanceY = Mathf.Abs((lastFingerPosition.y - firstFingerPosition.y));
        }
        if (Input.GetMouseButtonUp(0))
        {


            angle = Mathf.Atan2((lastFingerPosition.x - firstFingerPosition.x), (lastFingerPosition.y - firstFingerPosition.y)) * Mathf.Rad2Deg;


            float distance = Vector3.Distance(firstFingerPosition, lastFingerPosition);

            float power = distance / 10;

            if (distance < 10)
                return;

            player.GetComponent<CsPlayer>().Go(angle, power);

            CsEffectManager.instance.SetJumpEffect(player.transform.position);

            isMoved = true;
        }

    }

    private void TouchUpdate()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchFingerId = touch.fingerId;
                firstFingerPosition = touch.position;
                lastFingerPosition = touch.position;
                break;

            case TouchPhase.Moved:
                if (touch.fingerId == touchFingerId)
                {
                    lastFingerPosition = touch.position;
                    swipeDistanceX = Mathf.Abs((lastFingerPosition.x - firstFingerPosition.x));
                    swipeDistanceY = Mathf.Abs((lastFingerPosition.y - firstFingerPosition.y));
                }
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (touch.fingerId == touchFingerId)
                {
                    touchFingerId = -1;
                    angle = Mathf.Atan2((lastFingerPosition.x - firstFingerPosition.x), (lastFingerPosition.y - firstFingerPosition.y)) * Mathf.Rad2Deg;


                    float distance = Vector3.Distance(firstFingerPosition, lastFingerPosition);

                    float power = distance / 10;

                    if (distance < 10)
                        return;

                    player.GetComponent<CsPlayer>().Go(angle, power);

                    CsEffectManager.instance.SetJumpEffect(player.transform.position);

                    isMoved = true;

                }
                break;
        }
    }


}
