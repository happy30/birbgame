interface IStatusEffect
{
    void UseTick(Player owner);
    int GetRemainingTicks();
    void OnAdd(Player owner);
    void OnRemove(Player owner);
}
