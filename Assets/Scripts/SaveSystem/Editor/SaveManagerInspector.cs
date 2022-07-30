using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
public class SaveManagerInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        GUILayout.Space(5);
        
        var sm = (SaveManager)target;
        DrawButtons(sm);
    }

    private void DrawButtons(SaveManager sm) {
        GUILayout.Label("Debugging", EditorStyles.boldLabel);
        if (GUILayout.Button("Save")) {
            sm.Save();
        }
        else if (GUILayout.Button("Load")) {
            sm.Load();

            if (Application.isPlaying && !string.IsNullOrWhiteSpace(sm.location.scene)) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sm.location.scene);
                SceneAnchor.transitionPosition = sm.location.position;
            }
        }
        else if (GUILayout.Button("Open Save Folder")) {
            Application.OpenURL(Application.persistentDataPath);
        }
        else if (GUILayout.Button("Delete Save File")) {
            System.IO.File.Delete(SaveManager.path);
        }
    }
}
