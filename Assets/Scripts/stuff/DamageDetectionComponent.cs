using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageDetectionComponent : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private EnemyController enemyController;
    public enum CollTypes { Player, Enemy, PlayerBox}

    public CollTypes myCollType;

    [SerializeField]
    private List<CollTypes> canBeDamaged;

    void OnTriggerEnter(Collider _coll)
    {
        DamageDetectionComponent collDamage = _coll.GetComponent<DamageDetectionComponent>();

        if (collDamage != null)
        {
            CheckForDamage(collDamage, _coll.gameObject);
        }
    }

    void OnCollisionEnter(Collision _coll)
    {
        DamageDetectionComponent collDamage = _coll.gameObject.GetComponentInChildren<DamageDetectionComponent>();

        if (collDamage != null)
        {
            CheckForDamage(collDamage, _coll.gameObject);
        }
    }

    void CheckForDamage(DamageDetectionComponent _collDamage, GameObject _collGameObject)
    {

        foreach (CollTypes myDamageType in canBeDamaged)
        {
            if (myDamageType == _collDamage.myCollType )
            {


                if(playerController != null)
                {
                    playerController.GetDamaged();
                }
                else if (enemyController != null) 
                {
                    enemyController.GetDamaged();
                }
                
                break;
            }
        }
    }

}
