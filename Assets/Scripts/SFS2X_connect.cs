using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;

public class SFS2X_connect : MonoBehaviour {

    public string serverIP = "localhost";
    public int serverPort = 9933;
    public string zoneName = "BasicExamples";
    public string userName = "Test";


    SmartFox sfs;

	// Use this for initialization
	void Start () {
        print("Started SFS");
        sfs = new SmartFox();
        sfs.ThreadSafeMode = true;
        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);

        sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnTestRespond);
        sfs.Connect(serverIP, serverPort);
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
            Login();
        } else
        {
            printBaseEvent(e);
        }
    }

    void OnLogin(BaseEvent e)
    {
        print("Login Successfull");
        sendTestMessage();
    }

    void sendTestMessage()
    {
        ISFSObject output = new SFSObject();
        output.PutInt("numA", 15);
        output.PutInt("numB", 20);

        sfs.Send(new ExtensionRequest("SumNumbers", output));
    }

    void OnLoginError(BaseEvent e)
    {
        print("Login Failled with errorCode: " + e.Params["errorCode"] + e.Params["errorMessage"] );
    }

    private void Login()
    {
        if (sfs.IsConnected) {
            sfs.Send(new LoginRequest(userName, "", zoneName));
        } 
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

    void OnTestRespond(BaseEvent e)
    {
        string command = (string)e.Params["cmd"];
        ISFSObject inObject = (SFSObject)e.Params["params"];

        if (command == "SumNumbers")
        {
            Debug.Log("Responded with " + inObject.GetInt("result"));
        }

    }

}
