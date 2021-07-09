using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour {
	
	public Sprite[] 	spU, spD, spR, spL,
			spUD, spRL, spUR, spUL, spDR, spDL,
			spULD, spRUL, spDRU, spLDR, spUDRL;
	public bool up, down, left, right;
	public int type; // 0: normal, 1: enter
	public Color normalColor, enterColor;
	Color mainColor;
	SpriteRenderer rend;
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		mainColor = normalColor;
		PickSprite();
		PickColor();
	}
	void PickSprite(){ //picks correct sprite based on the four door bools
		if (up){
			if (down){
				if (right){
					if (left){
						rend.sprite = spUDRL[Random.Range(0,spUDRL.Length)];
					}else{
						rend.sprite = spDRU[Random.Range(0, spDRU.Length)];
					} 
				}else if (left){
					rend.sprite = spULD[Random.Range(0, spULD.Length)];
				}else{
					rend.sprite = spUD[Random.Range(0, spUD.Length)];
				}
			}else{
				if (right){
					if (left){
						rend.sprite = spRUL[Random.Range(0, spRUL.Length)];
					}else{
						rend.sprite = spUR[Random.Range(0, spUR.Length)];
					}
				}else if (left){
					rend.sprite = spUL[Random.Range(0, spUL.Length)];
				}else{
					rend.sprite = spU[Random.Range(0, spU.Length)];
				}
			}
			return;
		}
		if (down){
			if (right){
				if(left){
					rend.sprite = spLDR[Random.Range(0, spLDR.Length)];
				}else{
					rend.sprite = spDR[Random.Range(0, spDR.Length)];
				}
			}else if (left){
				rend.sprite = spDL[Random.Range(0, spDL.Length)];
			}else{
				rend.sprite = spD[Random.Range(0, spD.Length)];
			}
			return;
		}
		if (right){
			if (left){
				rend.sprite = spRL[Random.Range(0, spRL.Length)];
			}else{
				rend.sprite = spR[Random.Range(0, spR.Length)];
			}
		}else{
			rend.sprite = spL[Random.Range(0, spL.Length)];
		}
	}

	void PickColor(){ //changes color based on what type the room is
		if (type == 0){
			mainColor = normalColor;
		}else if (type == 1){
			mainColor = enterColor;
		}
		rend.color = mainColor;
	}
}