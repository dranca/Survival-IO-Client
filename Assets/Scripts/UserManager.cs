using UnityEngine;
using System.Collections;
using Sfs2X.Core;
using Sfs2X.Entities;
using System.Collections.Generic;
using Sfs2X.Entities.Data;

public interface UserManagerInput {
    void CreateLocalUser(BaseEvent e);
    void ProximityListUpdated(BaseEvent e);
    void UserVariablesUpdated(BaseEvent e);
}

public class UserManager : MonoBehaviour, UserManagerInput {

    public Transform playerPrefab;
    private Transform localPlayer;
    private User locaLuser;

    private Dictionary<User, GameObject> users = new Dictionary<User, GameObject>();

    private void Start()
    {
        if(playerPrefab == null)
        {
            Debug.LogError("Please add Player prefab");
        }
    }

    public void CreateLocalUser(BaseEvent e)
    {
        ISFSObject input = (ISFSObject) e.Params["params"];
        float xPos = input.GetFloat("x");
        float yPos = input.GetFloat("y");
        localPlayer = GameObject.Instantiate(playerPrefab);
        localPlayer.GetComponent<MovementController>().enabled = true;
        localPlayer.GetComponentInChildren<RotationController>().enabled = true;
        localPlayer.GetComponent<Rigidbody2D>().isKinematic = false;
        localPlayer.transform.position = new Vector3(xPos, yPos);
        localPlayer.GetComponent<NetworkingMovementController>().enabled = false;
        Camera.main.GetComponent<CameraFollow>().target = localPlayer.gameObject;
    }
    public void ProximityListUpdated(BaseEvent e)
    {
        var addedUsers = (List<User>)e.Params["addedUsers"];
        var removedUsers = (List<User>)e.Params["removedUsers"];

        // Handle all new Users
        foreach (User user in addedUsers)
        {
            var pos = new Vector3(user.AOIEntryPoint.FloatX, user.AOIEntryPoint.FloatY, 0);
            var rotation = Quaternion.Euler(0, (float)user.GetVariable("rot").GetDoubleValue(), 0);
            var player = GameObject.Instantiate(playerPrefab);
            player.transform.position = pos;
            player.transform.rotation = rotation;
            player.GetComponent<PolygonCollider2D>().enabled = false;
            users[user] = player.gameObject;
        }

        foreach (User user in removedUsers)
        {
            var removedUser = users[user];
            GameObject.Destroy(removedUser);
        }
    }
    public void UserVariablesUpdated(BaseEvent evt)
    {
        ArrayList changedVars = (ArrayList)evt.Params["changedVars"];
        SFSUser user = (SFSUser)evt.Params["user"];

        if (! users.ContainsKey(user))
        {
            return;
        }
        var player = users[user];
        var networkingMovement = player.GetComponent<NetworkingMovementControllerInput>();
        bool userMadeChangesToPosition = changedVars.Contains("x") || changedVars.Contains("y");
        if (userMadeChangesToPosition)
        {
            
            var targetPosition = new Vector2((float)user.GetVariable("x").GetDoubleValue(), 
                                             (float)user.GetVariable("y").GetDoubleValue());
            networkingMovement.moveToPosition(targetPosition);
        }

        bool userMadeChangesToRotation = changedVars.Contains("rot");
        if (userMadeChangesToRotation)
        {
            networkingMovement.rotateToEuler((float)user.GetVariable("rot").GetDoubleValue());
        }
    }
}
