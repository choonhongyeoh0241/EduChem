using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pause { 
    public static class PauseMovement { 
        private static List<IPauser> pausers = new List<IPauser>(); 

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)] 
        private static void Init() {
            SceneManager.activeSceneChanged += OnSceneChanged;
            var activeScene = SceneManager.GetActiveScene(); 
            OnSceneChanged(activeScene, activeScene);
        }

        private static void OnSceneChanged(Scene scene1, Scene scene2) { 
            GetIPausers();
        }

        private static void GetIPausers() {
            
            pausers.Clear();

            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects(); 
            for (int i = 0; i < rootGameObjects.Length; i++) {
                var childInterfaces = rootGameObjects[i].GetComponentsInChildren<IPauser>(true); // Use (true) so that disabled game objects are also searched for
                for (int j = 0; j < childInterfaces.Length; j++) {
                    pausers.Add(childInterfaces[j]);
                }
            }
        }

        public static bool IsActive() { 
            foreach (var pauser in pausers) { 
                // Debug.Log(pauser);
                if (pauser.active) {  
                    return true; 
                }
            }
            return false; 
        }
    }

    public interface IPauser {
        bool active { get; }
    }
}

