class PoisonEffect : IStatusEffect
{
    int damage = 1;
    int ticksAmount = 5;

    public void UseTick(Player p)
    {
        p.health -= damage;
        ticksAmount--;
    }

    public int GetRemainingTicks()
    {
        return ticksAmount;
    }

    public void OnAdd(Player p)
    { 
        // turn player color green 
    }
    public void OnRemove(Player p)
    { 
        // turn player color white 
    }
}
