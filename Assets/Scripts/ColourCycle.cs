using UnityEngine;

public class ColourCycle : MonoBehaviour
{
    public float cycleDuration = 2.0f;
    public float saturation = 1.0f;
    public float brightness = 1.0f;
    public float alpha = 1.0f;

    private ParticleSystem particleSystem;
    private float startTime;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        startTime = Time.time;
    }

    private void Update()
    {
        float cycleTime = (Time.time - startTime) % cycleDuration;
        float t = cycleTime / cycleDuration;
        float hue = Mathf.Lerp(0, 1, t);

        Color lerpedColor = Color.HSVToRGB(hue, saturation, brightness);
        lerpedColor.a = alpha;

        ParticleSystem.MainModule mainModule = particleSystem.main;

        mainModule.startColor = lerpedColor;

        if (cycleTime >= cycleDuration)
        {
            startTime = Time.time;
        }
    }
}