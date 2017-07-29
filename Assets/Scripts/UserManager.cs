using UnityEngine;
using System.Collections;
using Sfs2X.Core;
using Sfs2X.Entities;
using System.Collections.Generic;

public interface UserManagerInput {
    void CreateLocalUser();
    void ProximityListUpdated(BaseEvent e);
    void UserVariablesUpdated(BaseEvent e);
}

public class UserManager : MonoBehaviour, UserManagerInput {

    List<SFSUser> users = new List<SFSUser>();
    public Transform playerPrefab;
    private Transform localPlayer;

    private void Start()
    {
        if(playerPrefab == null)
        {
            Debug.LogError("Please add Player prefab");
        }
    }

    public void CreateLocalUser()
    {
        localPlayer = GameObject.Instantiate(playerPrefab);
        localPlayer.GetComponent<MovementController>().enabled = true;
        localPlayer.GetComponent<RotationController>().enabled = true;
    }
    public void ProximityListUpdated(BaseEvent e)
    {
        var addedUsers = (List<SFSUser>)e.Params["addedUsers"];
        var removedUsers = (List<SFSUser>)e.Params["removedUsers"];

        // Handle all new Users
        foreach (User user in addedUsers)
        {

        }
    }
    public void UserVariablesUpdated(BaseEvent e)
    {
        print("User Variable Changed");
    }
}
