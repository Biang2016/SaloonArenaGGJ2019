using UnityEngine;
using UnityEngine.UI;

public class PlayerTutorialConfirm : PoolObject
{
    [SerializeField] private Image ButtonBG;
    [SerializeField] private Image PlayerName;
    public Sprite[] PlayerTitleSprites;
    public MonoBehaviour Ready;
    public Players Player;

    public PlayerState m_PlayerState;

    public PlayerState M_PlayerState
    {
        get { return m_PlayerState; }
        set
        {
            if (value == PlayerState.None)
            {
                Player = Players.NoPlayer;
                ButtonBG.enabled = false;
                PlayerName.enabled = false;
                Ready.enabled = false;
            }
            else
            {
                PlayerName.enabled = true;
                ButtonBG.enabled = true;
                Ready.enabled = false;
            }

            if (value == PlayerState.Ready)
            {
                PlayerName.enabled = true;
                ButtonBG.enabled = true;
                Ready.enabled = true;
            }

            m_PlayerState = value;
        }
    }

    public void Initialize(Players player)
    {
        Player = player;
        if (player == Players.NoPlayer)
        {
            M_PlayerState = PlayerState.None;
        }
        else
        {
            M_PlayerState = PlayerState.Waiting;
            PlayerName.sprite = PlayerTitleSprites[(int) player];
        }
    }

    private int _currentRobotIndex = 0;

    public int CurrentRobotIndex
    {
        get { return _currentRobotIndex; }
        set
        {
            if (value != _currentRobotIndex)
            {
                if (value > 3)
                {
                    value = 0;
                }

                if (value <= -1)
                {
                    value = 3;
                }

                Initialize(Player);
                _currentRobotIndex = value;
            }
        }
    }

    public enum PlayerState
    {
        None,
        Waiting,
        Ready,
    }
}