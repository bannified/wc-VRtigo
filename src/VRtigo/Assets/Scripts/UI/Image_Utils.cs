using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public static class Image_Utils
{
    public static IEnumerator FaceToCameraCoroutine(Image img, Camera camera)
    {
        while (true)
        {
            Vector3 camForward = camera.transform.forward;
            camForward.y = 0.0f;

            img.transform.rotation = Quaternion.LookRotation(camForward);
            yield return null;
        }
    }

    public static IEnumerator FadeInImageCoroutine(Image img, float fadeDur)
    {
        float durationSoFar = 0.0f;
        float progress, alpha;

        while (durationSoFar < fadeDur)
        {
            progress = durationSoFar / fadeDur;
            alpha = progress + (Time.deltaTime / fadeDur);

            Color imageColor = img.color;
            imageColor.a = alpha;
            img.color = imageColor;

            durationSoFar += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator FadeOutImageCoroutine(Image img, float fadeDur)
    {
        float durationSoFar = 0.0f;
        float oriAlpha = img.color.a;
        float alpha = oriAlpha;
        float progress;

        while (alpha > 0.0f || durationSoFar < fadeDur)
        {
            progress = durationSoFar / fadeDur;
            alpha = oriAlpha - progress - (Time.deltaTime / fadeDur);

            Color imageColor = img.color;
            imageColor.a = alpha;
            img.color = imageColor;

            durationSoFar += Time.deltaTime;
            yield return null;
        }
    }
}
