using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetHostName : MonoBehaviour
{
    [SerializeField] private TMP_InputField addressInput = null;

    private void Awake()
    {
        PongNetworkManager.singleton.networkAddress = "localhost";
    }

    public void NewHostName()
    {
        PongNetworkManager.singleton.networkAddress = addressInput.text;
    }


}
