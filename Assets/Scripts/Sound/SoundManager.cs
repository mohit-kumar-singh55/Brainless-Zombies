using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    /// <summary>
    /// BGM音源
    /// </summary>
    [SerializeField]
    AudioSource bgmAudioSource;

    /// <summary>
    /// SE音源
    /// </summary>
    [SerializeField]
    AudioSource seAudioSource;

    /// <summary>
    /// BGM音量の取得設定
    /// </summary>
    public float BgmVolume
    {
        //音量の取得
        get
        {
            return bgmAudioSource.volume;
        }

        //範囲の設定
        set
        {
            bgmAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    /// <summary>
    /// SE音量の取得設定
    /// </summary>
    public float SeVolume
    {
        //音量の取得
        get
        {
            return seAudioSource.volume;
        }

        //範囲の設定
        set
        {
            seAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    /// <summary>
    /// シングルトン
    /// </summary>
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="bgmClip">BGMソース</param>
    /// <param name="volume">音量（0.0 ～ 1.0）</param>
    public void PlayBgm(AudioClip bgmClip, float volume = 1.0f)
    {
        // 現在の音楽が再生中なら停止する
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }

        // 音源がnullでない場合に再生
        if (bgmClip != null)
        {
            // 新しい音源と音量を設定
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.volume = volume;

            // 再生
            bgmAudioSource.Play();
        }
    }

    public void StopBgm()
    {
        // 現在の音楽が再生中なら停止する
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }

    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="seClip">SEソース</param>
    public void PlaySe(AudioClip seClip, float volume = 1.0f)
    {
        if (seClip == null)
        {
            return;
        }

        //再生
        seAudioSource.PlayOneShot(seClip, volume);
    }

}
