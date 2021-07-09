using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Shape", menuName = "Level Shape")]
public class LevelData : ScriptableObject
{
    public Texture2D[] LevelTexture;
    public int Type;
}
