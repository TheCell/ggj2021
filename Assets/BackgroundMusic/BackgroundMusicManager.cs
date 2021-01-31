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
            if (activeMusic != value)
            {
                activeMusic = value;

                if (trackSwitched)
                {
                    PrepareToSwitchTrackAndSwitch(value);
                }
            }
        }
    }

    private ActiveMusic activeMusic = ActiveMusic.GameTrack;
    //[SerializeField]
    //private AudioSource DialogueAudioSource;
    [SerializeField]
    private AudioSource GameTrackAudioSource;
    [SerializeField]
    private AudioSource BarTrackAudioSource;
    [SerializeField]
    private AudioSource ToilettTrackAudioSource;
    [SerializeField]
    private AudioSource BillboardAudioSource;
    [SerializeField]
    private float switchTime = 1f;

    private float switchingStartedTime;
    private bool trackSwitched = true;
    private AudioSource fadingIn;
    private AudioSource fadingOut;


    public void Start()
    {
        DontDestroyOnLoad(this);

        if (GameTrackAudioSource == null)
        {
            Debug.LogError("missing GameTrackAudioSource");
        }
        if (BarTrackAudioSource == null)
        {
            Debug.LogError("missing BarTrackAudioSource");
        }
        if (ToilettTrackAudioSource == null)
        {
            Debug.LogError("missing ToilettTrackAudioSource");
        }
        if (BillboardAudioSource == null)
        {
            Debug.LogError("missing BillboardAudioSource");
        }

        GameTrackAudioSource.volume = 0f;
        BarTrackAudioSource.volume = 0f;
        ToilettTrackAudioSource.volume = 0f;
        BillboardAudioSource.volume = 0f;

        switchingStartedTime = Time.time;
        fadingIn = GameTrackAudioSource;
        fadingIn.volume = .5f;

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
                fadingIn = GameTrackAudioSource;
                break;
            case ActiveMusic.ToiletTrack:
                fadingIn = ToilettTrackAudioSource;
                break;
            case ActiveMusic.BillboardTrack:
                fadingIn = BillboardAudioSource;
                break;
            case ActiveMusic.BarTrack:
                fadingIn = BarTrackAudioSource;
                break;
            case ActiveMusic.GameEndTrack:
                fadingIn = GameTrackAudioSource;
                break;
        }

        // dark fantasy
        // gamestart
        // entrance too
        // warderobe

        // bar ambient
        // end scene sound vergessen
        // menu music für endscene und credits

        // toilett sound
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
            case 0: // Game Start
                ActiveMusic = ActiveMusic.GameTrack;
                break;
            case 1: // Entrance
                ActiveMusic = ActiveMusic.GameTrack;
                break;
            case 2: // Warderobe
                ActiveMusic = ActiveMusic.GameTrack;
                break;
            case 3: // Bar
                ActiveMusic = ActiveMusic.BarTrack;
                break;
            case 4: // Bathroom
                ActiveMusic = ActiveMusic.ToiletTrack;
                break;
            case 5: // Bulletin board
                ActiveMusic = ActiveMusic.BillboardTrack;
                break;
            case 6: // Game end
                ActiveMusic = ActiveMusic.BillboardTrack;
                break;
            case 7: // Credit
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
            fadingOut.volume = (1 - t) * .5f;
            fadingIn.volume = t * .5f;

            yield return null;
        }


        fadingOut.volume = 0;
        fadingIn.volume = .5f;

        trackSwitched = true;
    }
}
