using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    //UI and logic for Client or Host selection and starting
    private void Awake()
    {

        hostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            gameObject.SetActive(false); // hides buttons once pressed
        });
        
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            

            gameObject.SetActive(false); // hides buttons once pressed
        });
    }
}
