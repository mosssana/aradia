using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

/// <summary>
/// The Empress: 
/// Tras un breve periodo de tiempo, Diana canaliza y lanza un hechizo que drena la vida de cada enemigo cercano.
/// Este hechizo hace daño a los enemigos y cura a Diana. La cantidad de vida recuperada varía en función del daño de Diana.
/// Si Diana utiliza esta carta en Forma Arconte, recupera maná en su lugar.
/// </summary>
public class TheEmpress : TarotCard
{
    new void Awake()
    {
        cardName = CardName.TheEmpress;
        difficultyScore = 10;
        luckScore = 100;
        cooldown = 40;
        manaCost = 8;
        canMove = false;
        base.Awake();
    }

    #region Card Stats Variables

    public float skillDamage = 15;
    public float percentageOfLifeDrained = 250;

    #endregion

    LifeSystem lifeSystem;
    SearchRadius searchRadius;

    Object particles;

    void Start()
    {
        lifeSystem = gameObject.GetComponent<LifeSystem>();
        searchRadius = gameObject.GetComponentInChildren<SearchRadius>();
        particles = Resources.Load("Prefabs/TheEmpressParticleSystem");
    }

    public override void SkillPerform(InputAction.CallbackContext ctx)
    {
        base.SkillPerform(ctx);

        if (ctx.started) { if (Available) AudioManager.PlayUnique(AudioClipName.theEmpressCharge); }
        else if (ctx.canceled && !fired) AudioManager.StopUnique(AudioClipName.theEmpressCharge);
    }

    override public void SkillFire()
    {
        base.SkillFire();

        foreach (GameObject enemy in searchRadius.EnemiesList.ToList())
        {
            if (enemy != null)
            {
                Instantiate(particles, new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0), Quaternion.identity);
                PlayerDamageIndex.DealDamage(enemy, skillDamage);
            }
        }
    }
}
