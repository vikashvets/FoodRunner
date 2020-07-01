using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour

{
    public AudioMixer MasterMixer;
    public AudioSource Example;

    //Saving volume level after change
    public void SetEffectsLvl(float EffectsLevel)
    {
        MasterMixer.SetFloat("EffVol", EffectsLevel);
    }

    public void SetMusicLvl(float MusicLvl)
    {
        MasterMixer.SetFloat("MusicVol", MusicLvl);
    }
    public void SetMasterLvl(float MasterLvl)
    {
        MasterMixer.SetFloat("MasterVol", MasterLvl);
    }

    public void ClearVolume()
    {
        MasterMixer.ClearFloat("MusicVol");
    }

    //When a player changes the volume level of effects in the menu, an example effect is played so that the player can hear what volume level after the change.
    public void ExamplePlay ()
    {
        MasterMixer.SetFloat("Examples", GetLevels("EffVol"));
        Example.Play();
    }

    float GetLevels(string name)
    {
        float value;
        bool result = MasterMixer.GetFloat(name, out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }
}
