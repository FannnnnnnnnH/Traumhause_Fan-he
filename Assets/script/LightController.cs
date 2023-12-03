using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LightController : MonoBehaviour
{
    public Light[] lights;
    public ParticleSystem particleGluehwuermchen;
    private int currentLightIndex = 0;

    void Start()
    {
        Debug.Log("Number of lights: " + lights.Length);
        particleGluehwuermchen.Pause();
        // 在场景开始时关闭所有灯光
        foreach (var light in lights)
        {
            light.enabled = false;
        }
        // 在 Start 方法结束后检查灯光状态
        //Debug.Log("Light status after Start: " + string.Join(", ", lights.Select(light => light.enabled.ToString()).ToArray()));
    }

    public void LightEnableFalsh()
    {
        foreach (var light in lights)
        {
            light.enabled = false;
        }
    }

    // 在这里添加一个方法，使灯光依次亮起
    public void TurnOnLightsSequentially(float delayBetweenLights)
    {
        if (lights.Length == 0)
        {
            Debug.LogWarning("No lights assigned to the LightController.");
            return;
        }

        StartCoroutine(TurnOnLightsCoroutine(delayBetweenLights));
    }

    private System.Collections.IEnumerator TurnOnLightsCoroutine(float delay)
    {
        foreach (Light light in lights)
        {
            yield return new WaitForSeconds(delay);
            light.enabled = true;
        }
    }
}
