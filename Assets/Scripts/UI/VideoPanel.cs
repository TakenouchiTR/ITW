using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPanel : MonoBehaviour
{
    private const string PlaySymbol = "▶";
    private const string PauseSymbol = "Ⅱ";

    [SerializeField]
    VideoPlayer vid_player;
    [SerializeField]
    Text txt_buttonText;

    public UnityEvent Closed;

    private void Start()
    {
        this.vid_player.loopPointReached += OnVidPlayerLoopPointReached;
    }
    
    public void PlayVideo(string name)
    {
        string filePath = $"{Application.streamingAssetsPath}/Video/{name}";

        this.vid_player.url = filePath;
        this.vid_player.Play();
    }

    private void UpdatePlayPauseButton()
    {
        txt_buttonText.text = this.vid_player.isPlaying ? PauseSymbol : PlaySymbol;
    }

    private void OnVidPlayerLoopPointReached(VideoPlayer source)
    {
        this.UpdatePlayPauseButton();
    }

    public void OnPlayPausedPressed()
    {
        if (this.vid_player.isPlaying)
        {
            this.vid_player.Pause();
        }
        else
        {
            this.vid_player.Play();
        }
        this.UpdatePlayPauseButton();
    }

    public void OnClosePressed()
    {
        this.vid_player.Stop();
        this.gameObject.SetActive(false);
        this.Closed?.Invoke();
    }
}
