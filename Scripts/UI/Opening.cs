using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{
    public GameObject _title;

    void Start()
    {
        StartCoroutine(Open(3f));
    }

    void Update()
    {
        
    }

    IEnumerator Open(float wait)
    {
        yield return new WaitForSeconds(wait);

        _title.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
