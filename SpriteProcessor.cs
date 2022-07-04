/*
 * Graffiti Softwerks 2022
 * SpriteProcessor.cs
 * Author: Nash Ali
 * Creation Date: 04-21-2022
 * 
 * Copyright (c) Graffiti Softwerks 2022
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteProcessor : AssetPostprocessor
{
    private void OnPostprocessTexture(Texture2D texture)
    {
        string lowerCaseAssetPath = assetPath.ToLower();
        bool isInSpriteDirectory = lowerCaseAssetPath.IndexOf("/sprites/") != -1;
        if (isInSpriteDirectory)
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
        }
    }
}

