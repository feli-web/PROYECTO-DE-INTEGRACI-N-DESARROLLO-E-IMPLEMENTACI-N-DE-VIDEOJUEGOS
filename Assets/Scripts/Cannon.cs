using TMPro;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int numberOfShots;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject pointer;
    public float bulletSpeed = 15f;
    public bool canShoot;

    private Camera cam;

    public LevelTextCreator levelTextCreator;
    public TextMeshProUGUI bulletCountText;

    private bool isAiming;

    private void Start()
    {
        numberOfShots = levelTextCreator.bullets;
        bulletCountText.text = numberOfShots.ToString();

        cam = Camera.main;
        canShoot = true;

        pointer.SetActive(false);
    }

    public void BeginAim(Vector2 screenPosition)
    {
        if (!canShoot)
            return;

        isAiming = true;
        pointer.SetActive(true);

        UpdateAim(screenPosition);
    }

    public void UpdateAim(Vector2 screenPosition)
    {
        if (!isAiming || !canShoot)
            return;

        Vector3 worldPos = cam.ScreenToWorldPoint(screenPosition);
        worldPos.z = 0f;

        Vector2 direction = worldPos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    public void EndAim(Vector2 screenPosition)
    {
        if (!isAiming || !canShoot)
            return;

        isAiming = false;

        pointer.SetActive(false);

        Shoot();

        transform.rotation = Quaternion.identity;
    }

    private void Shoot()
    {
        if (numberOfShots <= 0)
            return;

        GameObject bullet =
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity =
            firePoint.up * bulletSpeed;

        numberOfShots--;

        bulletCountText.text = numberOfShots.ToString();

        canShoot = false;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            isAiming = false;
            pointer.SetActive(false);
        }
    }
}