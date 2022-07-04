/*
 * Graffiti Softwerks 2022
 * Playground.cs
 * Author: Nash Ali
 * Creation Date: 04-21-2022
 * 
 * Copyright (c) Graffiti Softwerks 2022
 */

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;

public class Playground : EditorWindow
{
    [MenuItem("MyTools / Playground")]
    public static void ShowWindow()
    {
        var window = EditorWindow.GetWindow(typeof(Playground));
        //add a title
        window.titleContent = new GUIContent("Playground");
        //sets a minimun size to the window
        window.minSize = new Vector2(280, 50);
    }
    private void CreateObject(PointerUpEvent _, string primitiveTypeName)
    {
        var pt = (PrimitiveType)Enum.Parse
                     (typeof(PrimitiveType), primitiveTypeName, true);
        var go = ObjectFactory.CreatePrimitive(pt);
        go.transform.position = Vector3.zero;
    }
    private void SetupButton(VisualElement button)
    {
        // Reference to the VisualElement inside the button that serves
        // as the button's icon.
        var buttonIcon = button.Q(className: "quicktool-button-icon");

        // Icon's path in our project.
        var iconPath = "Icons/" + button.parent.name + "_icon";

        // Loads the actual asset from the above path.
        var iconAsset = Resources.Load<Texture2D>(iconPath);

        // Applies the above asset as a background image for the icon.
        buttonIcon.style.backgroundImage = iconAsset;

        // Instantiates our primitive object on a left click.
        button.RegisterCallback<PointerUpEvent, string>(CreateObject, button.parent.name);

        // Sets a basic tooltip to the button itself.
        button.tooltip = button.parent.name;
    }
    private void CreateGUI()
    {
        // Reference to the root of the window.
        var root = rootVisualElement;

        // Associates a stylesheet to our root. Thanks to inheritance, all root?s
        // children will have access to it.
        root.styleSheets.Add(Resources.Load<StyleSheet>("QuickTool_Style"));

        // Loads and clones our VisualTree (eg. our UXML structure) inside the root.
        var quickToolVisualTree = Resources.Load<VisualTreeAsset>("QuickTool_Main");
        quickToolVisualTree.CloneTree(root);

        // Queries all the buttons (via type) in our root and passes them
        // in the SetupButton method.
        var toolButtons = root.Query(className: "quicktool-button");
        toolButtons.ForEach(SetupButton);
    }
    private void OnGUI()
    {
       
    }

}
