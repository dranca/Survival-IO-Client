using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using UnityEngine.SceneManagement;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using System.Collections.Generic;

public interface SFS2XConnectInput {
    void signInWithName(string name);
    void sendTransformData(Transform player);
    void sendRotationData(double euler);
}

public class SFS2X_connect : MonoBehaviour, SFS2XConnectInput {

    public string serverIP = "localhost";
    public int serverPort = 9933;
    public string zoneName = "BasicExamples";
    public string userName = "Test";

    public string mainSceneName = "GameScene";

    public string mainMenuScene = "Main";

    public string roomName = "Europe";

    List<UserVariable> listToBeSent = new List<UserVariable>();
    
    SmartFox sfs;

    UserManagerInput userManager;

	// Use this for initialization
	void Start () {
        bool otherSFS2XExists = GameObject.FindObjectsOfType<SFS2X_connect>().Length > 1;

        if (otherSFS2XExists)
        {
            print("Delete this object");
            DestroyObject(this.gameObject);
            return;
        }

        userManager = gameObject.GetComponent<UserManagerInput>();

        if (userManager == null)
        {
            Debug.LogError("We could not find the user manager");
        }
        
        sfs = new SmartFox();
        sfs.ThreadSafeMode = true;

        AddEventListeners();

        sfs.Connect(serverIP, serverPort);

        DontDestroyOnLoad(gameObject);
	}

    void AddEventListeners()
    {
        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
        sfs.AddEventListener(SFSEvent.PROXIMITY_LIST_UPDATE, OnProximityListUpdate);
        sfs.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVariableUpdate);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnDisconnect);
    }

    void OnSocketError(BaseEvent e)
    {
        print(e.Params["errorMessage"]);
    }

    void OnConnection(BaseEvent e)
    {
        if ((bool) e.Params["success"])
        {
            Debug.Log("Connected successfully");
            if (Debug.isDebugBuild)
            {
                signInWithName("F11");
            }
        } else
        {
            printBaseEvent(e);
        }
    }

    void OnRoomJoin(BaseEvent e)
    {
        ISFSObject output = new SFSObject();
        IRequest request = new ExtensionRequest("PositionRandom", output);
        print("OnRoom Join");
        sfs.Send(request);
    }

    void OnLogin(BaseEvent e)
    {   
        SceneManager.LoadScene(mainSceneName);
        sfs.Send(new JoinRoomRequest(roomName));
        print("Join Room");
    }

    void OnLoginError(BaseEvent e)
    {
        print("Login Failled with errorCode: " + e.Params["errorCode"] + e.Params["errorMessage"] );
    }

    void OnProximityListUpdate(BaseEvent e)
    {
        userManager.ProximityListUpdated(e);
    }

    void OnUserVariableUpdate(BaseEvent e)
    {
        userManager.UserVariablesUpdated(e);
    }

    void OnDisconnect(BaseEvent e)
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    void OnExtensionResponse(BaseEvent e)
    {
        printBaseEvent(e);
        var command = (string)e.Params["cmd"];
        if (command == "SetOnMap")
        {
            OnSetOnMap(e);
        }
    }

    void OnSetOnMap(BaseEvent e)
    {
        print("Set on Map called");
        userManager.CreateLocalUser(e);
    }

    private void FixedUpdate()
    {
        if(sfs == null)
        {
            SceneManager.LoadScene(mainMenuScene);
            return;
        }
        sfs.ProcessEvents();
        if (listToBeSent.Count > 0)
        {
            sfs.Send(new SetUserVariablesRequest(listToBeSent));
            listToBeSent.Clear();
        }
    }

    private void OnApplicationQuit()
    {
        if(sfs.IsConnected)
        {
            print("Application Quit");
            sfs.Disconnect();
        }
    }

    void printBaseEvent(BaseEvent e)
    {
        string message = "";
        foreach(string key in e.Params.Keys)
        {
            message += key + " " + e.Params[key] + " ";
        }

        print(message);
    }

    public void signInWithName(string name)
    {
        if (sfs.IsConnected)
        {
            sfs.Send(new LoginRequest(name, "", zoneName));
        } else
        {
            sfs.Connect();
        }
    }

    public  void sendTransformData(Transform player)
    {
        listToBeSent.Add(new SFSUserVariable("x", (double)player.position.x));
        listToBeSent.Add(new SFSUserVariable("y", (double)player.position.y));
    }

    public void sendRotationData(double euler)
    {
        listToBeSent.Add(new SFSUserVariable("rot", euler));
    }
}