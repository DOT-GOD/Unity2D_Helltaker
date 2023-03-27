using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject _unlockEffect;                // 지정필요 : 잠금해제 효과 오브젝트
    public GameObject _keyPickUpSFX;                // 지정필요 : 열쇠 줍기 효과음 오브젝트

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player _playerComponent = other.gameObject.GetComponent<Player>();
            _playerComponent._haveKey = true;

            // 키획득 이펙트
            GameObject tempEffect;
            tempEffect = GameObject.Instantiate(_unlockEffect, this.transform.position, Quaternion.identity);
            tempEffect.transform.rotation = this.transform.rotation;
            tempEffect.SetActive(true);
            Destroy(tempEffect, 1.0f);

            // 사운드
            GameObject _sound = new GameObject();
            SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
            _soundComponent.Play(_keyPickUpSFX);

            Destroy(this.gameObject);
        }

    }
}
