using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    public AudioClip FinalFightBGM;
    public AudioClip BGM;
    public AudioClip[] BattleBGM;
    public AudioSource BGMsfx;
    public AudioSource BattleBGMsfx;
    public AudioSource Effectsfx;
    public AudioClip HitBtn;
    

	void Start () {
        if (instance == null)
            instance = this;
	}
    
    public void SwitchToBGM()
    {
        BattleBGMsfx.Stop();
        BGMsfx.UnPause();
    }
    public void SwitchToBattleBGM()
    {
        BGMsfx.Pause();
        if(GameManager.instance.currentNum==20)
        {
            BattleBGMsfx.clip = FinalFightBGM;
        }
        else
            BattleBGMsfx.clip = BattleBGM[Random.Range(0,6)];
        BattleBGMsfx.Play();
    }
    public void PlayBtnSound()
    {
        Effectsfx.PlayOneShot(HitBtn);
    }
}
