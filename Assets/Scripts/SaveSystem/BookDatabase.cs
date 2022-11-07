using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BookDatabase : ScriptableObject
{
    [SerializeField] private BookData[] books;
    private Dictionary<string, BookData> booksByName = new Dictionary<string, BookData>();

    private void OnEnable()
    {
        for (int i = 0; i < books.Length; i++)
        {
            booksByName[books[i].name] = books[i];
        }
        // Debug.Log($"Created dictionary with {booksByName.Count} entries.");
    }

    public BookData GetBookByName(string name) => booksByName.TryGetValue(name, out BookData book) ? book : null;
}

// #region Editor
// #if UNITY_EDITOR
// //Adapted from: https://github.com/itsschwer/schwer-scripts
// [CustomEditor(typeof(BookDatabase))]
// public class BookDatabaseInspector : Editor {
//     private const string property = "books";

//     public override void OnInspectorGUI() {
//         if (GUILayout.Button("Regenerate Book Database")) {
//             Generate();
//         }
//         GUILayout.Space(5);

//         var targ = new SerializedObject((BookDatabase)target);
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
//     [MenuItem("Assets/Create/Scriptable Object/Book Database", false, -11)]
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

//     private static void Populate(BookDatabase database) {
//         var entries = new List<BookData>();

//         var books = FindAllAssets<BookData>();
//         var names = new List<string>();
//         for (int i = 0; i < books.Length; i++) {
//             if (names.Contains(books[i].name)) {
//                 Debug.LogWarning($"BookDatabase: '{books[i].name}' was excluded from because it shares its filename with another BookData asset.");
//             }
//             else {
//                 entries.Add(books[i]);
//                 names.Add(books[i].name);
//             }
//         }

//         var prop = typeof(BookDatabase).GetField(property, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//         prop.SetValue(database, entries.ToArray());
//     }

//     private static BookDatabase GetDatabase() {
//         var databases = FindAllAssets<BookDatabase>();

//         if (databases.Length < 1) {
//             return CreateDatabase();
//         }
//         else if (databases.Length > 1) {
//             Debug.LogError($"Multiple Book Databases exist. Please delete the extra(s) and try again.");
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

//     private static BookDatabase CreateDatabase() {
//         var database = ScriptableObject.CreateInstance<BookDatabase>();

//         var path = AssetDatabase.GetAssetPath(Selection.activeObject);
//         if (path == "") {
//             path = "Assets";
//         }
//         else if (System.IO.Path.GetExtension(path) != "") {
//             path = path.Replace(System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
//         }
//         path = AssetDatabase.GenerateUniqueAssetPath($"{path}/{typeof(BookDatabase)}.asset");

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
