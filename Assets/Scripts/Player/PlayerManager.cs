using UnityEngine;

public enum CURSOR_STATE
{
    DEFAULT,
    HOVER,
    DRAG,
}
public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private Texture2D clickCursorTex;
    private bool IsInTransition;
    private PlayerController currentPlayer;

    public bool m_canControl => !IsInTransition;
    
    protected override void Awake()
    {
        base.Awake();

        EventHandler.E_AfterLoadScene += FindPlayer;
    }
    void Start() {
        FindPlayer();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventHandler.E_AfterLoadScene -= FindPlayer;
    }
    void TransitionBeginHandler() {
        IsInTransition = true;
        currentPlayer?.CheckControllable();
    }
    void TransitionEndHandler() {
        IsInTransition = false;
        currentPlayer?.CheckControllable();
    }
    void FindPlayer() {
        currentPlayer = FindAnyObjectByType<PlayerController>();
    }
    void FlashInputHandler() {
        currentPlayer?.ReleaseCurrentHolding();
    }
    void HideCursor() => Cursor.visible = false;
    void ShowCursor() => Cursor.visible = true;
    public Vector3 GetCursorWorldPos(float depth) => currentPlayer.GetCursorWorldPoint(depth);
    public void UpdateCursorState(CURSOR_STATE newState)
    {
        switch (newState)
        {
            case CURSOR_STATE.DEFAULT:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                ShowCursor();
                break;
            case CURSOR_STATE.HOVER:
                Cursor.SetCursor(clickCursorTex, new Vector2(26,11), CursorMode.Auto);
                ShowCursor();
                break;
            case CURSOR_STATE.DRAG:
                break;
        }
    }
}