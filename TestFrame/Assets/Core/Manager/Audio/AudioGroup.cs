using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioGroup
{
    private AudioMixerGroup _mixer;
    public AudioMixerGroup Mixer { get { return _mixer; } }

    public AudioGroup(AudioMixerGroup mixer)
    {
        _mixer = mixer;
    }
}
