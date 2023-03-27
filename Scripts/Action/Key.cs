using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject _unlockEffect;                // �����ʿ� : ������� ȿ�� ������Ʈ
    public GameObject _keyPickUpSFX;                // �����ʿ� : ���� �ݱ� ȿ���� ������Ʈ

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player _playerComponent = other.gameObject.GetComponent<Player>();
            _playerComponent._haveKey = true;

            // Űȹ�� ����Ʈ
            GameObject tempEffect;
            tempEffect = GameObject.Instantiate(_unlockEffect, this.transform.position, Quaternion.identity);
            tempEffect.transform.rotation = this.transform.rotation;
            tempEffect.SetActive(true);
            Destroy(tempEffect, 1.0f);

            // ����
            GameObject _sound = new GameObject();
            SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
            _soundComponent.Play(_keyPickUpSFX);

            Destroy(this.gameObject);
        }

    }
}
