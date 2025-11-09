using UnityEngine;

public class MusicClass : MonoBehaviour
{
    public static MusicClass music;
    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (music == null) {
		    music = this;
        } else {
            DestroyObject(gameObject);
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
}