using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaObjectSO : ScriptableObject
{
    [SerializeField] public string Type;
    [SerializeField] public string Content;
    [SerializeField] public int Amount;
    public Sprite Sprite;
}
