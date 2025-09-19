using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadTargetScene()
    {
        StartCoroutine(PlaySoundAndChangeScene());
    }

    IEnumerator PlaySoundAndChangeScene()
    {
        SeManager.Instance.ButtonTapSe();
        yield return new WaitForSeconds(SeManager.Instance.buttonTap.length);

        SceneManager.LoadScene(sceneName);
    }
}
