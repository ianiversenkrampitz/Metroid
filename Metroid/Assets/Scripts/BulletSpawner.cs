using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iversen-Krampitz, Ian 
//10/20/2023
//Controls the bullet spawner.
public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnrate = 1f;
    public bool shootRight = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootBullet", 0, spawnrate);
    }
    private void ShootBullet()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.GetComponent<Bullet>().goingRight = shootRight;
    }
}
