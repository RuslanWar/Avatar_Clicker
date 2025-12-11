using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinBagPulse : MonoBehaviour
{
    public float scaleAmount = 1.1f;
    public float speed = 0.15f;
    public float colorFlashAmount = 1.4f;

    private Image img;
    private RawImage rawImg;
    private SpriteRenderer sr;

    private Vector3 originalScale;
    private Color baseColor;

    private void Awake()
    {
        img = GetComponent<Image>();
        rawImg = GetComponent<RawImage>();
        sr = GetComponent<SpriteRenderer>();

        originalScale = transform.localScale;

        if (img != null) baseColor = img.color;
        if (rawImg != null) baseColor = rawImg.color;
        if (sr != null) baseColor = sr.color;
    }

    public void Pulse()
    {
        StopAllCoroutines();
        StartCoroutine(PulseRoutine());
    }

    private IEnumerator PulseRoutine()
    {
        
        transform.localScale = originalScale;

        Vector3 bigger = originalScale * scaleAmount;
        Color brightColor = baseColor * colorFlashAmount;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / speed;
            float p = Mathf.Clamp01(t);

            transform.localScale = Vector3.Lerp(originalScale, bigger, p);
            ApplyColor(Color.Lerp(baseColor, brightColor, p));

            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / speed;
            float p = Mathf.Clamp01(t);

            transform.localScale = Vector3.Lerp(bigger, originalScale, p);
            ApplyColor(Color.Lerp(brightColor, baseColor, p));

            yield return null;
        }
    }

    private void ApplyColor(Color c)
    {
        if (img != null) img.color = c;
        if (rawImg != null) rawImg.color = c;
        if (sr != null) sr.color = c;
    }
}
