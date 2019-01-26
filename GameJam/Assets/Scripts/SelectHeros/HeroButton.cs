using UnityEngine;
using UnityEngine.UI;

public class HeroButton : PoolObject
{
    [SerializeField] private Image ButtonBG;
    [SerializeField] private Image RobotImage;
    [SerializeField] private Image SwitchArrowImage;
    [SerializeField] private Image PlayerName;
    [SerializeField] private Image PressJoinTipImage;
    [SerializeField] private Image KeyTips;
    [SerializeField] private Text JoinKeyText;
    [SerializeField] private Text UpKeyText;
    [SerializeField] private Text LeftKeyText;
    [SerializeField] private Text DownKeyText;
    [SerializeField] private Text RightKeyText;
    [SerializeField] private Text ShootKeyText;
    public Sprite[] SPs;
    public Slider[] Sliders;
    public float[] Values;
    public Animator JumpAnim;
    public MonoBehaviour Ready;
    public Players Player;
    public Sprite NoPlayerSP;

    public Sprite[] PlayerTitleSprites;

    public PlayerState m_PlayerState;

    public PlayerState M_PlayerState
    {
        get { return m_PlayerState; }
        set
        {
            if (value == PlayerState.None)
            {
                Player = Players.NoPlayer;
                RobotImage.sprite = NoPlayerSP;

                PressJoinTipImage.enabled = true;
                JoinKeyText.enabled = true;

                KeyTips.enabled = false;
                UpKeyText.enabled = false;
                LeftKeyText.enabled = false;
                DownKeyText.enabled = false;
                RightKeyText.enabled = false;
                ShootKeyText.enabled = false;

                Ready.enabled = false;
                SwitchArrowImage.enabled = false;
                PlayerName.enabled = false;
            }
            else
            {
                RobotImage.enabled = true;

                PressJoinTipImage.enabled = false;
                JoinKeyText.enabled = false;

                KeyTips.enabled = true;
                UpKeyText.enabled = true;
                LeftKeyText.enabled = true;
                DownKeyText.enabled = true;
                RightKeyText.enabled = true;
                ShootKeyText.enabled = true;

                Ready.enabled = false;
                SwitchArrowImage.enabled = true;
                PlayerName.enabled = true;
            }

            if (value == PlayerState.Ready)
            {
                PressJoinTipImage.enabled = false;
                JoinKeyText.enabled = false;

                KeyTips.enabled = true;
                UpKeyText.enabled = true;
                LeftKeyText.enabled = true;
                DownKeyText.enabled = true;
                RightKeyText.enabled = true;
                ShootKeyText.enabled = true;

                Ready.enabled = true;
                SwitchArrowImage.enabled = false;
                PlayerName.enabled = true;
            }

            m_PlayerState = value;
        }
    }

    public void Initialize(Players player, Robots robot)
    {
        Player = player;
        if (player == Players.NoPlayer)
        {
            M_PlayerState = PlayerState.None;
        }
        else
        {
            _currentRobotIndex = (int) player;
            M_PlayerState = PlayerState.Waiting;
            RobotImage.sprite = SPs[(int) robot];
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