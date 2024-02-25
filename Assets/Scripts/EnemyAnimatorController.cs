using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] Animator anim;
    void Start()
    {
        TryGetComponent(out anim);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("Time", ObjectMovements.timeScale);
    }
}
