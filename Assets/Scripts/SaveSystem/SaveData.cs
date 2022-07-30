using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string scene;
    public Vector2 position;
    public List<string> quizzes = new List<string>();
    public List<string> books = new List<string>();
    public List<string> crates = new List<string>();
    public List<string> completeQuizzes = new List<string>();
    public enum Flag {
        Quiz,
        Crate
    }
}
