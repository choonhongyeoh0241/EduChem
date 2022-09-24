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
    [SerializeField] private string keyName;
    [SerializeField] private int number;
    private void OnEnable() => _playableDirector.stopped += OnTimelineStopped;
    private void OnDisable() => _playableDirector.stopped -= OnTimelineStopped;
    private void Awake() 
    {
        skippable();
        if (SkipButton == null) return;
    }

    private void OnTimelineStopped(PlayableDirector timeline)
    {
        if (timeline == null) return;
        timeline = _playableDirector;
        Debug.Log("Stopped");

        Destroy(SkipButton);
        Destroy(timelineObject);
    }

    public void PlayScene(float time)
    {
        GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
        Destroy(panel, 1);
        _playableDirector.time = time;
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

