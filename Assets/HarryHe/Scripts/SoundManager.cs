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

    [Header("玩家基础动作音效")]
    public AudioClip playerPullsRopeSound; // 玩家拉扯绳子音效
    public AudioClip playerReleasesRopeSound; // 玩家释放绳子音效
    public AudioClip grapplingHookHooksPackageSound; // 钩爪勾上包裹音效
    public AudioClip packagePlacedInBasketSound; // 包裹放入篮筐音效

    [Header("关卡机关音效")]
    public AudioClip trapRopeRetrievalSound; // 陷阱绳子回收音效
    public AudioClip baffleItemTouchingPlateSound; // 挡板物品触碰板块音效
    public AudioClip laserTriggerSound; // 激光触发音效
    public AudioClip windFieldAmbientSound; // 风场环境音效
    public AudioClip timedGateOpeningSound; // 定时闸门开启音效
    public AudioClip timedGateClosingSound; // 定时闸门关闭音效
    public AudioClip timedBombExplosionSound; // 定时炸弹爆炸音效

    [Header("UI音效")]
    public AudioClip successSound; // 成功音效
    public AudioClip failSound; // 失败音效
    public AudioClip menuButtonSound; // 菜单按键音效

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
        // 音频池已在InitializeSoundManager中设置
    }

    private void InitializeSoundManager()
    {
        audioSourcePool = new List<AudioSource>();
        originalVolumes = new Dictionary<AudioSource, float>();
        SetupAudioSourcePool(); // 在初始化时立即设置音频池
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
        // 检查音频池是否为空
        if (audioSourcePool == null || audioSourcePool.Count == 0)
        {
            Debug.LogError("AudioSource pool is empty! Make sure audioSourcePoolSize > 0");
            return null;
        }
        
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
        if (audioSource == null)
        {
            Debug.LogWarning("No available AudioSource to play sound: " + clip.name);
            return;
        }
        else
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