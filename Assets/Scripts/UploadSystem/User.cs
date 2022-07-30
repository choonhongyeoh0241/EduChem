using UnityEngine;

[System.Serializable]
public class User
{
    public string userName;
    public string school;
    public string currentScene;
    public int totalBooks;
    public int totalCrates;
    public int totalQuizzes;
   
    public User()
    {
        userName = PlayerData.playerName;
        school = PlayerData.schoolName;
        currentScene = PlayerData.sceneName;
        totalBooks = PlayerData.collectedBooks;
        totalCrates = PlayerData.completedCrates;
        totalQuizzes = PlayerData.completeCounts;
    }
}
