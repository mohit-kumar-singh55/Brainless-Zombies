using System.Collections;
using UnityEngine;

public class LightFlickkering : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds_3 = new WaitForSeconds(.3f);
    [SerializeField] GameObject lightToFlicker;

    void Start()
    {
        StartCoroutine(FlikkerLight());
    }

    IEnumerator FlikkerLight()
    {
        while (true)
        {
            lightToFlicker.SetActive(Random.Range(0f, 1f) >= .5f);
            yield return _waitForSeconds_3;
        }
    }
}
