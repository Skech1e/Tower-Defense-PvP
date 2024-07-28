using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;
using touchip = UnityEngine.InputSystem.EnhancedTouch;

public class GameManager : MonoBehaviour
{
    public static GameManager gm { get; private set; }

    [SerializeField] BaseTower P1, P2;
    [SerializeField] Transform target;
    [SerializeField] List<Bot> spawnBotType = new();
    [SerializeField, Range(1f, 7f)] float timeForRouteSelect;
    [SerializeField] Material routeMat;

    [SerializeField] Button paladin, heavy;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else
            Destroy(gm);
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        paladin.onClick.AddListener(() => StartCoroutine(WaitforRouteSelect(0)));
    }
    private void OnDisable()
    {
        paladin.onClick.RemoveAllListeners();
        EnhancedTouchSupport.Disable();
    }

    public IEnumerator WaitforRouteSelect(int type)
    {
        touchip.Touch.onFingerDown += RouteSelection;
        float timer = timeForRouteSelect;
        yield return new WaitUntil(() =>
        {
            HighlightTowers();
            timer -= Time.deltaTime;
            return target != null || timer <= 0;
        });
        touchip.Touch.onFingerUp -= RouteSelection;
        routeMat.SetColor("_EmissionColor", Color.black);
        if (target != null)
        {
            P1.Spawn(spawnBotType[type], target);
            target = null;
        }
    }

    void RouteSelection(Finger finger)
    {
        var cam = Camera.main;
        Vector3 touchPosNear = new Vector3(finger.screenPosition.x, finger.screenPosition.y, cam.nearClipPlane);
        Vector3 touchPosFar = new Vector3(finger.screenPosition.x, finger.screenPosition.y, cam.farClipPlane);
        Vector3 touchPosN = cam.ScreenToWorldPoint(touchPosNear);
        Vector3 touchPosF = cam.ScreenToWorldPoint(touchPosFar);
        RaycastHit hit;
        if (Physics.Raycast(touchPosN, touchPosF, out hit))
        {
            Debug.LogAssertion(hit.transform.name);
            target = hit.transform;
        }

    }

    private void Update()
    {

    }

    void HighlightTowers()
    {
        Color color = Color.blue;
        float emission = Mathf.PingPong(Time.time, 1f);
        Color final = color * Mathf.LinearToGammaSpace(emission);
        routeMat.SetColor("_EmissionColor", final);
    }
}

public enum E_Player
{
    P1, P2
}

public enum Tags
{
    Player = 0,
    Enemy = 1,
    Top = 2,
    Mid = 3,
    Bottom = 4,
    Attack = 5,
    Paladin = 6,
    Border_Post = 7,
}

public enum AnimStates
{
    Idle,
    Move,
    Attack,
    Dead,
}
