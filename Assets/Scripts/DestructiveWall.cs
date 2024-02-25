using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class DestructiveWall : MonoBehaviour, IBulletTarget
{
    public void Hit()
    {
        AudioPlayer.Instance.PlayInterruptableSFX(SFXType.WallDestruction);
        Destroy(gameObject);
    }
}
