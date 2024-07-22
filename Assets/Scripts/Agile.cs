using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Agile : Bot
{
    [field: SerializeField]
    public override int HP { get; set; }
    [field: SerializeField]
    public override int DMG { get; set; }

    CapsuleCollider Aoe;
    private void Awake()
    {
        Aoe = GetComponent<CapsuleCollider>();
        Aoe.isTrigger = true;
        Aoe.radius = 3f;
    }
    public override void Attack()
    {

    }

}
