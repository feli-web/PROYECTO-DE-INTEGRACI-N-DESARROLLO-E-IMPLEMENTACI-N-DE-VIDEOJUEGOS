using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int numberOfShots;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;
    public bool canShoot;
    private Camera cam;
    public LevelTextCreator levelTextCreator;

    void Start()
    {
        numberOfShots = levelTextCreator.bullets;
        cam = Camera.main;
        canShoot = true;
    }

    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            Aim(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
            transform.rotation = Quaternion.identity;
        }

#else

    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        Aim(touch.position);

        if (touch.phase == TouchPhase.Ended)
        {
            Shoot();
        }
    }

#endif
    }

    void Aim(Vector3 screenPosition)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPosition);
        worldPos.z = 0f;

        Vector2 direction = worldPos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void Shoot()
    {
        if (numberOfShots > 0 && canShoot == true)
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * bulletSpeed;
            numberOfShots--;
            canShoot = false;
        }
    }
}
