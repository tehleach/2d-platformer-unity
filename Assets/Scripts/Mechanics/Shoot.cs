using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerCharacter))]
public class Shoot : MonoBehaviour {
    public PlayerCharacter parent;
    public GameObject bullet;
    public GameObject bulletIcon;
    List<GameObject> icons = new List<GameObject>();

    public int MaxBullets = 3;
    public int BulletCount = 3;
    public bool InfAmmo = false;
    public float CantPickupDuration = .5f;

    void Start() {
    }

    public bool CanRetrieve() {
        return InfAmmo || BulletCount < MaxBullets;
    }

    public void Retrieve(GameObject other) {
        BulletCount++;
        CreateIcons();
        Destroy(other);
    }

    public void CreateIcons() {
        foreach (GameObject icon in icons) {
            Destroy(icon);
        }
        icons.Clear();
        float xOffset = -0.05f * BulletCount / 2f;
        for (int i = 0; i < BulletCount; i++) {
            var icon = Instantiate(bulletIcon, new Vector3(parent.transform.position.x + xOffset, parent.transform.position.y + 1f, parent.transform.position.z), Quaternion.identity);
            icon.transform.parent = parent.transform;
            xOffset += .1f;
            icons.Add(icon);
        }
    }

    public void TryToShoot(Vector2 directionalInput, int dir) {
        if (BulletCount > 0 || InfAmmo) {
            Fire(directionalInput, dir);
            BulletCount--;
            CreateIcons();
        }
    }

    public void Fire(Vector2 directionalInput, int dir) {
        Vector2 directionToFire = directionalInput != Vector2.zero ? directionalInput : new Vector2(dir * 1f, 0f);
        Arrow bulletClone = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Arrow>();
        bulletClone.Fire(directionToFire.normalized);
        StartCoroutine(IgnoreCollisionTimer(bulletClone.GetComponent<Collider2D>(), parent.GetComponent<Collider2D>()));
    }

    IEnumerator IgnoreCollisionTimer(Collider2D bullet, Collider2D me) {
        Physics2D.IgnoreCollision(bullet, me, true);
        yield return new WaitForSeconds(CantPickupDuration);
        Physics2D.IgnoreCollision(bullet, me, false);
    }
}
