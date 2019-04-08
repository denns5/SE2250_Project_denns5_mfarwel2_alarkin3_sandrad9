using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{//declaring all necessary variables
    private Transform _rocketTarget;
    public Rigidbody rocketRigidBody;
    public float turn;
    public float rocketVelocity;
    // Update is called once per frame
   private void FixedUpdate()
    {
        if (FindClosestByTag("Enemy") == null && FindClosestByTag("EnemyBoss") == null)//if there are no enemies on screen
        {
            rocketRigidBody.velocity = transform.forward * rocketVelocity;//set the velocity and direction to forward
        }
        else
        {
            if (FindClosestByTag("Enemy") == null)//if there is no target enemy
            {
                _rocketTarget = FindClosestByTag("EnemyBoss").transform;//find a new target with tag "EnemyBoss"
            }

            else if (FindClosestByTag("EnemyBoss") == null)//if there is no enemy boss
            {
                _rocketTarget = FindClosestByTag("Enemy").transform;//find a new target with tag "Enemy"
            }
            else if (Vector3.Distance(rocketRigidBody.position, FindClosestByTag("EnemyBoss").transform.position) < Vector3.Distance(rocketRigidBody.position, FindClosestByTag("Enemy").transform.position))
            {//if the boss is closer than the other enemies, target the boss
                _rocketTarget = FindClosestByTag("EnemyBoss").transform;
            }
            else//otherwise target the regular enemies
            {
                _rocketTarget = FindClosestByTag("Enemy").transform;
            }
            rocketRigidBody.velocity = transform.forward * rocketVelocity; //Set the rockets velocity
            var rocketTargetRotation = Quaternion.LookRotation(_rocketTarget.position - transform.position);//rotating the rocket toward the targeted enemy
            rocketRigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, turn));//having the rigit body also look toward the targeted enemy
        }
    }

    GameObject FindClosestByTag(string tag)
    {
        GameObject[] gameObj;
        gameObj = GameObject.FindGameObjectsWithTag(tag);//creating an array of objects with the tag
        GameObject closest = null;//currently there is no closest
        float distance = Mathf.Infinity;//setting this variable to infinite
        Vector3 position = transform.position;//position is the current position
        foreach (GameObject go in gameObj)//for every object in the array...
        {
            Vector3 diff = go.transform.position - position;//difference between rocket and enemy
            float curDistance = diff.sqrMagnitude;//getting the magnitude of the difference
            if (curDistance < distance)//if there is a closer enemy...
            {
                closest = go;//this is the new closest enemy
                distance = curDistance;//the new distance to compare to other enemies
            }
        }
        return closest;//return the closest enemy
    }

}

