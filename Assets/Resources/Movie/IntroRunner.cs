using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroRunner : MonoBehaviour
{
    void Start() {
        var videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        if (videoPlayer) {
            videoPlayer.loopPointReached += ((vp) => EndMovie());
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)
            || Input.GetKeyDown(KeyCode.Return)
            || Input.GetKeyDown(KeyCode.Space)
            || Input.GetMouseButtonDown(0))
        {
            EndMovie();
        }
    }

    void EndMovie()
    {
        if (movieEnd != null)
        {
            movieEnd();
        }
        enabled = false;
    }

    public delegate void MovieEnd();
    public static MovieEnd movieEnd;
}