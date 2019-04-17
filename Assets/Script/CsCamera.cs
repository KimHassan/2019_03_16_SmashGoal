using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsCamera : MonoBehaviour
{
    public float shakeTimer = 0; //흔들림 효과 시간 
    public float shakeAmount = 5f; //흔들림 범위

    Vector3 originPos;
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
    }



    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }
        transform.localPosition = originPos;

    }

}
