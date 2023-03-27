using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPointType
{
    Player,
    Monster,
    Rock,
    Thorn,
    Lock,
    Key
}

public class SpawnPointData : MonoBehaviour
{
    public SpawnPointType _type = SpawnPointType.Player;

    public bool _isUpDownThorn = false;
    public bool _isBurrowedThorn = false;
}
