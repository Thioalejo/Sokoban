using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveByPlatform : MonoBehaviour
{
    [SerializeField] private bool _cancelInEditor = true;
    private RuntimePlatform _runtimePlatform;
    // Start is called before the first frame update

    private void Awake()
    {
        _runtimePlatform = Application.platform;

#if UNITY_EDITOR
        if (_cancelInEditor) return;
#endif

        if (Application.platform != RuntimePlatform.Android &&
           Application.platform != RuntimePlatform.IPhonePlayer)
        {
            Destroy(gameObject);
            return;
        }
    }


}
