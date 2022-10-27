using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxRandomizer : MonoBehaviour
{
    public List<Material> skybox;
    void Start()
    {
        int random = Random.Range(0, skybox.Count + 1);
        Debug.Log(random);
        RenderSettings.skybox = skybox[random];
        DynamicGI.UpdateEnvironment();
    }
}
