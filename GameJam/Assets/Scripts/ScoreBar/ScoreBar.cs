using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : PoolObject
{
    [SerializeField] private Image Scorebar;
    [SerializeField] private Image PlayerIcon;
    [SerializeField] private Image RankIcon;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Transform HigherPivot;
    [SerializeField] private Transform LowerPivot;
    [SerializeField] private Sprite[] ScorebarSprites;
    [SerializeField] private Sprite[] RankSprites;
    [SerializeField] private Sprite[] PlayerIconSprites;

    private static float barHeight;
    private static float barWidth;
    private float RankIconTargetPosY;

    void Awake()
    {
        barHeight = Scorebar.rectTransform.rect.height;
        barWidth = Scorebar.rectTransform.rect.width;
    }

    public void Initialize(Players player, int Score, int highestScore, int Rank)
    {
        Scorebar.sprite = ScorebarSprites[(int) player];
        PlayerIcon.sprite = PlayerIconSprites[(int) player];
        RankIcon.sprite = RankSprites[Rank];

        ScoreText.text = Score.ToString();
        RankIconTargetPosY = (HigherPivot.localPosition.y - LowerPivot.localPosition.y) * (float) Score / highestScore + LowerPivot.localPosition.y;
        RankIcon.transform.localPosition = LowerPivot.localPosition;

        Scorebar.rectTransform.sizeDelta = new Vector2(barWidth, barHeight * (float) Score / highestScore);
        StartBarGrowUpAnimation();
    }

    void StartBarGrowUpAnimation()
    {
        Scorebar.fillAmount = 0;
        StartCoroutine(Co_BarGrowUp());
    }

    IEnumerator Co_BarGrowUp()
    {
        for (int i = 0; i < 60; i++)
        {
            SetBarHeight((float) i / 60);
            yield return null;
        }
    }

    void SetBarHeight(float ratio)
    {
        Scorebar.fillAmount = ratio;
        RankIcon.transform.localPosition = new Vector3(RankIcon.transform.localPosition.x, (RankIconTargetPosY - LowerPivot.localPosition.y) * ratio + LowerPivot.localPosition.y, RankIcon.transform.localPosition.z);
    }
}