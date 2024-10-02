using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class BaseCard : ScriptableObject
{

    [SerializeField] private int _idCard;
    [SerializeField] private string _nameCard;
    [SerializeField] private Sprite _sprite;

    public void Use()
    {

    }
}
