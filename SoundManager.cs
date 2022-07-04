/*
 * Graffiti Softwerks 2022
 * SoundManager.cs
 * Author: Nash Ali
 * Creation Date: 04-23-2022
 * Last Update: 04-30-2022
 * 
 * Copyright (c) Graffiti Softwerks 2022
 * 
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    #region defs & vars

    [SerializeField]
    private AudioSource ambiencePlayer, foleyPlayer, specialfxPlayer;
    [SerializeField]
    private AudioClip shipNoise, beamNoise, ambEasy, ambMed, ambHard, backgroundMusic, spawnSound;
    [SerializeField]
    public AudioMixer myMixer;
    [SerializeField]
    private Slider mainSlider, ambienceSlider, sfxSlider, foleySlider;

    #endregion vars

    #region MonoBehaviour ************************************************************************
    private void Start()
    {
        myMixer.GetComponent<AudioMixer>();
        ambiencePlayer.GetComponent<AudioSource>();
        foleyPlayer.GetComponent<AudioSource>();
        specialfxPlayer.GetComponent<AudioSource>();

        mainSlider.value = PlayerPrefs.GetFloat("MainVolume", mainSlider.value);
        ambienceSlider.value = PlayerPrefs.GetFloat("AmbienceVolume", ambienceSlider.value);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", sfxSlider.value);
        foleySlider.value = PlayerPrefs.GetFloat("FoleyVolume", sfxSlider.value);

    }
    #endregion *****************

    #region User Code ******************************************************************************
    /// <summary>
    /// Sets the main volume.
    /// </summary>
    /// <param name="sliderVolume"></param>
    public void SetMainVolume(float sliderVolume)
    {
        _ = myMixer.SetFloat("MasterVolume", Mathf.Log10(sliderVolume) * 30); 
    }
    /// <summary>
    /// Clears the main volume back to the last snapshot.
    /// </summary>
    public void ClearMainVolume()
    {
        _ = myMixer.ClearFloat("MasterVolume");
    }
    /// <summary>
    /// Sets the Ambience volume level
    /// </summary>
    /// <param name="sliderVolume"></param>
    public void SetAmbienceVolume(float sliderVolume)
    {
        _ = myMixer.SetFloat("AmbienceVolume", sliderVolume);
    }
    /// <summary>
    /// goes back to the last snapshot
    /// </summary>
    public void ClearAmbienceVolume()
    {
        _ = myMixer.ClearFloat("AmbienceVolume");
    }
    /// <summary>
    /// Set the SFX volume
    /// </summary>
    /// <param name="sliderVolume"></param>
    public void SetSFXVolume(float sliderVolume)
    {
        _ = myMixer.SetFloat("SFXVolume", sliderVolume);
    }
    /// <summary>
    /// goes back to the last snapshot
    /// </summary>
    public void ClearSFXVolume()
    {
        _ = myMixer.ClearFloat("SFXVolume");
    }
    /// <summary>
    /// Set the SFX volume
    /// </summary>
    /// <param name="sliderVolume"></param>
    public void SetFoleyVolume(float sliderVolume)
    {
        _ = myMixer.SetFloat("FoleyVolume", sliderVolume);
    }
    /// <summary>
    /// goes back to the last snapshot
    /// </summary>
    public void ClearFoleyVolume()
    {
        _ = myMixer.ClearFloat("FoleyVolume");
    }
    /// <summary>
    /// Play the easy ambience music
    /// </summary>
    public void PlayAmbienceEasy()
    {
        ambiencePlayer.loop = true;
        ambiencePlayer.Play();
    }
    /// <summary>
    /// Play the medium level ambience music
    /// </summary>
    public void PlayAmbienceMedium()
    {
        ambiencePlayer.loop = true;
        ambiencePlayer.Play();
    }
    /// <summary>
    /// plays the hard ambience music.
    /// </summary>
    public void PlayAmbienceHard()
    {
        ambiencePlayer.loop = true;
        ambiencePlayer.Play();
    }
    /// <summary>
    /// play spawn sound - one shot
    /// </summary>
    public void PlaySpawnSound()
    {
        specialfxPlayer.PlayOneShot(spawnSound);
    }
    /// <summary>
    /// play ufo arriving
    /// </summary>
    public void UfoArrive()
    {
        specialfxPlayer.PlayOneShot(shipNoise);
    }
    /// <summary>
    /// Play the transport sound.
    /// </summary>
    public void BeamMeUpSound()
    {
        specialfxPlayer.PlayOneShot(beamNoise);
    }
    /// <summary>
    /// play the sfx specified
    /// </summary>
    /// <param name="sfx"></param>
    public void PlaySFX(AudioClip sfx)
    {
        specialfxPlayer.PlayOneShot(sfx);
    }
    /// <summary>
    /// play the foley specified.
    /// </summary>
    /// <param name="foley"></param>
    public void PlayFoley(AudioClip foley)
    {
        foleyPlayer.PlayOneShot(foley);
    }
    #endregion *******************************************************
}
