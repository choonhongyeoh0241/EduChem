using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class QuizOption : MonoBehaviour
{
    private MultipleChoiceQuestion.Response _response;
    public MultipleChoiceQuestion.Response response{
        get => _response;

        set{
            _response = value;
            text.text = _response?.option;
            image.color = initialColor;
        }
    }

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private Color initialColor;

    private void OnValidate() => initialColor = image.color;
    public void AnswerColor(bool isCorrect) => image.color = isCorrect ? Color.green : Color.red;
}
