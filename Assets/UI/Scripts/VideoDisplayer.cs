
namespace CraftsPeople
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Video;

    public class VideoDisplayer : MonoBehaviour
    {
        [SerializeField]
        private Image previewImage;
        [SerializeField]
        private RawImage videoImage;
        [SerializeField]
        private VideoPlayer videoPlayer;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private GameObject startVideoButton;

        private Coroutine playVideo;
        private WaitForEndOfFrame waitForFrameEnd = new WaitForEndOfFrame();

        public void Display(VideoClip video, Sprite displayImage)
        {
            startVideoButton.SetActive(true);
            previewImage.sprite = displayImage;

#if UNITY_WEBGL && UNITY_EDITOR == false
            // WebGL does not support embedded videos.
            videoPlayer.source = VideoSource.Url;
            var url = System.IO.Path.Combine(Application.streamingAssetsPath, video.name + ".mp4");
            videoPlayer.url = url;
#else
            videoPlayer.source = VideoSource.VideoClip;
            videoPlayer.clip = video;
#endif

            // Audio
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.controlledAudioTrackCount = 1;
            videoPlayer.EnableAudioTrack(0, true);
            videoPlayer.SetTargetAudioSource(0, audioSource);

            videoPlayer.Prepare();
        }

        public void ToggleVideo()
        {
            if (videoPlayer.isPaused)
            {
                videoPlayer.Play();
                audioSource.Play();
                return;
            }
            else if (videoPlayer.isPlaying)
                PauseVideo();
            else
                StartVideo();
        }

        public void StartVideo()
        {
            // Cancel preparation
            if (playVideo != null)
                StopCoroutine(playVideo);

            // Start preparation
            playVideo = StartCoroutine(Play());
        }

        public void PauseVideo()
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                audioSource.Stop();
            }
        }

        private IEnumerator Play()
        {
            while (videoPlayer.isPrepared == false)
                yield return waitForFrameEnd;

            videoImage.texture = videoPlayer.texture;
            videoPlayer.Play();
            audioSource.Play();
        }
    }
}
