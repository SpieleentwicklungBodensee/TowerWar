using UnityEngine;

public class Music : MonoBehaviour
{
    public  AudioSource audioSource;
    public  AudioClip   intro;
    public  AudioClip   main;
    public  AudioClip   end;
    private Mode        _mode = Mode.Intro;

    // Start is called before the first frame update
    void Start()
    {
        SetMode(Mode.Intro);
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if(_mode == Mode.Intro)
                SetMode(Mode.Main);
        }
    }

    public void SetMode(Mode mode)
    {
        _mode = mode;

        switch (_mode)
        {
            case Mode.Intro:
                audioSource.clip = intro;
                audioSource.loop = false;
                audioSource.Play();
                break;
            case Mode.Main:
                audioSource.clip = main;
                audioSource.loop = true;
                audioSource.Play();
                break;
            case Mode.End:
                audioSource.clip = end;
                audioSource.loop = false;
                audioSource.Play();
                break;
        }
    }

    public enum Mode
    {
        Intro,
        Main,
        End
    }
}