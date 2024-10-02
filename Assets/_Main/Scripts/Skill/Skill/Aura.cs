using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// skill tạo vùng gây sát thương xung quanh
public class Aura : Skill
{
    [SerializeField] private float damageRange;

    private void Update()
    {
        
    }

    private void AuraDame()
    {
        Collider[] aura = Physics.OverlapSphere(transform.position, damageRange);
        foreach(Collider collider in aura)
        {
            // nhận sát thương
        }
    }
}
