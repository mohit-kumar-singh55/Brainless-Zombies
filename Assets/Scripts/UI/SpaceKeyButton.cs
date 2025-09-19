using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceKeyButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;

            if (current == null)
            {
                Debug.Log("⚠️ 選択中のUIがありません");
                return;
            }

            Button btn = current.GetComponent<Button>();
            if (btn != null)
            {
                Debug.Log("✅ " + current.name + " をスペースキーでクリックしました");
                btn.onClick.Invoke();
            }
            else
            {
                Debug.Log("⚠️ 選択中のオブジェクトはボタンではありません: " + current.name);
            }
        }
    }
}
