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
        // �ڳ�����ʼʱ�ر����еƹ�
        foreach (var light in lights)
        {
            light.enabled = false;
        }
        // �� Start ������������ƹ�״̬
        //Debug.Log("Light status after Start: " + string.Join(", ", lights.Select(light => light.enabled.ToString()).ToArray()));
    }

    public void LightEnableFalsh()
    {
        foreach (var light in lights)
        {
            light.enabled = false;
        }
    }

    // ���������һ��������ʹ�ƹ���������
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
