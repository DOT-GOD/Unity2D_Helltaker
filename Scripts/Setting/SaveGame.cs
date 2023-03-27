using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public GameObject _player;

    [SerializeField]
    [Range(0,3)]
    public int _checkPointNum;


    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
    }

    //충돌시 저장
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        //JsonManager _json = new JsonManager();
        JsonManager _json = this.gameObject.AddComponent<JsonManager>();
        _json.Save(_checkPointNum);


    }
    */


}
