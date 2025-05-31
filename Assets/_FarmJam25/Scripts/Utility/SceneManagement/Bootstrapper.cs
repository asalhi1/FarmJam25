//using FMODUnity;
using UnityEngine;
using System.Collections.Generic;
using MEC;
using UnityEngine.SceneManagement;

namespace NJG.Utilities.SceneManagement
{
    public class Bootstrapper : MonoBehaviour
    {
        // [SerializeField]
        // private string _nextSceneName = "1_Splash";
        // [SerializeField]
        // private string _fmodBankName = "Master";

        //private void Start() => Timing.RunCoroutine(LoadFMODBankRoutine().CancelWith(gameObject));

        // private IEnumerator<float> LoadFMODBankRoutine()
        // {
        //     RuntimeManager.LoadBank(_fmodBankName);
        //     
        //     while (!RuntimeManager.HasBankLoaded(_fmodBankName))
        //         yield return Timing.WaitForOneFrame;
        //
        //     Debug.Log($"[Bootstrapper] FMOD Bank '{_fmodBankName}' loaded.");
        //     SceneManager.LoadScene(_nextSceneName);
        // }
    }
}

