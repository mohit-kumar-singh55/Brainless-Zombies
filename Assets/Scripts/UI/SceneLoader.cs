using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.Select();
    }

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
