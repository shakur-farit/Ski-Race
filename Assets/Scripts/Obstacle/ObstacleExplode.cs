using UnityEngine;

/// <summary>
/// Child of Obstacle class.
/// </summary>
public class ObstacleExplode : Obstacle
{
    protected override void HitToPlayer()
   {
        playerDamage.HitDamage = 2;

        base.HitToPlayer();

        Destroy(this.gameObject);

   }
}
