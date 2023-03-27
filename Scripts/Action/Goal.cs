using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool _isCerberus = false;                  // 지정필요 : 케르베로스(3마리)인지 여부
    public GameObject _winEffect;                     // 지정필요 : 승리시 빛이 모이는 효과 오브젝트
    public GameObject _lovePlosionEffect;             // 지정필요 : 승리시 빛이 터지는 효과 오브젝트
    public GameObject _demon;                         // 지정필요 : 악마 스프라이트 오브젝트
    public GameObject _loveSign;                      // 지정필요 : 하트표시 오브젝트

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
            // 승리 이펙트
            StartCoroutine(PlayEffect(_winEffect, 0, 4));
            StartCoroutine(PlayEffect(_lovePlosionEffect, 2, 2));

            // 승리시 하얘지는 효과
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

    // waitNum초 후에 Effect재생, destroyNum초 후에 파괴
    IEnumerator PlayEffect(GameObject Effect, float waitNum, float destroyNum)
    {
        yield return new WaitForSeconds (waitNum);

        
        Debug.Log("파티클실행");
        GameObject tempEffect;
        tempEffect = GameObject.Instantiate(Effect, _demon.transform.position, Quaternion.identity);
        tempEffect.transform.rotation = this.transform.rotation;
        tempEffect.SetActive(true);
        Destroy(tempEffect, destroyNum);

        // 케르베로스 예외처리(이펙트 두 개 더 표시)
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
