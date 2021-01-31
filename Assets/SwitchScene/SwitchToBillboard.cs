using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwitchToBillboard : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap uiInputActionMap;
    private InputActionMap playerInputActionMap;

    private int cameFromSceneIndex = 0;
    private int currentSceneIndex = 0;
    private bool inputActionsSet = false;

    public void Start()
    {
        DontDestroyOnLoad(this);

        SceneManager.activeSceneChanged += OnSceneChanged;

        SetupInputAction();
    }

    public void SwitchToAndFromBillboard(InputAction.CallbackContext context)
    {
        ////Debug.Log("Next Scene called");
        //var currentScene = SceneManager.GetActiveScene();
        //var nextSceneIndex = 0;

        //if (SceneManager.sceneCountInBuildSettings < 2)
        //{
        //    //Debug.LogError("There are no other scenes to switch to. Add more scenes in the build settings");
        //    return;
        //}

        //// search the scene in the build. If it is the last we load the first one (index 0)
        //for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
        //{
        //    if (currentScene.buildIndex == i)
        //    {
        //        nextSceneIndex = + 1;
        //    }
        //}

        Debug.Log("SwitchToAndFromBillboard");
        //RemoveInputAction();
        SceneManager.LoadScene(cameFromSceneIndex);
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        //Debug.Log(current.name);
        //Debug.Log(current.buildIndex);
        //Debug.Log(next.name);
        //Debug.Log(next.buildIndex);
        cameFromSceneIndex = currentSceneIndex;
        currentSceneIndex = next.buildIndex;
        //cameFromSceneIndex = current.buildIndex;
        //currentSceneIndex = newScene.buildIndex;
        SetupInputAction();
    }

    private void SetupInputAction()
    {
        //if (inputActionsSet)
        //{
        //    return;
        //}

        //inputActionsSet = true;
        // I'm SURE this can be done MUCH better
        Debug.Log("SetupInputAction");
        playerInput = FindObjectOfType<PlayerInput>();
        Debug.Log(playerInput.gameObject.name);
        if (playerInput != null)
        {
            var actionMaps = playerInput.actions.actionMaps;
            foreach (var map in actionMaps)
            {
                //Debug.Log(map.name);

                if (map.name.Equals("UI"))
                {
                    Debug.Log("adding UI mapping");
                    uiInputActionMap = map;
                }
                else if (map.name.Equals("Player"))
                {
                    Debug.Log("adding player mapping");
                    playerInputActionMap = map;
                }
            }
        }

        if (uiInputActionMap != null)
        {
            var actions = uiInputActionMap.actions;
            foreach (var action in actions)
            {
                if (action.name.Equals("SwitchScene"))
                {
                    //Debug.Log("SwitchScene add NextScene");
                    action.performed += SwitchToAndFromBillboard;
                }
            }
        }
        if (playerInputActionMap != null)
        {
            var actions = uiInputActionMap.actions;
            foreach (var action in actions)
            {
                if (action.name.Equals("SwitchScene"))
                {
                    //Debug.Log("SwitchScene add NextScene");
                    action.performed += SwitchToAndFromBillboard;
                }
            }
        }
    }

    private void RemoveInputAction()
    {
        inputActionsSet = false;

        if (uiInputActionMap == null)
        {
            return;
        }

        Debug.Log("removing action");
        var uiActions = uiInputActionMap.actions;
        foreach (var action in uiActions)
        {
            if (action.name.Equals("SwitchScene"))
            {
                action.performed -= SwitchToAndFromBillboard;
            }
        }

        if (playerInputActionMap == null)
        {
            return;
        }

        var playerActions = playerInputActionMap.actions;
        foreach (var action in playerActions)
        {
            if (action.name.Equals("SwitchScene"))
            {
                action.performed -= SwitchToAndFromBillboard;
            }
        }
    }

    private void OnDestroy()
    {
        RemoveInputAction();
    }
}
