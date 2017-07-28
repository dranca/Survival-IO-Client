using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using UnityEngine.SceneManagement;

public interface SFS2XConnectInput {
    void signInWithName(string name);
}

public class SFS2X_connect : MonoBehaviour, SFS2XConnectInput {

    public string serverIP = "localhost";
    public int serverPort = 9933;
    public string zoneName = "BasicExamples";
    public string userName = "Test";

    public string mainSceneName = "GameScene";

    public string mainMenuScene = "Main";
    
    SmartFox sfs;

	// Use this for initialization
	void Start () {
        bool otherSFS2XExists = GameObject.FindObjectsOfType<SFS2X_connect>().Length > 1;

        if (otherSFS2XExists)
        {
            print("Delete this object");
            DestroyObject(this.gameObject);
            return;
        }

        print("Try to connect");
        sfs = new SmartFox();
        sfs.ThreadSafeMode = true;
        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        sfs.Connect(serverIP, serverPort);

        DontDestroyOnLoad(gameObject);
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

    void OnLogin(BaseEvent e)
    {   
        SceneManager.LoadScene(mainSceneName);
    }

    void OnLoginError(BaseEvent e)
    {
        print("Login Failled with errorCode: " + e.Params["errorCode"] + e.Params["errorMessage"] );
    }

    private void FixedUpdate()
    {
        sfs.ProcessEvents();
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
            message += e.Params[key];
        }

        print(message);
    }

    public void signInWithName(string name)
    {
        if (sfs.IsConnected)
        {
            sfs.Send(new LoginRequest(name, "", zoneName));
        }
    }
}