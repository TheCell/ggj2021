using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap uiInputActionMap;
    private InputActionMap playerInputActionMap;

    public void Start()
    {
        SetupInputAction();
    }

    public void NextScene(InputAction.CallbackContext context)
    {
        //Debug.Log("Next Scene called");
        var currentScene = SceneManager.GetActiveScene();
        var nextSceneIndex = 0;

        if (SceneManager.sceneCountInBuildSettings < 2)
        {
            //Debug.LogError("There are no other scenes to switch to. Add more scenes in the build settings");
            return;
        }
        
        // search the scene in the build. If it is the last we load the first one (index 0)
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
        {
            if (currentScene.buildIndex == i)
            {
                nextSceneIndex = + 1;
            }
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void SetupInputAction()
    {
        // I'm SURE this can be done MUCH better
        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
        {
            var actionMaps = playerInput.actions.actionMaps;
            foreach (var map in actionMaps)
            {
                //Debug.Log(map.name);

                if (map.name.Equals("UI"))
                {
                    uiInputActionMap = map;
                }
                else if (map.name.Equals("Player"))
                {
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
                    action.performed += NextScene;
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
                    action.performed += NextScene;
                }
            }
        }
    }

    private void OnDestroy()
    {
        //Debug.Log("on destroy");

        if (uiInputActionMap == null)
        {
            return;
        }

        var uiActions = uiInputActionMap.actions;
        foreach (var action in uiActions)
        {
            if (action.name.Equals("SwitchScene"))
            {
                action.performed -= NextScene;
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
                action.performed -= NextScene;
            }
        }
    }
}
