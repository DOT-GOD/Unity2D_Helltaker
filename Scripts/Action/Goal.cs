using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool _isCerberus = false;                  // �����ʿ� : �ɸ����ν�(3����)���� ����
    public GameObject _winEffect;                     // �����ʿ� : �¸��� ���� ���̴� ȿ�� ������Ʈ
    public GameObject _lovePlosionEffect;             // �����ʿ� : �¸��� ���� ������ ȿ�� ������Ʈ
    public GameObject _demon;                         // �����ʿ� : �Ǹ� ��������Ʈ ������Ʈ
    public GameObject _loveSign;                      // �����ʿ� : ��Ʈǥ�� ������Ʈ

    public Material flashWhite;
    private Material defaultMaterial;

    private SpriteRenderer[] _renderers = null;

    private void Start()
    {
        _renderers = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        if(_renderers != null)
            defaultMaterial = _renderers[0].material;
    }

    public void Win()
    {
        if (_renderers != null)
        {
            // �¸� ����Ʈ
            StartCoroutine(PlayEffect(_winEffect, 0, 4));
            StartCoroutine(PlayEffect(_lovePlosionEffect, 2, 2));

            // �¸��� �Ͼ����� ȿ��
            foreach (SpriteRenderer SR in _renderers)
            {
                SR.material = flashWhite;
            }
            Invoke("ResetMaterial", 2.0f);

            _loveSign.gameObject.SetActive(false);
            Invoke("Inactive", 2.0f);
            Invoke("Active", 5.0f);
        }
    }

    // waitNum�� �Ŀ� Effect���, destroyNum�� �Ŀ� �ı�
    IEnumerator PlayEffect(GameObject Effect, float waitNum, float destroyNum)
    {
        yield return new WaitForSeconds (waitNum);

        
        Debug.Log("��ƼŬ����");
        GameObject tempEffect;
        tempEffect = GameObject.Instantiate(Effect, _demon.transform.position, Quaternion.identity);
        tempEffect.transform.rotation = this.transform.rotation;
        tempEffect.SetActive(true);
        Destroy(tempEffect, destroyNum);

        // �ɸ����ν� ����ó��(����Ʈ �� �� �� ǥ��)
        if (_isCerberus)
        {
            GameObject tempEffect2;
            tempEffect2 = GameObject.Instantiate(Effect, this.transform.position + new Vector3(-1,0.3f,0), Quaternion.identity);
            tempEffect2.transform.rotation = this.transform.rotation;
            tempEffect2.SetActive(true);
            Destroy(tempEffect2, destroyNum);

            GameObject tempEffect3;
            tempEffect3 = GameObject.Instantiate(Effect, this.transform.position + new Vector3(-2, 0.3f, 0), Quaternion.identity);
            tempEffect3.transform.rotation = this.transform.rotation;
            tempEffect3.SetActive(true);
            Destroy(tempEffect3, destroyNum);
        }
    }

    private void Inactive()
    {
        _demon.gameObject.SetActive(false);
    }
    private void Active()
    {
        _demon.gameObject.SetActive(true);
        _loveSign.gameObject.SetActive(true);
    }

    void ResetMaterial()
    {
        foreach (SpriteRenderer SR in _renderers)
        {
            SR.material = defaultMaterial;
        }
    }
}
