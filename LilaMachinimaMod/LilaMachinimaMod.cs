using LilaMachinimaMod;
using MelonLoader;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

[assembly: MelonInfo(typeof(LilaMachinima.LilaMachinimaMod), "Lila Machinima Mod", "0.1.0", "Skeleton Programmer")]
namespace LilaMachinima
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

            if (Keyboard.current.f5Key.isPressed)
            {
                if(_dialogManager == null)
                {
                    LoggerInstance.Error("Dialog Manager is null!");
                    return;
                }
                _dialogManager.LaunchDialog();
            }

            if (Keyboard.current.f6Key.isPressed)
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
