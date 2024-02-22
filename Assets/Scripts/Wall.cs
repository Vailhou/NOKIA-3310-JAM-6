using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IBulletTarget
{
    public void Hit()
    {
        AudioSystem.Instance.PlaySFX(SFXType.WallDestruction);
        Destroy(gameObject);
    }
}
