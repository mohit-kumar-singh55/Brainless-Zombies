using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmManager : MonoBehaviour
{
    /// <summary>
    /// サウンドマネージャー
    /// </summary>
    [SerializeField]
    SoundManager soundManager;

    /// <summary>
    /// タイトル時のBGM
    /// </summary>
    [SerializeField]
    AudioClip TitleClip;

    /// <summary>
    /// メインゲームのBGM
    /// </summary>
    [SerializeField]
    AudioClip MainGameClip;

    [SerializeField]
    AudioClip ResultClip;


    void Start()
    {
        // ゲーム開始時の現在のシーンに応じてBGMを再生
        PlayBgmForScene(SceneManager.GetActiveScene().name);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBgmForScene(scene.name);
    }

    void PlayBgmForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "TitleScene":
                TitleBgm();
                break;
            case "Main Scene":
                MainGameBgm();
                break;
            case "ResultScene":
                ResultBgm();
                break;
        }

    }

    /// <summary>
    /// タイトルBGMを流す
    /// </summary>
    public void TitleBgm()
    {
        soundManager.PlayBgm(TitleClip, 0.1f);
    }

    /// <summary>
    /// メインゲームのBGMを流す
    /// </summary>
    public void MainGameBgm()
    {
        soundManager.PlayBgm(MainGameClip, 0.1f);
    }

    public void ResultBgm()
    {
        soundManager.PlayBgm(ResultClip, 0.1f);
    }
}