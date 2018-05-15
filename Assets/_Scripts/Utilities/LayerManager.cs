using UnityEngine;
using Unity.Mathematics;


public static class LayerManager
{
    public static int Platform = LayerMask.NameToLayer("Platform");
    public static int Ground = LayerMask.NameToLayer("Ground");
    public static int Player = LayerMask.NameToLayer("Player");


    public static LayerMask PlayerCollsionMask()
    {
        return Physics2D.GetLayerCollisionMask(Player);
    }

    public static LayerMask PlayerCollsionMaskIgnorePlatform()
    {
        return PlayerCollsionMask() - (int)math.pow(2, Platform);
    }
}
