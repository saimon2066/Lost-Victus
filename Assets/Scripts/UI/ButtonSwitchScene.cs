using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ButtonSwitchScene : MonoBehaviour
    {
        public void SwitchScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
