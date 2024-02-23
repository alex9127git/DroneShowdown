using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Vignette : MonoBehaviour
{
    public static Vignette Instance;
    private Image _image;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        _image = GetComponent<Image>();
    }

    public void Flash()
    {
        StartCoroutine(FlashingAnimation());
    }

    IEnumerator FlashingAnimation()
    {
        for (float t = 0; t < 0.2f; t += Time.deltaTime)
        {
            _image.color = new Color(1, 1, 1, 1 - t * 5);
            yield return null;
        }
    }
}
