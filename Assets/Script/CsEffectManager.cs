using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsEffectManager : MonoBehaviour
{
    static public CsEffectManager instance;

    public CsCamera camera;
    // public GameObject effect;

    // Start is called before the first frame update

   
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetSkillEffect(Vector3 _position)
    {
        GameObject effect;

        effect = Resources.Load<GameObject>("Effect/skillAttack");

        effect = Instantiate<GameObject>(effect, _position, Quaternion.identity);

        Destroy(effect, 1);

        StartCoroutine(camera.Shake(1, 0.2f));

        CsSoundManager.instance.PlaySkillSound();
        CsSoundManager.instance.PlayPeopleSound();

    }

    public void SetBombEffect(Vector3 _position)
    {
        GameObject effect;

        effect = Resources.Load<GameObject>("Effect/BombEffect");

        effect = Instantiate<GameObject>(effect, _position, Quaternion.identity);

        Destroy(effect, 1);

        //StartCoroutine(camera.Shake(1, 0.2f));

        CsSoundManager.instance.PlayBombSound();

    }

    public void SetClearEffect(Vector3 _position)
    {
        //GameObject effect;

        //effect = Resources.Load<GameObject>("Effect/ClearEffect");

        //effect = Instantiate<GameObject>(effect, _position, Quaternion.identity);

        ////effect.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        //Destroy(effect, 1);

        ////StartCoroutine(camera.Shake(1, 0.2f));

    }
    public void SetJumpEffect(Vector3 _position)
    {
        GameObject effect;

        effect = Resources.Load<GameObject>("Effect/JumpEffect");

        effect = Instantiate<GameObject>(effect, _position, Quaternion.identity);

        Destroy(effect, 1);

        StartCoroutine(camera.Shake(0.1f, 0.03f));

        CsSoundManager.instance.PlayJumpSound();

    }

}


