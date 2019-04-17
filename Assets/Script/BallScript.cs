using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody rigid;
    //private float forceX;
    private float tempX;
    private bool IsCurve = false;
    public bool IsShoot = false;
    public bool IsCollide = false;
    private float CountX;
    public bool IsGoal = false;
    public int randNum;
    private int randDirection;
    private float randForcex;
    private float randForcey;
    private float randForcez;
    private bool OnGround;
    private bool IsGameStart = false;

    public void Reset()
    {
        transform.position = new Vector3(0, 1, -8);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        IsShoot = false;
        rigid.velocity = new Vector3(0, 0, 0);
        IsCurve = false;
        IsGoal = false;
        IsCollide = false;
        OnGround = false;
        tempX = 0.2f;
        randNum = 0;
        rigid.isKinematic = true;
        
        StartCoroutine(Shoot());
        
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        tempX = 0.2f;
        CountX = 0;
        randNum = 0;
        Physics.gravity = new Vector3(0, -30);
        OnGround = false;


        Reset();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& IsGameStart == false)
        {
            IsGameStart = true;
            StartCoroutine(Shoot());
        }


        switch (randNum)
        {
            case 0:
                break;
            case 1:
                Pattern1();
                break;
            case 2:
                Pattern2();
                break;
        }
    }

    IEnumerator Shoot()
    {
       
        yield return new WaitForSeconds(2.0f);

        rigid.isKinematic = false;

        randNum = Random.Range(1, 3);

        switch (randNum)
        {
            case 0:
                break;
            case 1:
                Pattern1();
                break;
            case 2:
                Pattern2();
                break;
        }

        CsSoundManager.instance.PlayKickSound();
    }

    void Pattern1()
    {
        if (IsShoot != true)
        {
            randForcex = Random.Range(0, 5);
            randForcez = Random.Range(26, 28);


            randDirection = Random.Range(0, 2);

            if (randDirection == 0)//0일때 골키퍼기준 오른쪽
                randForcex *= -1;

            rigid.AddForce(new Vector3(randForcex, 21, randForcez), ForceMode.Impulse); //x 6 //y 22 //z 23 30
            IsShoot = true;
        }
    }

    void Pattern2()
    {
        if (IsShoot != true)
        {
            randForcex = Random.Range(1, 1.5f);
            randForcey = Random.Range(22, 25);

            randDirection = Random.Range(0, 2);

            //Debug.Log(randDirection);
            if (randDirection == 0)
            {
                rigid.AddForce(new Vector3(-10, randForcey, 22), ForceMode.Impulse); //y: 21 25
            }
            if (randDirection == 1)
            {
                rigid.AddForce(new Vector3(10, randForcey, 22), ForceMode.Impulse); //y: 21 25
                randForcex *= -1;
            }

            IsShoot = true;
        }
        else if (IsShoot == true)
        {

            if (IsCurve == false)
            {
                tempX += Time.deltaTime;
            }
            if (tempX > 1.0f && IsCurve == false)
            {
                IsCurve = true;

                //Debug.Log("회전");
            }

            if (IsCurve == true && OnGround == false)
            {
                //Debug.Log("회전" + randForcex);
                rigid.AddForce(new Vector3(randForcex, 0, 0), ForceMode.Impulse); //1부터 1.8까지
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsCurve)
            OnGround = true;
        if (collision.transform.tag == "Player" && IsCollide == false)
        {
            IsCollide = true;
            rigid.velocity *= -10;

            CsEffectManager.instance.SetSkillEffect(transform.position);

            collision.transform.GetComponent<CsPlayer>().NuckBack();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Goal" && IsCollide == false)
        {
            IsCollide = true;
            IsGoal = true;
        }
    }

}

