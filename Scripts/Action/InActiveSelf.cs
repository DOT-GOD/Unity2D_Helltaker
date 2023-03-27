using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InActiveSelf : MonoBehaviour
{
    public void InActive()
    {
        this.gameObject.SetActive(false);
    }
}
