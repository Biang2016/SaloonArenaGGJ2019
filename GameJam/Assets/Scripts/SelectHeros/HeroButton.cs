using UnityEngine;
using UnityEngine.UI;

public class HeroButton : PoolObject
{
    [SerializeField] private Image ButtonBG;
    [SerializeField] private Image RobotImage;
    [SerializeField] private Text PlayerName;
    public Sprite[] SPs;
    public Slider[] Sliders;
    public float[] Values;
    public Animator JumpAnim;
    public MonoBehaviour Ready;
    public Players Player;
    public Sprite NoPlayerSP;

    public PlayerState m_PlayerState;

    public PlayerState M_PlayerState
    {
        get { return m_PlayerState; }
        set
        {
            if (value == PlayerState.None)
            {
                ButtonBG.enabled = true;
                ButtonBG.sprite = NoPlayerSP;
                RobotImage.enabled = false;
                Ready.enabled = false;
                Player = Players.NoPlayer;
            }
            else
            {
                ButtonBG.enabled = false;
                RobotImage.enabled = true;
                Ready.enabled = false;
            }

            if (value == PlayerState.Ready)
            {
                Ready.enabled = true;
            }

            m_PlayerState = value;
        }
    }

    public void Initialize(Players player, Robots robot)
    {
        Player = player;
        PlayerName.text = player.ToString();
        if (player == Players.NoPlayer)
        {
            M_PlayerState = PlayerState.None;
        }
        else
        {
            M_PlayerState = PlayerState.Waiting;
            RobotImage.sprite = SPs[(int) robot];
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

                Initialize(Player, (Robots) value);
                JumpAnim.SetTrigger("Jump");
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