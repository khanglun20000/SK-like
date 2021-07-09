using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour
{
	public int num;
	public Texture2D tex;
	[HideInInspector]
	public Vector3 spawnPos;
	[HideInInspector]
	public Vector2 gridPos;
	public int type, waveCount;
	[HideInInspector]
	public bool doorTop, doorBot, doorLeft, doorRight,isSpecial;

	public bool playerWentIn=false;
	private bool canSumMons=false;
	public Transform appearingFrame;
	public Transform[] monsSpawners;
	[SerializeField]
	Transform tileContainer;
	[SerializeField]
	Transform doorTriggerV, doorTriggerH;
	[SerializeField]
	Transform detectV, detectH;
	[SerializeField]
	Transform[] mons;
    readonly List<GameObject> DoorTriggers = new List<GameObject>();
	public int monsCount;


	[SerializeField]
	GameObject doorWallV, doorWallH;
	[SerializeField]
	GameObject[] doorU, doorD, doorL, doorR;
	[SerializeField]
	ColorToGameObjectRoom[] mappings;
    readonly float tileSize = 1;
	Vector2 roomSizeInTiles = new Vector2(17, 49);


    public void Setup(Texture2D _tex, Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight, bool _isSpecial, int _waveCount)
	{
		num = FindObjectOfType<SheetAssigner>().roomCountLeft;
		tex = _tex;
		gridPos = _gridPos;
		type = _type;
		doorTop = _doorTop;
		doorBot = _doorBot;
		doorLeft = _doorLeft;
		doorRight = _doorRight;
		isSpecial = _isSpecial;
		waveCount = _waveCount;
		MakeDoors();
		GenerateRoomTiles();
	}
	void MakeDoors()
	{

		switch (type)
		{
			case 1:
				//top door, get position then spawn
				spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
				PlaceDoorU(spawnPos, doorTop, doorU[type - 1]);
				//bottom door
				spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
				PlaceDoorD(spawnPos, doorBot, doorD[type - 1]);
				//right door
				spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
				PlaceDoorR(spawnPos, doorRight, doorR[type - 1]);
				//left door
				spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
				PlaceDoorL(spawnPos, doorLeft, doorL[type - 1]);
				break;
			case 2:
				//top door, get position then spawn
				spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
				PlaceDoorU(spawnPos, doorTop, doorU[type - 1]);
				//bottom door
				spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
				PlaceDoorD(spawnPos, doorBot, doorD[type - 1]);
				//right door
				spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x - 9) * tileSize;
				PlaceDoorR(spawnPos, doorRight, doorR[type - 1]);
				//left door
				spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x - 9) * tileSize;
				PlaceDoorL(spawnPos, doorLeft, doorL[type - 1]);
				break;
			case 3:
				//top door, get position then spawn
				spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 + 2.75f) * tileSize;
				PlaceDoorU(spawnPos, doorTop, doorU[type - 1]);
				//bottom door
				spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 + 2.75f) * tileSize;
				PlaceDoorD(spawnPos, doorBot, doorD[type - 1]);
				//right door
				spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
				PlaceDoorR(spawnPos, doorRight, doorR[type - 1]);
				//left door
				spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
				PlaceDoorL(spawnPos, doorLeft, doorL[type - 1]);
				break;
			case 4:
				//top door, get position then spawn
				spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 + 2.75f) * tileSize;
				PlaceDoorU(spawnPos, doorTop, doorU[type - 1]);
				//bottom door
				spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 + 2.75f) * tileSize;
				PlaceDoorD(spawnPos, doorBot, doorD[type - 1]);
				//right door
				spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x - 9) * tileSize;
				PlaceDoorR(spawnPos, doorRight, doorR[type - 1]);
				//left door
				spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x - 9) * tileSize;
				PlaceDoorL(spawnPos, doorLeft, doorL[type - 1]);
				break;
		}
	}
	void PlaceDoorR(Vector3 spawnPos, bool door, GameObject doorSpawn)
	{
		// check whether its a door or wall, then spawn
		if (door)
		{
			switch (type)
			{
				case 1:
					Instantiate(doorSpawn, spawnPos + new Vector3(9, 0, 0), Quaternion.identity,transform);
					break;
				case 2:
					Instantiate(doorSpawn, spawnPos + new Vector3(17, 0, 0), Quaternion.identity,transform);
					break;
				case 3:
					Instantiate(doorSpawn, spawnPos + new Vector3(9, 0, 0), Quaternion.identity,transform);
					break;
				case 4:
					Instantiate(doorSpawn, spawnPos + new Vector3(17, 0, 0), Quaternion.identity,transform);
					break;
			}
			GameObject doorTrigger=Instantiate(doorTriggerV, spawnPos, Quaternion.identity,transform).gameObject;
			DoorTriggers.Add(doorTrigger);
			if(!isSpecial)
				Instantiate(detectV, spawnPos + new Vector3(-2, 0, 0), Quaternion.identity,transform);
		}
		else
		{
			Instantiate(doorWallV, spawnPos, Quaternion.identity, transform);
		}
	}
	void PlaceDoorL(Vector3 spawnPos, bool door, GameObject doorSpawn)
	{
		// check whether its a door or wall, then spawn
		if (door)
		{
			Instantiate(doorSpawn, spawnPos, Quaternion.identity, transform);
			GameObject doorTrigger = Instantiate(doorTriggerV, spawnPos, Quaternion.identity, transform).gameObject;
			DoorTriggers.Add(doorTrigger);
			if (!isSpecial)
				Instantiate(detectV, spawnPos + new Vector3(2, 0, 0), Quaternion.identity, transform);
		}
		else
		{
			Instantiate(doorWallV, spawnPos, Quaternion.identity, transform);
		}
	}
	void PlaceDoorU(Vector3 spawnPos, bool door, GameObject doorSpawn)
	{
		Vector3 offset = Vector3.zero;
		Vector3 offset2 = Vector3.zero;
		switch (type)
		{
			case 1:
				offset = new Vector3(0, 0, 0);
				break;
			case 2:
				offset = new Vector3(0, 0, 0);
				break;
			case 3:
				offset = new Vector3(0, -3, 0);
				break;
			case 4:
				offset = new Vector3(0, -3, 0);
				break;
		}
		switch (type)
		{
			case 1:
				offset2 = new Vector3(0, 0, 0);
				break;
			case 2:
				offset2 = new Vector3(0, 0, 0);
				break;
			case 3:
				offset2 = new Vector3(0, -7, 0);
				break;
			case 4:
				offset2 = new Vector3(0, -7, 0);
				break;
		}
		// check whether its a door or wall, then spawn
		if (door)
		{
			Instantiate(doorSpawn, spawnPos + offset, Quaternion.identity, transform);
			GameObject doorTrigger = Instantiate(doorTriggerH, spawnPos + offset2, Quaternion.identity, transform).gameObject;
			DoorTriggers.Add(doorTrigger);
			if (!isSpecial)
				Instantiate(detectH, spawnPos + offset2 + new Vector3(0, -2, 0), Quaternion.identity, transform);
		}
		else
		{
			Instantiate(doorWallH, spawnPos + offset2, Quaternion.identity, transform);
		}
	}
	void PlaceDoorD(Vector3 spawnPos, bool door, GameObject doorSpawn)
	{
		Vector3 offset = Vector3.zero;
		Vector3 offset2 = Vector3.zero;
		switch (type)
		{
			case 1:
				offset = new Vector3(0, -7, 0);
				break;
			case 2:
				offset = new Vector3(0, -7, 0);
				break;
			case 3:
				break;
			case 4:
				break;
		}
		switch (type)
		{
			case 1:
				break;
			case 2:
				break;
			case 3:
				offset2 = new Vector3(0, 7, 0);
				break;
			case 4:
				offset2 = new Vector3(0, 7, 0);
				break;
		}
		// check whether its a door or wall, then spawn
		if (door)
		{

			Instantiate(doorSpawn, spawnPos + offset, Quaternion.identity, transform);
			GameObject doorTrigger = Instantiate(doorTriggerH, spawnPos + offset2, Quaternion.identity, transform).gameObject;
			DoorTriggers.Add(doorTrigger);
			if (!isSpecial)
				Instantiate(detectH, spawnPos + offset2 + new Vector3(0, 2, 0), Quaternion.identity, transform);
		}
		else
		{

			Instantiate(doorWallH, spawnPos + offset2, Quaternion.identity, transform);
		}
	}
	void GenerateRoomTiles()
	{
		//loop through every pixel of the texture
		for (int x = 0; x < tex.width; x++)
		{
			for (int y = 0; y < tex.height; y++)
			{
				GenerateTile(x, y);
			}
		}
	}
	void GenerateTile(int x, int y)
	{
		Color pixelColor = tex.GetPixel(x, y);
		//skip clear spaces in texture
		if (pixelColor.a == 0)
		{
			return;
		}
		//find the color to math the pixel
		foreach (ColorToGameObjectRoom mapping in mappings)
		{
			if (Mathf.Round(mapping.color.r * 255) == Mathf.Round(pixelColor.r * 255) && Mathf.Round(mapping.color.b * 255) == Mathf.Round(pixelColor.b * 255) && Mathf.Round(mapping.color.g * 255) == Mathf.Round(pixelColor.g * 255))
			//if(mapping.color.Equals(pixelColor))
			{
				Vector2 spawnPos = PositionFromTileGrid(x, y);
				if (Mathf.Round(pixelColor.r * 255) == 255 && Mathf.Round(mapping.color.b * 255) == 0 && Mathf.Round(mapping.color.g * 255) == 0)
				{
					Instantiate(mapping.prefab[Random.Range(0, mapping.prefab.Length)], spawnPos, Quaternion.identity, tileContainer);
				}
				else
				{
					Instantiate(mapping.prefab[Random.Range(0, mapping.prefab.Length)], spawnPos, Quaternion.identity,transform);
					if (mapping.prefab2.Length != 0)
					{
						Instantiate(mapping.prefab2[Random.Range(0, mapping.prefab2.Length)], spawnPos, Quaternion.identity, transform);
					}
				}
			}
		}
	}
	Vector2 PositionFromTileGrid(int x, int y)
	{
		Vector2 ret;
		//find difference between the corner of the texture and the center of this object
		Vector2 offset = new Vector3((-roomSizeInTiles.x + 1) * tileSize, (roomSizeInTiles.y / 4) * tileSize - (tileSize / 4));
		//find scaled up position at the offset
		ret = new Vector2(tileSize * (float)x, -tileSize * (float)y) + offset + (Vector2)transform.position;
		return ret;
	}
	public void TriggerRoom()
	{
		Ability1.isInRoom = true;
		playerWentIn = true;
		foreach(GameObject doorTrigger in DoorTriggers)
        {
			doorTrigger.gameObject.SetActive(true);
        }
		Object[] detects = GetComponentsInChildren<DectectPlayerEnter>();
		for (int i = 0; i < detects.Length; i++)
		{
			Destroy(detects[i],0.5f);
		}
	}
	public void LocateMonsSpawners(int _maxMonsNum)
	{
		monsSpawners = new Transform[_maxMonsNum];
		HashSet<int> alreadyChosen = new HashSet<int>();
		for (int monsNum = 0; monsNum < _maxMonsNum; monsNum++)
		{
			int RandTileIndex = Random.Range(0, tileContainer.childCount);
			while (alreadyChosen.Contains(RandTileIndex))
			{
				RandTileIndex = Random.Range(0, tileContainer.childCount);
			}
			alreadyChosen.Add(RandTileIndex);
			monsSpawners[monsNum] = tileContainer.GetChild(RandTileIndex);
		}
	}
	void SummonMons()
    {
		int maxMonsCount= SetMaxMonsCount();
		LocateMonsSpawners(maxMonsCount);
		for (int i = 0; i < maxMonsCount; i++)
		{
			StartCoroutine(MonsAppear(i));
		}
		canSumMons = false;
		waveCount--;
	}
	IEnumerator MonsAppear(int i)
    {
		Instantiate(appearingFrame, monsSpawners[i].transform.position, Quaternion.identity, transform);
		monsCount++;
		yield return new WaitForSeconds(1.5f);
		Instantiate(mons[Random.Range(0,mons.Length)], monsSpawners[i].transform.position, Quaternion.identity, transform);
	}
	int SetMaxMonsCount()
    {
		int _maxMonsNum=0;
		switch (type)
		{
			case 1:
				_maxMonsNum = Random.Range(12, 16);
				break;
			case 2:
				_maxMonsNum = Random.Range(8, 12);
				break;
			case 3:
				_maxMonsNum = Random.Range(8, 12);
				break;
			case 4:
				_maxMonsNum = Random.Range(5, 8);
				break;
		}
		return _maxMonsNum;
	}
	private void Update()
	{
		if (!isSpecial)
		{
			if (canSumMons == true && playerWentIn==true)
			{
				StartCoroutine(nameof(SummonMons));
			}
			if (monsCount <= 0 && waveCount == 0)
			{
				foreach (GameObject doorTrigger in DoorTriggers)
				{
					doorTrigger.gameObject.SetActive(false);
				}
			}
			else if (monsCount <= 0 && waveCount > 0)
			{
				canSumMons = true;
			}
		}
	}
}
