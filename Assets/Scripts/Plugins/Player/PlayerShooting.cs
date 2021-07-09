 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static bool canShoot=true;
    public static bool isShooting;
    
    // for melee only
    public float timeBwAttack = 2f;
    public float knockbackDuration, knockbackPower;
    public Vector2 range;

    private float timeBtwShots;
    private float timeShooting;

    public Transform shootPoint;
    private Animator animator;

    public WeaponData weaponData;
    private PlayerBehaviour PB;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = 0;
        PB = GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (weaponData != null)
        {
            timeShooting = weaponData.delayTime;
            if (Time.time >= timeBtwShots)
            {
                StartCoroutine(Shoot());
            }
        }
        if (Ability1.isUsingSkill1)
        {
            weaponData = Resources.Load<WeaponData>("DataBase/Special/CallOfVoid");
            animator= gameObject.transform.GetChild(4).GetComponentInChildren<Animator>();
        }
        else if (!Ability1.isUsingSkill1)
        {
            if (gameObject.transform.GetChild(0).GetComponentInChildren<Weapon>() != null)
            {
                weaponData = gameObject.transform.GetChild(0).GetComponentInChildren<Weapon>().d_weapondata;
                animator = gameObject.transform.GetChild(0).GetComponentInChildren<Animator>();
            }
            else
            {
                weaponData = null;
            }
        }
    }
    IEnumerator Shoot()
    {
        if (PB.currentMana>=weaponData.mana)
        {
            if (Input.GetMouseButton(0) && isShooting == false && !Ability1.isUsingSkill1 && canShoot)
            {
                isShooting = true;
                if (weaponData.shootAniCtrl != null)
                {
                    animator.SetBool("isAttacking", true);
                }
                if(weaponData.cbStyle != CombatStyle.Melee)
                {
                    for (int k = 0; k < weaponData.timesEachShot; k++)
                    {
                        if (weaponData.bulletsEachShot % 2 == 0)
                        {
                            for (int i = 0; i <= weaponData.bulletsEachShot - 1; i++)
                            {
                                Vector3 spreadAngle = Vector3.zero;
                                if (i % 2 == 0)
                                {
                                    spreadAngle = new Vector3(0, 0, (i + 1) * 5);
                                }
                                else if (i % 2 == 1)
                                {
                                    spreadAngle = new Vector3(0, 0, (i + 1) * -5);
                                }
                                GameObject bullet = Instantiate(weaponData.bulletType, shootPoint.position, shootPoint.rotation);
                                bullet.transform.Rotate(spreadAngle);
                            }
                        }
                        else if (weaponData.bulletsEachShot % 2 == 1 && weaponData.bulletsEachShot != 1)
                        {
                            Instantiate(weaponData.bulletType, shootPoint.position, shootPoint.rotation);
                            for (int i = 0; i <= weaponData.bulletsEachShot - 2; i++)
                            {
                                Vector3 spreadAngle = Vector3.zero;
                                if (i % 2 == 0)
                                {
                                    spreadAngle = new Vector3(0, 0, (i + 1) * 5);
                                }
                                else if (i % 2 == 1)
                                {
                                    spreadAngle = new Vector3(0, 0, i * -5);
                                }
                                GameObject bullet = Instantiate(weaponData.bulletType, shootPoint.position, shootPoint.rotation);
                                bullet.transform.Rotate(spreadAngle);
                            }
                        }
                        else if (weaponData.bulletsEachShot == 1)
                        {
                            Instantiate(weaponData.bulletType, shootPoint.position, shootPoint.rotation);
                        }
                        yield return new WaitForSeconds(timeShooting);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                    Vector3 mousePosition = Input.mousePosition; ;
                    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    mousePosition.z = 0;
                    DebugDrawBox(new Vector2(((range.x / 2) * (mousePosition.x - transform.position.x) / Mathf.Sqrt(Mathf.Pow(mousePosition.y - transform.position.y, 2) + Mathf.Pow(mousePosition.x - transform.position.x, 2))) + transform.position.x, ((range.y / 2) * (mousePosition.y - transform.position.y) / Mathf.Sqrt(Mathf.Pow(mousePosition.y - transform.position.y, 2) + Mathf.Pow(mousePosition.x - transform.position.x, 2))) + transform.position.y), new Vector2(range.x, range.y), Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg, Color.blue, 0.1f);
                    LayerMask thingstocut = LayerMask.GetMask("whatIsEnemy", "whatIsEnemyBullet","destroyableBlock");
                    Collider2D[] hit = Physics2D.OverlapBoxAll(new Vector2(((range.x / 2) * (mousePosition.x - transform.position.x) / Mathf.Sqrt(Mathf.Pow(mousePosition.y - transform.position.y, 2) + Mathf.Pow(mousePosition.x - transform.position.x, 2))) + transform.position.x, ((range.y / 2) * (mousePosition.y - transform.position.y) / Mathf.Sqrt(Mathf.Pow(mousePosition.y - transform.position.y, 2) + Mathf.Pow(mousePosition.x - transform.position.x, 2))) + transform.position.y), new Vector2(range.x, range.y), Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg,thingstocut);
                    foreach(Collider2D thing in hit)
                    {
                        if(thing.gameObject.layer == 14)
                        {
                            EnemyController EC = thing.transform.GetComponent<EnemyController>();
                            int critChance = Random.Range(1, 101);
                            StartCoroutine(EC.PushedBack(knockbackDuration,knockbackPower,transform));
                            if (critChance > weaponData.crit)
                            {
                                EC.TakeDamage(weaponData.damage);
                            }
                            else
                            {
                                EC.TakeDamage(weaponData.damage * 2);
                            }
                        }
                        else if(thing.gameObject.layer == 17)
                        {
                            Destroy(thing.gameObject);
                        }
                        else if(thing.gameObject.layer == 18)
                        {
                            thing.GetComponent<Block>().isAttacked = true;
                        }

                    }
                    yield return new WaitForSeconds(timeShooting);
                }
                PB.currentMana -= weaponData.mana;
                timeBtwShots = Time.time + weaponData.timeBtwShots;
                isShooting = false;
                if (weaponData.shootAniCtrl != null)
                {
                    animator.SetBool("isAttacking", false);
                }

            }
            else if (Input.GetMouseButton(0) && isShooting == false && Ability1.isUsingSkill1)
            {
                isShooting = true;
                if (weaponData.shootAniCtrl != null)
                {
                    animator.SetBool("isAttacking", true);
                }
                Instantiate(weaponData.bulletType, shootPoint.position, shootPoint.rotation);
                timeBtwShots = Time.time + weaponData.timeBtwShots;
                GetComponent<Ability1>().StartCoroutine(GetComponent<Ability1>().EndPhase1());
                yield return new WaitForSeconds(timeShooting);
                isShooting = false;
                if (weaponData.shootAniCtrl != null)
                {
                    animator.SetBool("isAttacking", false);
                }
            }
        }
    }
    void DebugDrawBox(Vector2 point, Vector2 size, float angle, Color color, float duration)
    {

        var orientation = Quaternion.Euler(0, 0, angle);

        // Basis vectors, half the size in each direction from the center.
        Vector2 right = orientation * Vector2.right * size.x / 2f;
        Vector2 up = orientation * Vector2.up * size.y / 2f;

        // Four box corners.
        Vector2 topLeft = point + up - right;
        Vector2 topRight = point + up + right;
        Vector2 bottomRight = point - up + right;
        Vector2 bottomLeft = point - up - right;

        // Now we've reduced the problem to drawing lines.
        Debug.DrawLine(topLeft, topRight, color, duration);
        Debug.DrawLine(topRight, bottomRight, color, duration);
        Debug.DrawLine(bottomRight, bottomLeft, color, duration);
        Debug.DrawLine(bottomLeft, topLeft, color, duration);
    }
}