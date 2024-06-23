using LilaMachinimaMod;
using MelonLoader;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

[assembly: MelonInfo(typeof(LilaMachinimaMod.LilaMachinimaMod), "Lila Machinima Mod", "0.2.0", "raito133")]
[assembly: MelonPriority(200)]
namespace LilaMachinimaMod
{
    public class LilaMachinimaMod : MelonMod
    {
        VideoManager? _videoManager;
        DialogManager? _dialogManager;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            LoggerInstance.Msg($"Scene {sceneName} with build index {buildIndex} has been loaded!");
            try
            {
                SetLilaManagerComponents();
            }
            catch (System.Exception e)
            {
                LoggerInstance.Error("Error when initializing: ", e);
            }
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();

            if (Keyboard.current.f5Key.wasPressedThisFrame)
            {
                if(_dialogManager == null)
                {
                    LoggerInstance.Error("Dialog Manager is null!");
                    return;
                }
                _dialogManager.LaunchDialog();
            }

            if (Keyboard.current.f6Key.wasPressedThisFrame)
            {
                if(_videoManager == null)
                {
                    LoggerInstance.Error("Video Manager is null!");
                    return;
                }
                _videoManager.PlayVideo();
            }
        }

        private void SetLilaManagerComponents()
        {
            var gameManager = GameObject.Find("GameManager");
            _videoManager = new VideoManager(GameObject.Find("Videoplayer").GetComponent<VideoPlayer>());
            _dialogManager = new DialogManager(gameManager.GetComponent<DialogParser>(), gameManager.GetComponent<TextManager>());
        }
    }
}
