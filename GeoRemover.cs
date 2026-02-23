using Modding;
using UnityEngine;

public class GeoRemover : Mod, ITogglableMod {
    internal static GeoRemover instance;
    private GameObject geoCounter;
    private GameObject geoSprite;

    public GeoRemover() : base("Geo Remover") {
        instance = this;
    }

    public override void Initialize() {
        Log("Initializing");

        instance = this;
        On.HeroController.Start += OnHeroStart;
        HideGeo();

        Log("Initialized");
    }

    public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

    private void OnHeroStart(On.HeroController.orig_Start orig, HeroController self) {
        orig(self);
        HideGeo();
    }

    private void HideGeo() {
        GeoCounter counter = GameCameras.instance.geoCounter;
        if (counter == null) return;

        geoCounter = counter.gameObject;
        geoSprite = counter.geoSprite;

        geoCounter.transform.localScale = Vector3.zero;
        geoSprite.transform.localScale = Vector3.zero;
    }

    public void Unload() {
        On.HeroController.Start -= OnHeroStart;

        if (geoCounter != null) {
            geoCounter.transform.localScale = Vector3.one;
        }
        if (geoSprite != null) {
            geoSprite.transform.localScale = Vector3.one;
        }

        geoCounter = null;
        geoSprite = null;
    }
}
