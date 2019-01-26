using UnityEngine;
using UnityEngine.UI;

public class HeroButton : MonoBehaviour
{
    [SerializeField] private Image ButtonBG;
    [SerializeField] private Image RobotImage;
    public Sprite[] SPs;
    public Slider[] Sliders;
    public float[] Values;

    private void Initialize(Robots robot)
    {
        RobotImage.sprite = SPs[(int) robot];
    }

    private int _currentRobotIndex = 0;

    public int CurrentRobotIndex1
    {
        get { return _currentRobotIndex; }
        set
        {
            if (value != _currentRobotIndex)
            {
                if (value >= 4)
                {
                    value = 0;
                }

                Initialize((Robots) value);
                _currentRobotIndex = value;
            }
        }
    }
}