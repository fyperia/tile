using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Platform : Tile
{
	[SerializeField]
	private Sprite[] spriteList;
	[SerializeField]
	private Sprite preview;

	Vector3Int nPos;

	private enum AdjacentTile : int
	{
		NorthWest = 1,
		North = 1 << 1,
		NorthEast = 1 << 2,
		West = 1 << 3,
		East = 1 << 4,
		SouthWest = 1 << 5,
		South = 1 << 6,
		SouthEast = 1 << 7
	}

	public override void RefreshTile(Vector3Int position, ITilemap tilemap)
	{
		for (int y = -1; y <= 1; y++)
		{
			for (int x = -1; x <= 1; x++)
			{
				nPos = new Vector3Int(position.x + x, position.y + y, position.z);
				if (HasPlatform(tilemap, nPos))
					tilemap.RefreshTile(nPos);
			}
		}
	}

	// Calculates a bitmasking value for the tile at the given position
	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		int n = 0;
		int bitVal = 0;
		for (int y = -1; y <= 1; y++)
		{
			for (int x = 1; x >= -1; x--)
			{
				if (!(x == 0 && y == 0))
				{
					if (HasPlatform(tilemap, new Vector3Int(position.x + x, position.y + y, position.z)))
					{
						bitVal += (int) Mathf.Pow(2, n);
					}
					n++;
				}
			}
		}
		tileData.sprite = spriteList[SpriteDict.getIndex(bitVal)];
	}

	//Checks to see if there is a "Platform" tile in the given position
	private bool HasPlatform(ITilemap tilemap, Vector3Int position)
	{
		return tilemap.GetTile(position) == this;
	}

	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		if (!SpriteDict.exists)
			SpriteDict.CreateSpriteDict();
		return base.StartUp(position, tilemap, go);
		
	}

#if UNITY_EDITOR
	[MenuItem("Assets/Create/Tiles/Platform")]
	public static void CreatePlatform()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Platform", "New Platform", "asset", "Save Platform", "Assets");
		if (path == "")
			return;
		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<Platform>(), path);
	}

#endif
}
