﻿using Modding;
using System.Collections.Generic;
using UnityEngine;

public class GeoRemover : Mod, ITogglableMod {
    internal static GeoRemover Instance;
    private GameObject geoText = null;
    private GameObject geoSprite = null;

    public GeoRemover() : base("Geo Remover") {
       Instance = this;
    }

    public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
        Log("Initializing");

        Instance = this;
        ModHooks.HeroUpdateHook += PollForGeo;

        Log("Initialized");
    }

    public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

    private void PollForGeo() {
        if (geoText == null) {
            geoText = GameObject.Find("Geo Counter");
        }
        if (geoSprite == null) {
            geoSprite = GameObject.Find("Geo Sprite");
        }
        if (geoText != null && geoSprite != null) {
            geoText.transform.localScale = new Vector3(0, 0, 0);
            geoSprite.transform.localScale = new Vector3(0, 0, 0);
            ModHooks.HeroUpdateHook -= PollForGeo;
        }
    }

    public void Unload() {
        if (geoText != null && geoSprite != null) {
            geoText.transform.localScale = new Vector3(1, 1, 1);
            geoSprite.transform.localScale = new Vector3(1, 1, 1);
        }
        ModHooks.HeroUpdateHook -= PollForGeo;
    }
}