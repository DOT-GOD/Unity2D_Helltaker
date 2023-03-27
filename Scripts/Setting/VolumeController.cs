using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    // 1 = master, 2 = BGM, 3 = SFX
    [SerializeField]
    [Range(1, 3)]
    public int _volumeType = 1;

    [SerializeField]
    [Range(0.0001f, 1f)]
    public float _volume = 0.8f;

    public AudioMixer _mixer;

    void Start()
    {
    }

    void Update()
    {
        if (_volumeType == 1)
            _mixer.SetFloat("Master", Mathf.Log10(_volume) * 20);
        else if (_volumeType == 2)
            _mixer.SetFloat("BGM", Mathf.Log10(_volume) * 20);
        else if (_volumeType == 3)
            _mixer.SetFloat("SFX", Mathf.Log10(_volume) * 20);
    }
}
