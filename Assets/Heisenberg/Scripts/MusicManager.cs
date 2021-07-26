using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    protected const int REALITY1 = 7;
    protected const int REALITY2 = 8;
    protected const int MERGED = 9;
    private static MusicManager musicManagerInstance;


    // Music
    public AudioClip ACReality1;
    public AudioClip ACReality2;
    public AudioClip ACRealityMerged;
    public AudioClip ACRealitySplit;
    private AudioSource ASReality1;
    private AudioSource ASReality2;
    private AudioSource ASRealityMerged;
    private AudioSource ASRealitySplit;

    // SFX
    public AudioClip ACPushbox;
    public AudioClip ACDropPushbox;
    public AudioClip ACGoal;
    public AudioClip ACWarning;

    [HideInInspector]
    public AudioSource ASPushbox;
    
    [HideInInspector]
    public AudioSource ASDropPushbox;

    [HideInInspector]
    public AudioSource ASGoal;
    
    [HideInInspector]
    public AudioSource ASWarning;

    void Start() 
    {
        ASReality1 = gameObject.AddComponent<AudioSource>();
        ASReality2 = gameObject.AddComponent<AudioSource>();
        ASRealityMerged = gameObject.AddComponent<AudioSource>();
        ASRealitySplit = gameObject.AddComponent<AudioSource>();
        ASPushbox = gameObject.AddComponent<AudioSource>();
        ASDropPushbox = gameObject.AddComponent<AudioSource>();
        ASGoal = gameObject.AddComponent<AudioSource>();
        ASWarning = gameObject.AddComponent<AudioSource>();

        ASReality1.clip = ACReality1;
        ASReality2.clip = ACReality2;
        ASRealityMerged.clip = ACRealityMerged;
        ASRealitySplit.clip = ACRealitySplit;
        ASPushbox.clip = ACPushbox;
        ASDropPushbox.clip = ACDropPushbox;
        ASGoal.clip = ACGoal;
        ASWarning.clip = ACWarning;

        ASReality1.Play();
        ASReality2.Play();
        ASRealityMerged.Play();
        ASRealitySplit.Play();

        ASReality1.volume = 1;
        ASReality2.volume = 0;
        ASRealityMerged.volume = 0;
        ASRealitySplit.volume = 0;
    }
    void Awake() 
    {
        DontDestroyOnLoad(this);

        if( musicManagerInstance == null )
        {
            musicManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SwitchMusic( int layer )
    {
        ASReality1.volume = 0;
        ASReality2.volume = 0;
        ASRealityMerged.volume = 0;
        ASRealitySplit.volume = 0;

        if( layer == REALITY1 )
        {
            ASReality1.volume = 1;
        }
        else if( layer == REALITY2 )
        {
            ASReality2.volume = 1;
        }
        else if( layer == MERGED )
        {
            ASRealityMerged.volume = 1;
        }
    }

    public IEnumerator SwitchToSplitMusicCo(int layer)
    {
        ASReality1.volume = 0;
        ASReality2.volume = 0;
        ASRealityMerged.volume = 0;
        ASRealitySplit.volume = 1;

        yield return new WaitForSeconds(3f);

        float currentTime = 0;
        float duration = 2f;
        AudioSource prevSound;

        if( layer == REALITY1 )
        {
            prevSound = ASReality1;
        }
        else if( layer == REALITY2 )
        {
            prevSound = ASReality2;
        }
        else
        {
            prevSound = ASRealityMerged;
        }

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            ASRealitySplit.volume = Mathf.Lerp(1, 0, currentTime / duration);
            prevSound.volume = Mathf.Lerp(0, 1, currentTime / duration);
            yield return null;
        }
    }
}
