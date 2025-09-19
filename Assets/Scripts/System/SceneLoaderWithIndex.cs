using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderWithIndex : MonoBehaviour
{
    public static void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
