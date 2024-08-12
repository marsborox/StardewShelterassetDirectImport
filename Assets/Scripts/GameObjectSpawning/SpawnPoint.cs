using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] public Transform spawnPointTransform;
    public string spawnPointName;
    public int spawnPointNumber;

    public void SetName(string newName) 
    {
        spawnPointName = newName;
    }
    public Transform ReturnTransform()
    {
        return this.transform;
    }
}
