using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpWeapon : MonoBehaviour
{
    private static readonly List<Transform> wpSlot = new List<Transform>();
    private Transform wpHolder1, wpHolder2, c_weapon, s_weapon;
    static public bool canPickUp = true; //to stop picking up wp while doing st in another script
    private static bool isPressed=false;
    private RectTransform wpInfoBar;
    public TextMeshProUGUI damage,crit,mana;

    // Start is called before the first frame update
    void Start()
    {
        wpInfoBar = GameObject.Find("WpInfoBar").GetComponent<RectTransform>();
        wpHolder1 = FindObjectOfType<PlayerMovement>().transform.GetChild(0);
        wpHolder2 = FindObjectOfType<PlayerMovement>().transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] wps = GameObject.FindGameObjectsWithTag("DroppedWeapon");
        Transform nearestWp = GetClosestWp(wps);
        if (Vector2.Distance(transform.position, nearestWp.position) <= 1.5f)
        {
            wpInfoBar.anchoredPosition = new Vector2(-25, -245);
            damage.text = nearestWp.GetComponent<Weapon>().d_weapondata.damage.ToString();
            crit.text = nearestWp.GetComponent<Weapon>().d_weapondata.crit.ToString();
            mana.text = nearestWp.GetComponent<Weapon>().d_weapondata.mana.ToString();
            if (nearestWp.CompareTag("DroppedWeapon"))
            {
                foreach (GameObject wp in wps)
                {
                    wp.GetComponent<Weapon>().Name.SetActive(false);
                }
                nearestWp.GetComponent<Weapon>().Name.SetActive(true);  
            }
            nearestWp.GetComponent<Weapon>().Name.GetComponent<Canvas>().sortingOrder=6;
            if (Input.GetKeyDown(KeyCode.R) && isPressed == false && canPickUp == true)
            {
                isPressed = true;
                if (wpSlot.Count == 0)
                {
                    PickUp(nearestWp);
                }
                else if (wpSlot.Count == 1)
                {
                    SwapWeaponWithHand();
                    PickUp(nearestWp);
                }
                else if (wpSlot.Count >= 2)
                {
                    DropWeapon(c_weapon);
                    PickUp(nearestWp);
                }
                wpSlot.Add(transform);
            }
        }
        else
        {
            wpInfoBar.anchoredPosition = new Vector2(-25, -400);
            nearestWp.GetComponent<Weapon>().Name.SetActive(false);
            nearestWp.GetComponent<Weapon>().Name.GetComponent<Canvas>().sortingOrder = 5;
        }
        if (GameObject.FindGameObjectWithTag("CurrentWeapon") != null)
        {
            c_weapon = GameObject.FindGameObjectWithTag("CurrentWeapon").transform;
            c_weapon.GetComponent<SpriteRenderer>().sortingOrder = 1;
            c_weapon.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("SecondaryWeapon") != null)
        {
            s_weapon = GameObject.FindGameObjectWithTag("SecondaryWeapon").transform;
            s_weapon.GetComponent<SpriteRenderer>().sortingOrder = -1;
            s_weapon.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private Transform GetClosestWp(GameObject[] wps)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in wps)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        return bestTarget;
    }
    private void PickUp(Transform nearestWp)
    {
        if (nearestWp.CompareTag("DroppedWeapon"))
        {
            isPressed = false;
            nearestWp.GetComponent<SpriteRenderer>().enabled = true;
            nearestWp.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            nearestWp.tag = "CurrentWeapon";
            nearestWp.SetParent(wpHolder1);
            if (nearestWp.GetComponent<Weapon>().d_weapondata.cbStyle != CombatStyle.Ranged)
            {
                wpHolder1.localPosition = new Vector2(0, -3.5f);
            }
            else
            {
                wpHolder1.localPosition = new Vector2(0,-3f);
            }
        }
    }
    private void SwapWeaponWithHand()
    {
        c_weapon.SetParent(wpHolder2);
        c_weapon.localPosition = Vector2.zero + new Vector2(0, 0.15f);
        c_weapon.tag = "SecondaryWeapon";
    }

    private void DropWeapon(Transform weapon)
    {
        weapon.tag = "DroppedWeapon";
        weapon.position = GetComponent<PlayerMovement>().DropPoint.position;
        weapon.SetParent(null);
        weapon.localEulerAngles = Vector3.zero;
    }
}
