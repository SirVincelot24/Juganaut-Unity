using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScenePersistence
{
    public class SceneChanger : MonoBehaviour
    {
        public string sceneToLoad;

        public void ChangeSceneNow()
        {
            ChangeSceneNow(sceneToLoad);
        }

        public static void ChangeSceneNow(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
