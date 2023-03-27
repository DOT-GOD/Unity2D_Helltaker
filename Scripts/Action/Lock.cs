using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject _unlockEffect;
    public GameObject _lockOpenSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // 언락 이펙트
            GameObject tempEffect;
            tempEffect = GameObject.Instantiate(_unlockEffect, this.transform.position, Quaternion.identity);
            tempEffect.transform.rotation = this.transform.rotation;
            tempEffect.SetActive(true);
            Destroy(tempEffect, 1.0f);

            // 사운드
            GameObject _sound = new GameObject();
            SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
            _soundComponent.Play(_lockOpenSFX);

            Destroy(this.gameObject);
        }

    }
}
