using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // インスペクターからシーン名を設定できるようにする
    [SerializeField] private string sceneName;

    public void LoadTargetScene()
    {
        StartCoroutine(PlaySoundAndChangeScene());
    }

    IEnumerator PlaySoundAndChangeScene()
    {
        SeManager.Instance.ButtonTapSe();
        yield return new WaitForSeconds(SeManager.Instance.buttonTap.length); // 効果音が終わるのを待つ

        SceneManager.LoadScene(sceneName);

    }
}
