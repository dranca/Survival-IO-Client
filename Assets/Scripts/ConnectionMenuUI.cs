using UnityEngine;
using UnityEngine.UI;

public class ConnectionMenuUI : MonoBehaviour {
    SFS2XConnectInput networking;
    // Use this for initialization

    public InputField textField;

	void Start () {
        findNetworking();

        if (networking == null)
        {
            Debug.LogError("The networking component was not found. Please make sure to fix this");
        }
	}
    
    private void findNetworking()
    {
        networking = GameObject.Find("Networking").GetComponent<SFS2XConnectInput>();
    }

    public void UserSelectedConnect()
    {
        string userInput = textField.text;
        if (networking != null)
        {
            networking.signInWithName(userInput);
        } else {
            Debug.LogError("Networking Not found.");
            findNetworking();
        }
    }

}
