using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pause { 
    public static class PauseMovement { 
        private static List<IPauser> pausers = new List<IPauser>(); 

        //https://docs.unity3d.com/ScriptReference/RuntimeInitializeOnLoadMethodAttribute.html
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)] 
        private static void Init() { // Since Init method subscribes to SceneManager.activeSceneChanged, it will receive callback when scene changes
            // https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager-activeSceneChanged.html
            SceneManager.activeSceneChanged += OnSceneChanged;
            var activeScene = SceneManager.GetActiveScene(); 
            OnSceneChanged(activeScene, activeScene);
        }

        private static void OnSceneChanged(Scene scene1, Scene scene2) { 
            GetIPausers();
        }

        private static void GetIPausers() {
            // http://answers.unity.com/answers/1516848/view.html
            pausers.Clear();

            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects(); // https://answers.unity.com/questions/190074/is-it-possible-to-get-all-root-transforms-of-the-s.html

            for (int i = 0; i < rootGameObjects.Length; i++) {
                var childInterfaces = rootGameObjects[i].GetComponentsInChildren<IPauser>(true); // Use (true) so that disabled game objects are also searched for
                for (int j = 0; j < childInterfaces.Length; j++) {
                    pausers.Add(childInterfaces[j]);
                }
            }
        }

        public static bool IsActive() { 
            foreach (var pauser in pausers) { 
                if (pauser.active) {  // Check if pauser has active 
                    return true; // If true, return true
                }
            }
            return false; // else return false
        }
    }

    public interface IPauser {
        bool active { get; }
    }
}

