
[System.Serializable]
public class PlayerStats
{
    public float moveSpeed;
    public float maxJumpHeight;
    public double attackDamageMultiplier;
    public float modelWidth;
    public float modelHeigth;


    public PlayerStats(float moveSp, float maxJumpH, double dmgMultiplier)
    {
        moveSpeed = moveSp;
        maxJumpHeight = maxJumpH;
        attackDamageMultiplier = dmgMultiplier;
    }

	
}
