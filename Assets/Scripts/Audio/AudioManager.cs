using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _pickUpCoinSound;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one AudioManager!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        _sfxSource.PlayOneShot(clip, volume);
    }
    
    public void PlayPickUpCoinSound(float volume = 1f)
    {
        _sfxSource.PlayOneShot(_pickUpCoinSound, volume);
    }

    public void PlayClickSound()
    {
        PlaySound(_clickSound);
    }
}
