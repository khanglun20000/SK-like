using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetAssigner : MonoBehaviour {
	[SerializeField]
	GameObject RoomObj;
	public int roomCountLeft;
	public Vector2 roomDimensions;
	public Vector2 gutterSize;
	private readonly List<LevelData> levelTypes = new List<LevelData>();
	private void Awake()
	{
		Object[] levelShapes = Resources.LoadAll("Database/LevelData");
		for (int k = 0; k < levelShapes.Length; k++)
		{
			levelTypes.Add(levelShapes[k] as LevelData);
		}
	}

	public void Assign(Room[,] rooms){
		int specialRoomCount=Random.Range(8,11);
		int specialRoomLeft = specialRoomCount;
		foreach (Room room in rooms){
			//skip point where there is no room
			if (room == null)
			{
				continue;
			}
			if (specialRoomLeft > 0)
			{
				if (roomCountLeft % 4 == 0 && room.gridPos != Vector2.zero)
				{
					room.isSpecial = true;
					specialRoomLeft--;
				}
			}
			int index, index2;
			//set starting room type 5-1
            if (room.gridPos == Vector2.zero)
            {
				index = 4;
				index2 = 0;
				room.isSpecial = true;
            }
			//pick random type from LevelData
            else
            {
                if (!room.isSpecial)
                {
					index = Mathf.RoundToInt(Random.value * (levelTypes.Count - 1));
					//pick a random index for the array LevelType
					while (index == 4 || index==5)
					{
						index = Mathf.RoundToInt(Random.value * (levelTypes.Count - 1));
					}
					//pick a random index for the array LevelTexture
					index2 = Mathf.RoundToInt(Random.value * (levelTypes[index].LevelTexture.Length - 1));
				}
                else
                {
					index = 5;
					index2= Mathf.RoundToInt(Random.value * (levelTypes[index].LevelTexture.Length - 1));
				}
				
			}
			room.waveCount = Mathf.RoundToInt(Random.Range(1, 4));
			//find position to place room
			Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
			RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
			roomCountLeft--;
			myRoom.Setup(levelTypes[index].LevelTexture[index2], room.gridPos, levelTypes[index].Type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight, room.isSpecial,room.waveCount);
		}
	}
}
