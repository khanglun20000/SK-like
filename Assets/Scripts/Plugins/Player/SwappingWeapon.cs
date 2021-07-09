using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwappingWeapon : MonoBehaviour
{
    public Transform wpHolder1, wpHolder2;
    private Transform slot1, slot2;

    //public GameObject Name;

    //private TextMeshProUGUI TextName;

    //private float timeDisplay;

    public bool canSwap = true;

    /*
    private void Awake()
    {
        TextName = Name.GetComponentInChildren<TextMeshProUGUI>();
    }
    */
    void Update()
    {
        if (wpHolder1.childCount==1)
        {
            slot1 = wpHolder1.GetChild(0);
            slot1.tag = "CurrentWeapon";
            slot1.localPosition = Vector2.zero + new Vector2(0, 0.15f);
            if (slot1.GetComponent<Weapon>().d_weapondata.cbStyle != CombatStyle.Ranged)
            {
                wpHolder1.localPosition = new Vector2(0, -3.5f);
            }
            else
            {
                wpHolder1.localPosition = new Vector2(0, -3f);
            }
            slot1.localEulerAngles = new Vector3(0, 0, slot1.GetComponent<Weapon>().d_weapondata.holdRotaion);
            /*
            TextName.text = slot1.GetComponent<Weapon>().d_weapondata.name;
            switch (slot1.GetComponent<Weapon>().d_weapondata.rarity)
            {
                case Rarity.Common:
                    TextName.color = Color.white;
                    break;
                case Rarity.Uncommon:
                    TextName.color = new Color(0, 1, 0.3722396f, 1);
                    break;
                case Rarity.Rare:
                    TextName.color = new Color(0, 0.5179996f, 1, 1);
                    break;
                case Rarity.SuperRare:
                    TextName.color = new Color(0.8223186f, 0, 1, 1);
                    break;
                case Rarity.Epic:
                    TextName.color = new Color(1, 0.5765381f, 0, 1);
                    break;
                case Rarity.Legendary:
                    TextName.color = Color.red;
                    break;
            }
            */
        }
        if (wpHolder2.childCount==1)
        {
            slot2 = wpHolder2.GetChild(0);
            slot2.tag = "SecondaryWeapon";
            slot2.localPosition = Vector2.zero + new Vector2(0, 0.15f);
            if (slot2.GetComponent<Weapon>().d_weapondata.cbStyle != CombatStyle.Ranged)
            {
                wpHolder2.localPosition = new Vector2(-3.2f, -1.8f);
            }
            else
            {
                wpHolder2.localPosition = new Vector2(0, -3f);
            }
            if (slot2.GetComponent<Weapon>().d_weapondata.type != Type.Bow)
            {
                slot2.localEulerAngles = new Vector3(0, 0, -135);
            }
            else
            {
                slot2.localEulerAngles = Vector3.zero;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && slot1 != null && slot2 != null && canSwap)
        {
            slot1.SetParent(wpHolder2);
            slot2.SetParent(wpHolder1);
            //timeDisplay = 3f;
        }
       // DisplayName();
    }
    /*
    void DisplayName()
    {
        if (timeDisplay <= 0)
        {
            Name.SetActive(false);
        }
        else
        {
            Name.SetActive(true);
            timeDisplay -= Time.deltaTime;
        }
    }
    */
}
