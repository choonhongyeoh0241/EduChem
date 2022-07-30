using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Object/Book Data")]
public class BookData : ScriptableObject 
{
    [SerializeField] private Color _color = Color.white; 
    public Color color => _color; 

    [SerializeField] private string _title; 
    public string title => _title; 

    [SerializeField][Multiline(10)] private string[] _pages; 
    public string[] pages => _pages; 
}
