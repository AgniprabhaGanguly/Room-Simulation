using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using RenderSettings = UnityEngine.RenderSettings;

public class DayChanger : MonoBehaviour
{
    public static DayChanger instance;
    
    [Header("Lights & Emissives")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private GameObject outdoorLights;
    private Light[] lights;
    
    [SerializeField] private Material bulbMaterialGlow;
    [SerializeField] private Material glassMaterialGlow;
    [ColorUsage(false, true)] 
    [SerializeField] private Color bulbGlowColor;
    [ColorUsage(false, true)]
    [SerializeField] private Color glassGlowColor;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    [Header("Night Settings")] 
    public Texture2D[] nightLightMapDir;
    public Texture2D[] nightLightMapColor;
    private LightmapData[] nightLightmapData;
    
    [SerializeField] private Vector3 moonRotation;
    [SerializeField] private float moonColorTemperature;
    [SerializeField] private float moonIntensity;
    
    [SerializeField] Material nightSkybox;
    [ColorUsage(false, true)] 
    [SerializeField] private Color NightSkyColor;

    [ColorUsage(false, true)]
    [SerializeField] private Color NightEquatorColor;

    [ColorUsage(false,true)]
    [SerializeField] private Color NightGroundColor;

    [SerializeField] private GameObject nightPostprocessing;
    
    [Header("Noon Settings")] 
    public Texture2D[] noonLightMapDir;
    public Texture2D[] noonLightMapColor;
    private LightmapData[] noonLightmapData;
    
    [SerializeField] private Vector3 sunRotationNoon;
    [SerializeField] private float sunColorTemperatureNoon;
    [SerializeField] private float sunIntensityNoon;
    
    [SerializeField] Material noonSkybox;
    [ColorUsage(false, true)] 
    [SerializeField] private Color NoonSkyColor;

    [ColorUsage(false, true)]
    [SerializeField] private Color NoonEquatorColor;

    [ColorUsage(false,true)]
    [SerializeField] private Color NoonGroundColor;

    [SerializeField] private GameObject noonPostprocessing;
    
    
    private void Start()
    {
        lights = outdoorLights.GetComponentsInChildren<Light>(true);
        
        // NightLightmap
        List<LightmapData> nLightmap = new List<LightmapData>();
        
        for(int i = 0; i < nightLightMapDir.Length; i++)
        {
            LightmapData data = new LightmapData();
            data.lightmapDir = nightLightMapDir[i];
            data.lightmapColor = nightLightMapColor[i];
            nLightmap.Add(data);
        }
        nightLightmapData = nLightmap.ToArray();
        
        // NoonLightmap
        List<LightmapData> noonLightmap = new List<LightmapData>();
        
        for(int i = 0; i < noonLightMapDir.Length; i++)
        {
            LightmapData data = new LightmapData();
            data.lightmapDir = noonLightMapDir[i];
            data.lightmapColor = noonLightMapColor[i];
            noonLightmap.Add(data);
        }
        noonLightmapData = noonLightmap.ToArray();
    }

    public void Night()
    {
        directionalLight.transform.rotation = Quaternion.Euler(moonRotation);
        directionalLight.colorTemperature = moonColorTemperature;
        directionalLight.intensity = moonIntensity;
        
        
        RenderSettings.skybox = nightSkybox;
        RenderSettings.ambientMode = AmbientMode.Trilight;
        RenderSettings.ambientSkyColor = NightSkyColor;
        RenderSettings.ambientEquatorColor = NightEquatorColor;
        RenderSettings.ambientGroundColor = NightGroundColor;
        
        noonPostprocessing.SetActive(false);
        nightPostprocessing.SetActive(true);
        
        LightmapSettings.lightmaps = nightLightmapData;
        Debug.Log("Current Lightmaps: " + LightmapSettings.lightmaps.Length);
        
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
        bulbMaterialGlow.SetColor("_EmissionColor",bulbGlowColor);
        glassMaterialGlow.SetColor("_EmissionColor",glassGlowColor);
    }

    public void Sunrise()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
        bulbMaterialGlow.SetColor("_EmissionColor",Color.black);
        glassMaterialGlow.SetColor("_EmissionColor",Color.black);
    }
    
    public void Noon()
    {
        directionalLight.transform.rotation = Quaternion.Euler(sunRotationNoon);
        directionalLight.colorTemperature = sunColorTemperatureNoon;
        directionalLight.intensity = sunIntensityNoon;
        
        
        RenderSettings.skybox = noonSkybox;
        RenderSettings.ambientMode = AmbientMode.Trilight;
        RenderSettings.ambientSkyColor = NoonSkyColor;
        RenderSettings.ambientEquatorColor = NoonEquatorColor;
        RenderSettings.ambientGroundColor = NoonGroundColor;
        
        noonPostprocessing.SetActive(true);
        nightPostprocessing.SetActive(false);
        
        LightmapSettings.lightmaps = noonLightmapData;
        Debug.Log("Current Lightmaps: " + LightmapSettings.lightmaps.Length);
        
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
        bulbMaterialGlow.SetColor("_EmissionColor",Color.black);
        glassMaterialGlow.SetColor("_EmissionColor",Color.black);
    }
}
