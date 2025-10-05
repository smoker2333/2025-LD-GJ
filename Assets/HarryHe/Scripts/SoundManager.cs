using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("音效设置")] public float volume = 1f;
    public bool isMuted;

    [Header("音效池设置")] public int audioSourcePoolSize = 10;
    private List<AudioSource> audioSourcePool;
    private Dictionary<AudioSource, float> originalVolumes;
    private int currentAudioSourceIndex = 0;

    [Header("音效")] public AudioClip buttonClickSound;
    public AudioClip buttonHoverSound;
    public AudioClip skillUseSound;
    public AudioClip enemyDeathSound;
    public AudioClip playerDamageSound;
    public AudioClip bossDamageSound;
    public AudioClip explosionSound;
    public AudioClip levelCompleteSound;
    public AudioClip victorySound;
    public AudioClip defeatSound;
    public AudioClip gameOverSound;
    public AudioClip countdownSound;
    public AudioClip bossLandSound;
    
    public AudioClip playerAttack1Sound; // Attack1音效
    public AudioClip playerAttack3Sound; // Attack3音效

    [Header("Bonus界面音效")] public AudioClip bonusMusic;
    public AudioClip rollButtonClickSound;
    public AudioClip rollCompleteSound;
    public AudioClip skillSelectSound;
    public AudioClip confirmButtonSound;
    public AudioClip slotMachineRollingSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSoundManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetupAudioSourcePool();
    }

    private void InitializeSoundManager()
    {
        audioSourcePool = new List<AudioSource>();
        originalVolumes = new Dictionary<AudioSource, float>();
    }

    private void SetupAudioSourcePool()
    {
        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = volume;
            audioSource.mute = isMuted;
            audioSource.playOnAwake = false;
            audioSourcePool.Add(audioSource);
            originalVolumes[audioSource] = 1f;
        }
    }

    // 获取下一个可用的AudioSource
    private AudioSource GetNextAvailableAudioSource()
    {
        // 查找当前可用的AudioSource
        for (int i = 0; i < audioSourcePool.Count; i++)
        {
            int index = (currentAudioSourceIndex + i) % audioSourcePool.Count;
            if (!audioSourcePool[index].isPlaying)
            {
                currentAudioSourceIndex = index;
                return audioSourcePool[index];
            }
        }
        
        currentAudioSourceIndex = (currentAudioSourceIndex + 1) % audioSourcePool.Count;
        return audioSourcePool[currentAudioSourceIndex];
    }

    // 播放音效
    public void PlaySound(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (isMuted || clip == null) return;

        AudioSource audioSource = GetNextAvailableAudioSource();
        if (audioSource != null)
        {
            // 存储原始音量
            originalVolumes[audioSource] = volume;

            audioSource.clip = clip;
            audioSource.volume = volume * this.volume;
            audioSource.pitch = pitch;
            audioSource.Play();
        }
    }

    // 设置音量
    public void SetVolume(float volume)
    {
        this.volume = Mathf.Clamp01(volume);
        // 遍历池中的所有AudioSource并更新音量
        if (audioSourcePool != null)
        {
            foreach (AudioSource audioSource in audioSourcePool)
            {
                if (audioSource != null && originalVolumes.ContainsKey(audioSource))
                {
                    // 使用存储的原始音量计算最终音量
                    audioSource.volume = originalVolumes[audioSource] * this.volume;
                }
            }
        }
    }

    // 静音切换
    public void ToggleMute()
    {
        isMuted = !isMuted;
        // 遍历池中的所有AudioSource并更新静音状态
        foreach (AudioSource audioSource in audioSourcePool)
        {
            audioSource.mute = isMuted;
        }
    }

    // 停止音效
    public void StopSound()
    {
        // 遍历池中的所有AudioSource并停止播放
        foreach (AudioSource audioSource in audioSourcePool)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    // 停止特定音效
    public void StopSound(AudioClip clip)
    {
        foreach (AudioSource audioSource in audioSourcePool)
        {
            if (audioSource.isPlaying && audioSource.clip == clip)
            {
                audioSource.Stop();
            }
        }
    }

    // 暂停所有音效
    public void PauseAllSounds()
    {
        foreach (AudioSource audioSource in audioSourcePool)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    // 恢复所有音效
    public void ResumeAllSounds()
    {
        foreach (AudioSource audioSource in audioSourcePool)
        {
            if (audioSource.clip != null && !audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }
    }

    // 获取当前播放的音效数量
    public int GetPlayingSoundCount()
    {
        int count = 0;
        foreach (AudioSource audioSource in audioSourcePool)
        {
            if (audioSource.isPlaying)
            {
                count++;
            }
        }

        return count;
    }

    // 获取当前音量设置
    public float GetVolume() => volume;
    public bool IsMuted() => isMuted;
}