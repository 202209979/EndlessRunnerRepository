using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private bool enableAudio; 
    [SerializeField] private AudioClip backgroundMusic;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        if (!enableAudio)
        {
            return;
        }

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }
}
