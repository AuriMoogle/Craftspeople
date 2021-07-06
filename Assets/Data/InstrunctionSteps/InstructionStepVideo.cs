namespace CraftsPeople
{
    using System;
    using System.Collections.Generic;
    using CraftsPeople.UI;
    using UnityEngine;
    using UnityEngine.Video;

    /// <summary>
    /// Warning: Video instructions will not finish at the moment!
    /// </summary>
    [CreateAssetMenu(fileName = "InstructionStep-Video", menuName = "ScriptableObjects/InstructionStep-Video")]
    public class InstructionStepVideo : InstructionStep
    {
        [SerializeField]
        private VideoClip video;
        private GameObject videoPlayerGameObject;

        public override void Enter(TextDisplayer textDisplayer, List<InteractableObject> interactables, ToolsDisplay toolsDisplay, Action onFinished)
        {
            base.Enter(textDisplayer, interactables, toolsDisplay, onFinished);
            videoPlayerGameObject = GameObject.FindGameObjectWithTag("VideoPlayer");

            if (videoPlayerGameObject == null)
                throw new Exception("InteractionStepVideo: VideoPlayer not found. Are you missing the video player prefab in the scene?");

            VideoDisplayer videoDisplayer = videoPlayerGameObject.GetComponent<VideoDisplayer>();
            videoDisplayer.Display(video, InteractionTarget);
        }
    }
}