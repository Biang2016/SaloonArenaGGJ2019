public abstract class Skills
{
    public enum SkillType
    {
        TimeStop,
        BaseballBar,
        Pitch,
        SubRobot,
        RotateSelf,
        Rush,
    }

    public SkillType M_SkillType;
    public PlayerBody PlayerBody;

    public abstract void Skill_Execute();
}