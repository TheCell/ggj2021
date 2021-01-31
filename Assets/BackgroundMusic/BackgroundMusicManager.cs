using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    public ActiveMusic ActiveMusic
    {
        get { return activeMusic; }
        set
        {
            activeMusic = value;

            if (trackSwitched)
            {
                PrepareToSwitchTrackAndSwitch(value);
            }
        }
    }

    [SerializeField]
    private ActiveMusic activeMusic = ActiveMusic.GameTrack;
    [SerializeField]
    private AudioSource DialogueAudioSource;
    [SerializeField]
    private AudioSource BillboardAudioSource;
    [SerializeField]
    private AudioSource SceneAudioSource;
    [SerializeField]
    private float switchTime = 3f;

    private float switchingStartedTime;
    private bool trackSwitched = true;
    private AudioSource fadingIn;
    private AudioSource fadingOut;


    public void Start()
    {
        DontDestroyOnLoad(this);

        if (DialogueAudioSource == null)
        {
            Debug.LogError("missing DialogueAudioSource");
        }
        if (BillboardAudioSource == null)
        {
            Debug.LogError("missing BillboardAudioSource");
        }
        if (SceneAudioSource == null)
        {
            Debug.LogError("missing SceneAudioSource");
        }

        DialogueAudioSource.volume = 0f;
        BillboardAudioSource.volume = 0f;
        SceneAudioSource.volume = 0f;
        switchingStartedTime = Time.time;
        fadingIn = SceneAudioSource;
        fadingIn.volume = 1;

        SceneManager.activeSceneChanged += SceneChanged;
    }

    private void PrepareToSwitchTrackAndSwitch(ActiveMusic switchTo)
    {
        trackSwitched = false;
        switchingStartedTime = Time.time;
        fadingOut = fadingIn;

        switch (switchTo)
        {
            case ActiveMusic.GameTrack:
                fadingIn = SceneAudioSource;
                break;
            case ActiveMusic.DialogueTrack:
                fadingIn = DialogueAudioSource;
                break;
            case ActiveMusic.BillboardTrack:
                fadingIn = BillboardAudioSource;
                break;
        }
        
        StartCoroutine("Fade");
    }
    
    private void SceneChanged(Scene current, Scene next)
    {
        if (current == null)
        {
            return;
        }

        switch(next.buildIndex)
        {
            case 5:
                // Bulletin board
                ActiveMusic = ActiveMusic.BillboardTrack;
                break;
            default:
                ActiveMusic = ActiveMusic.GameTrack;
                break;
        }
    }

    IEnumerator Fade()
    {
        var finishtime = switchingStartedTime + switchTime;
        float t = 0;

        while (Time.time < finishtime)
        {
            t = (Time.time - switchingStartedTime) / (finishtime - switchingStartedTime);
            fadingOut.volume = 1 - t;
            fadingIn.volume = t;

            yield return null;
        }


        fadingOut.volume = 0;
        fadingIn.volume = 1;

        trackSwitched = true;
    }
}
