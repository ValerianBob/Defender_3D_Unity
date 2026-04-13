using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShockWave", menuName = "Skills/ShockWave")]
public class ShockWave : Skill
{
    public float DetectRange;
    public ShockWave(SkillConfig config) : base(config)
    {
    }

    public override void Execute(HeroController CurrentHeroController)
    {
        List<EnemyController> NearEnemies = EnemyController.AllEnemies;

        NearEnemies.Remove(CurrentHeroController.CurrentTarget.GetComponent<EnemyController>());

        foreach (var enemy in NearEnemies)
        {
            if (enemy == null)
            {
                continue;
            }
                
            if (Vector3.Distance(CurrentHeroController.transform.position, enemy.transform.position) > DetectRange)
            {
                continue;
            }

            enemy.ChangeHealth(true, CurrentHeroController.Hero_Attributes.CurrentDamage);
        }

        Debug.Log($"ShockWave hit");
    }
}
