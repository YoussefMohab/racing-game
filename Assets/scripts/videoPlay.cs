using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoPlay : MonoBehaviour
{
    public VideoPlayer videoPlayer;
  

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Load the next scene by name
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}