using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioSource audioSource;
    public AudioSource softJump;
    public AudioSource lightsOn;
    public AudioSource lightsOff;
    public AudioSource keyCollect;
    public AudioSource walkSounds;
    public AudioSource doorSounds;
    public AudioSource finalJumpSound;
    public AudioSource ambient;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneInfo.OnLightsChange += PlayLightSwitchSound;
    }

    private void OnDisable()
    {
        SceneInfo.OnLightsChange -= PlayLightSwitchSound;
    }

    public void PlaySoftJump()
    {
        softJump.Play();
    }

    public void PlayLightSwitchSound(bool lightState)
    {
        if (lightState)
        {
            lightsOn.Play();
        }
        else
        {
            lightsOff.Play();
        }
    }

    public void PlayKeyCollect()
    {
        keyCollect.Play();
    }

    public void PlayLockedDoor()
    {
        if(doorSounds.isPlaying == false)
        {
            doorSounds.Play();
        }
    }

    public void PlayWalk(bool state)
    {
        if (state)
        {
            if (walkSounds.isPlaying == true) return;
            walkSounds.Play();
        }
        else
        {
            if(walkSounds.isPlaying)
            walkSounds.Stop();
        }
    }

    public void PlayFinalJump()
    {
        finalJumpSound.Play();
        Debug.Log("Jump!");
    }

    public void StopAllSounds()
    {
        ambient.Stop();
        audioSource.Stop();
        softJump.Stop();
        lightsOn.Stop();
        lightsOff.Stop();
        keyCollect.Stop();
        walkSounds.Stop();
        doorSounds.Stop();
}
}
