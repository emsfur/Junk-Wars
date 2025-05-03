using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TextMeshProUGUI playerCountText;
    [SerializeField] private TextMeshProUGUI PlayerPoints;
    //[SerializeField] private PlayerCard playerCardPrefab;
    //[SerializeField] private Transform playerCardParent;
    //[SerializeField] private playerInputClass playerInput;
    [SerializeField]private PlayerInputClassM1 playerInput;
    private PlayerInputClassM1 playerInputOther;
    //private playerInputClass playerInputOther;
    //Below I think it should be fine, if not determine needed later
    //private Dictionary<int, PlayerCard> playerCards = new Dictionary<int, PlayerCard>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private NetworkVariable<int> playerNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

 private void Awake()
    {
        playerInputOther = playerInput.GetComponent<PlayerInputClassM1>();
      //  playerInputOther = playerInput.GetComponent<playerInputClass>();
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
    private void Update()
    {   
         playerCountText.text="Num Player: "+ playerNum.Value.ToString();
         //PlayerPoints.text = "Points:" + playerInputOther.thisScore.ToString();
         //PlayerPoints.text = "Points: " + playerInputOther.GetComponent<playerInputClass>().thisScore.ToString();
        //Debug.Log("points are"+ playerInput.GetComponent<playerInputClass>().thisScore.ToString());
        if(!IsServer) {
            return;
        }
        playerNum.Value=NetworkManager.Singleton.ConnectedClients.Count;   
       
    }
}
