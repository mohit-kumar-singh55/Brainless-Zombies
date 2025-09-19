using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class QuitGameG : MonoBehaviour
{
    // ボタンが押されたときに呼び出されるメソッド
    public void Quit()
    {
        StartCoroutine(PlaySoundAndChangeScene());
    }

    IEnumerator PlaySoundAndChangeScene()
    {
        SeManager.Instance.ButtonTapSe();
        yield return new WaitForSeconds(SeManager.Instance.buttonTap.length); // 効果音が終わるのを待つ
                                                                                 // ゲームがエディタで実行されている場合は停止する
#if UNITY_EDITOR
        // エディタを停止
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // ビルドされたゲームを終了
            Application.Quit();
#endif

    }
}
