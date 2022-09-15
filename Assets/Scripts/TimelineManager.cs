using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private GameObject timelineObject;
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject SkipButton;
    [SerializeField] private string keyName = "firstTime";
    [SerializeField] private int number;

    private string _destroyed = "timeline";
    public string destroyed => _destroyed = "timeline";

    private void OnEnable() => _playableDirector.stopped += OnTimelineStopped;
    private void OnDisable() => _playableDirector.stopped -= OnTimelineStopped;
    private void Awake() 
    {
        skippable();
        if (SkipButton == null) return;
    }

    private void Start() 
    {
        string Destroyed = PlayerPrefs.GetString(_destroyed);

        if (Destroyed == "true")
        {
            GetRid(SkipButton, timelineObject);
        }
    }

    private void OnTimelineStopped(PlayableDirector timeline)
    {
        if (timeline == null) return;
        timeline = _playableDirector;
        Debug.Log("Stopped");

        GetRid(SkipButton, timelineObject);
    }
    public void PlayScene(float time)
    {
        GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
        Destroy(panel, 1);
        _playableDirector.time = time;
    }

    private void GetRid(GameObject button, GameObject cutscene)
    {
        Destroy(button);
        Destroy(cutscene);
        PlayerPrefs.SetString(destroyed, "true");
    }

    private void skippable()
    {
        if (!PlayerPrefs.HasKey(keyName))
        {
            SkipButton.SetActive(false);
            PlayerPrefs.SetInt(keyName, number);
        }
        else
        {
            SkipButton.SetActive(true);
        }
    }
}

