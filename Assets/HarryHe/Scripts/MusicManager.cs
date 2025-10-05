using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    
    [Header("音乐设置")]
    public float volume = 1f;
    public float maxVolume = 0.7f;
    public bool isMuted = false;
    public bool loopMusic = true;
    public float fadeInTime = 1f;
    public float fadeOutTime = 1f;
    
    public AudioSource musicSource;
    
    [Header("背景音乐")]
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;

    
    private AudioClip currentMusic;
    private Coroutine fadeCoroutine;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeMusicManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        SetupMusicSource();
    }
    
    private void InitializeMusicManager()
    {
        if (musicSource == null)
        {
            CreateMusicSource();
        }
    }
    
    private void CreateMusicSource()
    {
        GameObject musicObj = new GameObject("MusicSource");
        musicObj.transform.SetParent(transform);
        
        musicSource = musicObj.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        
        musicSource.loop = loopMusic;
    }
    
    private void SetupMusicSource()
    {
        if (musicSource != null)
        {
            musicSource.volume = volume * maxVolume;
            musicSource.mute = isMuted;
            musicSource.loop = loopMusic;
        }
    }
    
    // 播放音乐
    public void PlayMusic(AudioClip music, bool fadeIn = true)
    {
        if (music == null) return;
        
        if (music == currentMusic && musicSource.isPlaying) 
        {
            return;
        }
        
        if (fadeIn && musicSource.isPlaying)
        {
            StartCoroutine(FadeToMusic(music));
        }
        else
        {
            StartMusic(music);
        }
    }
    
    // 立即播放音乐
    private void StartMusic(AudioClip music)
    {
        currentMusic = music;
        musicSource.clip = music;
        musicSource.volume = volume * maxVolume;
        musicSource.Play();
    }
    
    // 淡入淡出切换音乐
    private IEnumerator FadeToMusic(AudioClip newMusic)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        
        fadeCoroutine = StartCoroutine(FadeOut());
        yield return fadeCoroutine;
        
        StartMusic(newMusic);
        fadeCoroutine = StartCoroutine(FadeIn());
    }
    
    // 淡出
    private IEnumerator FadeOut()
    {
        float startVolume = musicSource.volume;
        float elapsed = 0f;
        
        while (elapsed < fadeOutTime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / fadeOutTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, progress);
            yield return null;
        }
        
        musicSource.Stop();
        musicSource.volume = startVolume;
    }
    
    // 音乐淡入效果
    private IEnumerator FadeIn()
    {
        float targetVolume = volume * maxVolume;
        float elapsed = 0f;
        
        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / fadeInTime;
            musicSource.volume = Mathf.Lerp(0f, targetVolume, progress);
            yield return null;
        }
        
        musicSource.volume = targetVolume;
    }
} 