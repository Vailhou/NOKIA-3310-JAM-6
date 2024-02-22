using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletCreatorController : MonoBehaviour
{
    [SerializeField] private BulletController bulletPrefab;
    public void Fire(Vector2 position, Vector2 direction)
    {
        var bullet = Instantiate(bulletPrefab, position, new Quaternion());
        bullet.direction = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
