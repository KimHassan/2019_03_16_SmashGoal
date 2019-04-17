using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSoundManager : MonoBehaviour
{

    public static CsSoundManager instance;
   AudioSource myAudio;

   AudioClip bombSound;

    AudioClip jumpSound;

    AudioClip skillSound;

    AudioClip peopleSound;

    AudioClip kickSound;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        myAudio = this.GetComponent<AudioSource>();

        skillSound = Resources.Load<AudioClip>("Sound/SkillSound");

        bombSound = Resources.Load<AudioClip>("Sound/BombSound");

        jumpSound = Resources.Load<AudioClip>("Sound/BombSound3");

        peopleSound = Resources.Load<AudioClip>("Sound/PeopleSound");

        kickSound = Resources.Load<AudioClip>("Sound/KickSound");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySkillSound()
    {
        myAudio.PlayOneShot(skillSound);
    }

    public void PlayBombSound()
    {
        myAudio.PlayOneShot(jumpSound);
    }

    public void PlayJumpSound()
    {
        myAudio.PlayOneShot(bombSound);
    }

    public void PlayPeopleSound()
    {
        myAudio.PlayOneShot(peopleSound);
    }

    public void PlayKickSound()
    {
        myAudio.PlayOneShot(kickSound);
    }

}
