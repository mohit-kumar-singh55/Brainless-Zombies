using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class SeManager : MonoBehaviour
{
    /// <summary>
    /// サウンドマネージャー
    /// </summary>
    [SerializeField]
    SoundManager soundManager;

    [SerializeField]
    public AudioClip buttonTap;

    [SerializeField]
    AudioClip choiceChange;

    [SerializeField]
    AudioClip brainTap;

    public static SeManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void PlayChoiceChangeSe() => soundManager.PlaySe(choiceChange);

    public void BrainTapSe() => soundManager.PlaySe(brainTap);

    public void ButtonTapSe() => soundManager.PlaySe(buttonTap);
}