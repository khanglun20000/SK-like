using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    private TextMeshProUGUI TextName;

    public GameObject Name;
    private static GameObject[] d_weapons;
    public WeaponData d_weapondata;
    private readonly List<WeaponData> d_wpdata= new List<WeaponData>();

    private void Awake()
    {
        TextName = Name.GetComponentInChildren<TextMeshProUGUI>();
        //add SO from WeaponData folder
        Object[] wpdata = Resources.LoadAll("DataBase/WeaponData");

        for (int k = 0; k < wpdata.Length; k++)
        {
            d_wpdata.Add(wpdata[k] as WeaponData);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //choose random SO from WeaponData folder
        d_weapondata = d_wpdata[Random.Range(0, d_wpdata.Count)];
        TextName.text = d_weapondata.name;
        switch (d_weapondata.rarity)
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
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("DroppedWeapon") != null)
        {
            d_weapons = GameObject.FindGameObjectsWithTag("DroppedWeapon");
            foreach (GameObject d_weapon in d_weapons)
            {
                d_weapon.GetComponent<SpriteRenderer>().enabled = false ;
                d_weapon.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        // Get sprite from WeaponData
        if (d_weapondata != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = d_weapondata.artwork;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = d_weapondata.display;
            if (d_weapondata.shootAniCtrl != null)
            {
                gameObject.GetComponent<Animator>().runtimeAnimatorController = d_weapondata.shootAniCtrl;
            }
        }
    }
}
