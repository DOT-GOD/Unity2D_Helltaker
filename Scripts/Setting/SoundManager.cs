using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public void Play(GameObject soundObject)
    {
        GameObject tempSound;
        tempSound = GameObject.Instantiate(soundObject, this.transform.position, Quaternion.identity);
        tempSound.transform.rotation = this.transform.rotation;
        tempSound.SetActive(true);
        Destroy(tempSound, 1.0f);
        Destroy(this.gameObject, 1.0f);
    }
    public void Play(GameObject soundObject, float time)
    {
        GameObject tempSound;
        tempSound = GameObject.Instantiate(soundObject, this.transform.position, Quaternion.identity);
        tempSound.transform.rotation = this.transform.rotation;
        tempSound.SetActive(true);
        Destroy(tempSound, time);
        Destroy(this.gameObject, time);
    }
}
