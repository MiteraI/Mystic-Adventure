using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class AreaEntry : MonoBehaviour
=======
public class AreaEntrance : MonoBehaviour
>>>>>>> 0953a54301447b29a5f66309b92330b120e19428
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }
    }
}
