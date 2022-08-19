using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class NetworkManger : MonoBehaviourPunCallbacks
{
    [Header("Connection Status")]
    public Text connectionStatusText;

    [Header("Login UI Panel")]
    public InputField playerNameInput;
    public GameObject Login_UI_Panel;

    [Header("Game Options UI Panel")]
    public GameObject GameOptions_UI_Panel;
    #region Unity Method
    // Start is called before the first frame update
    void Start()
    {
        ActivatePanel(Login_UI_Panel.name);
        Login_UI_Panel.SetActive(true);
        GameOptions_UI_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        connectionStatusText.text = "Connection status" + PhotonNetwork.NetworkClientState;   
    }
    #endregion
    #region UI Callbacks
    public void OnLoginButtonClicked()
    {
        string playerName = playerNameInput.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Playername is invalid!");
        }
    }
    #endregion
    #region Photon Callbacks
    public override void OnConnected()
    {
        Debug.Log("Connected to internet");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName+ " is connected to photon");
        ActivatePanel(GameOptions_UI_Panel.name);
    }
    #endregion
    #region Public Methods
    public void ActivatePanel(string panelToBeActivated)
    {
        Login_UI_Panel.SetActive(panelToBeActivated.Equals(Login_UI_Panel.name));
        GameOptions_UI_Panel.SetActive(panelToBeActivated.Equals(GameOptions_UI_Panel.name));
    }
    #endregion
}
