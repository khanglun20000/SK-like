using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability1 : MonoBehaviour
{
    public Transform skillWpHolder;
    public static bool isInRoom;
    public static bool isUsingSkill1;//used in PlayerShooting
    public float delay;
    public float abiCD;
    public float startAbiCd;
    static public bool isPhasing2, isPhasing1;
    public Transform voidAura;

    public WeaponData weaponData;
    public Transform shootPoint;
    // Start is called before the first frame update
    void Start()
    {
        isPhasing2 = false;
        isPhasing1 = false;
        abiCD = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (abiCD <= 0)
        {
            Phase();
        }
        else
        {
            abiCD -= Time.deltaTime;
        }
        
        if (isInRoom)
        {
            VoidController.isActive = false;
            voidAura.GetChild(0).gameObject.SetActive(false);
            isUsingSkill1 = false;
            skillWpHolder.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            voidAura.GetComponent<SpriteRenderer>().enabled = false;
            isPhasing2 = false;
            abiCD = startAbiCd;
            isInRoom = false;
        }
    }
    public IEnumerator EndPhase1()
    {
        yield return new WaitForSeconds(delay);
        PickUpWeapon.canPickUp = true;
        isUsingSkill1 = false;
        skillWpHolder.GetComponentInChildren<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<SwappingWeapon>().enabled = true;
        isPhasing2 = true;
        yield return new WaitForSeconds(startAbiCd);
        if (isPhasing2)
        {
            abiCD = startAbiCd;
            isPhasing2 = false;
            voidAura.GetComponent<SpriteRenderer>().enabled = false;
            voidAura.GetChild(0).gameObject.SetActive(false);
        }
    }
    void Phase()
    {
        if (Input.GetMouseButtonDown(1) && !isPhasing2 && !PlayerShooting.isShooting)
        {
            isUsingSkill1 = true;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            GetComponent<SwappingWeapon>().enabled = false;
            PickUpWeapon.canPickUp = false;
            skillWpHolder.GetComponentInChildren<SpriteRenderer>().enabled = true;
            voidAura.GetComponent<SpriteRenderer>().enabled = true;
            voidAura.GetChild(0).gameObject.SetActive(true);
        }
        if (VoidController.isActive)
        {
            if(Input.GetMouseButtonDown(1) && isPhasing2)
            {
                voidAura.GetComponent<SpriteRenderer>().enabled = false;
                voidAura.GetChild(0).gameObject.SetActive(false);
                transform.position = FindObjectOfType<VoidController>().transform.position;
                VoidController.isActive = false;
                abiCD = startAbiCd;
                isPhasing2 = false;
                StartCoroutine(Immunity());
            }
        }
    }
    IEnumerator Immunity()
    {
        Color32 playerColor = GetComponent<SpriteRenderer>().color;
        PlayerBehaviour.canTakeDamage = false;
        GetComponent<SpriteRenderer>().color = new Color32(playerColor.r,playerColor.b,playerColor.g, 180);
        yield return new WaitForSeconds(2);
        PlayerBehaviour.canTakeDamage = true;
        GetComponent<SpriteRenderer>().color = playerColor;
    }
}
