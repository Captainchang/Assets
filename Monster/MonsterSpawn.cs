using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject turtle_prefab;
    [SerializeField]
    GameObject Mino_prefab;

    [SerializeField]
    Transform Turtlespawnposition;
    [SerializeField]
    Transform Minospawnposition;


    private void Start()
    {
        Instantiate(turtle_prefab, Turtlespawnposition,true);
        Instantiate(Mino_prefab, Minospawnposition,true);
    }
}
