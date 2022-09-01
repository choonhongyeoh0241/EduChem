using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuizDatabase : ScriptableObject
{
    [SerializeField] private QuizData[] quizzes;
    private Dictionary<string, QuizData> quizzesByName = new Dictionary<string, QuizData>();

    private void OnEnable()
    {
        for (int i = 0; i < quizzes.Length; i++)
        {
            quizzesByName[quizzes[i].name] = quizzes[i];
        }
    }

    public QuizData GetQuizByName(string name) => quizzesByName.TryGetValue(name, out QuizData quiz) ? quiz : null;
}

// #region Editor
// #if UNITY_EDITOR
// // Adapted from: https://github.com/itsschwer/schwer-scripts
// [CustomEditor(typeof(QuizDatabase))]
// public class QuizDatabaseInspector : Editor {
//     private const string property = "quizzes";

//     public override void OnInspectorGUI() {
//         if (GUILayout.Button("Regenerate Quiz Database")) {
//             Generate();
//         }
//         GUILayout.Space(5);

//         var targ = new SerializedObject((QuizDatabase)target);
//         var prop = targ.FindProperty(property);

//         if (prop.isArray && prop.propertyType != SerializedPropertyType.String) {
//             var size = prop.arraySize;
//             var name = prop.displayName;
//             GUILayout.Label($"{name} ({size})");

//             foreach (SerializedProperty elem in prop) {
//                 if (elem.propertyType == SerializedPropertyType.ObjectReference) {
//                     using (new EditorGUI.DisabledScope(true)) {
//                         EditorGUILayout.PropertyField(elem, GUIContent.none);
//                     }
//                 }
//             }
//         }
//     }

//     #region Utility
//     [MenuItem("Assets/Create/Scriptable Object/Quiz Database", false, -11)]
//     public static void Generate() {
//         var database = GetDatabase();
//         if (database == null) return;

//         Populate(database);

//         EditorUtility.SetDirty(database);
//         AssetDatabase.SaveAssets();
//         AssetDatabase.Refresh();
//         EditorUtility.FocusProjectWindow();
//         Selection.activeObject = database;
//     }

//     private static void Populate(QuizDatabase database) {
//         var entries = new List<QuizData>();

//         var quizzes = FindAllAssets<QuizData>();
//         var names = new List<string>();
//         for (int i = 0; i < quizzes.Length; i++) {
//             if (names.Contains(quizzes[i].name)) {
//                 Debug.LogWarning($"QuizDatabase: '{quizzes[i].name}' was excluded from because it shares its filename with another QuizData asset.");
//             }
//             else {
//                 entries.Add(quizzes[i]);
//                 names.Add(quizzes[i].name);
//             }
//         }

//         var prop = typeof(QuizDatabase).GetField(property, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//         prop.SetValue(database, entries.ToArray());
//     }

//     private static QuizDatabase GetDatabase() {
//         var databases = FindAllAssets<QuizDatabase>();

//         if (databases.Length < 1) {
//             return CreateDatabase();
//         }
//         else if (databases.Length > 1) {
//             Debug.LogError($"Multiple Quiz Databases exist. Please delete the extra(s) and try again.");
//             return null;
//         }
//         else {
//             return databases[0];
//         }
//     }

//     private static T[] FindAllAssets<T>() where T : Object {
//         var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
//         var instances = new T[guids.Length];
//         for (int i = 0; i < guids.Length; i++) {
//             string path = AssetDatabase.GUIDToAssetPath(guids[i]);
//             instances[i] = AssetDatabase.LoadAssetAtPath<T>(path);
//         }
//         return instances;
//     }

//     private static QuizDatabase CreateDatabase() {
//         var database = ScriptableObject.CreateInstance<QuizDatabase>();

//         var path = AssetDatabase.GetAssetPath(Selection.activeObject);
//         if (path == "") {
//             path = "Assets";
//         }
//         else if (System.IO.Path.GetExtension(path) != "") {
//             path = path.Replace(System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
//         }
//         path = AssetDatabase.GenerateUniqueAssetPath($"{path}/{typeof(QuizDatabase)}.asset");

//         AssetDatabase.CreateAsset(database, path);
//         AssetDatabase.SaveAssets();
//         AssetDatabase.Refresh();
//         EditorUtility.FocusProjectWindow();
//         return database;
//     }
//     #endregion
// }
// #endif
// #endregion
