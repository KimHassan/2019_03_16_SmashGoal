using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsPlayer : MonoBehaviour
{
    Rigidbody rigidbody;

    float speed = 30;

    Vector3 p;

    public Vector3 originPos;
    public Quaternion originRot;

    public Vector3 originPos2;

    public GameObject leftArm;
    public GameObject rightArm;

    Quaternion leftArmRot1 = Quaternion.Euler(-60, -160, 266);
    Quaternion rightArmRot1 = Quaternion.Euler(55, 155, 80);

    Quaternion leftArmRot2 = Quaternion.Euler(50,200,145);

    Quaternion rightArmRot2 = Quaternion.Euler(-50, 150, -30);

    // Start is called before the first frame update

    void Start()
    {
         rigidbody = GetComponent<Rigidbody>();

         p = transform.position;

         originPos = new Vector3(0, 2, 20);
         originRot = Quaternion.Euler(0, 0, 0);

        originPos2 = originPos;

        originPos2.y += 10;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            transform.position = p;
        }
    }

    public void Go(float _angle,float _power)
    {
        leftArm.transform.localRotation = Quaternion.identity;
        leftArm.transform.localRotation = leftArmRot2;

        
        rightArm.transform.localRotation = Quaternion.identity;
        rightArm.transform.localRotation = rightArmRot2;

        transform.rotation = Quaternion.Euler(new Vector3(0,0, _angle));

        Vector3 vector = Quaternion.AngleAxis(_angle, Vector3.forward) * Vector3.up;

        if (_power > 100)
            _power = 100;


        Debug.Log("power:" + _power);

        rigidbody.AddForce(vector * _power, ForceMode.Impulse);

    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.tag);
    }

    public void RespawnUpdate(GameObject gm)
    {
        transform.position = Vector3.MoveTowards(transform.position, originPos, Time.deltaTime * 50f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, originRot, Time.deltaTime * 50f);
        if (transform.position == GetComponent<CsPlayer>().originPos &&
            transform.rotation == GetComponent<CsPlayer>().originRot)
        {

            gm.GetComponent<CsGameManager>().isRespawn = false;
            GetComponent<CapsuleCollider>().enabled = true;

            rigidbody.isKinematic = false;
        }
    }
    public void RespawnStart(bool isGoal)
    {
        if(isGoal)
            CsEffectManager.instance.SetBombEffect(transform.position);
        else
            CsEffectManager.instance.SetClearEffect(transform.position);
    }
    public void RespawnStart2()
    {
        GetComponent<CapsuleCollider>().enabled = false;

        rigidbody.isKinematic = true;

        transform.position = originPos2;
        transform.rotation = originRot;


        leftArm.transform.localRotation = leftArmRot1;

        rightArm.transform.localRotation = rightArmRot1;

    }

    public void NuckBack()
    {
        Vector3 vector = new Vector3(0, 0, 1);

       rigidbody.AddForce(vector * 100f, ForceMode.Impulse);
    }
}
