using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{//setting all necessary variables
    [Header("Set in Inspector")]
    public Vector2 rotMinMax = new Vector2(15, 90);
    public Vector2 driftMinMax = new Vector2(0.25f, 2);
    public float liftime = 6f;
    public float fadeTime = 4f;

    [Header("Set Dynamically")]
    public string type;
    public GameObject cube;
    public TextMesh letter;
    public Vector3 rotPerSecond;
    public float birthTime;

    private Rigidbody _rigidBody;//creating private variables for components
    private BoundsCheck _bndCheck;
    private Renderer _cubeRend;

    private string[] _types = { "Multi", "Bomb" };//decalring power up types

    // Start is called before the first frame update
    void Awake()
    {
        //find cube reference
        cube = transform.Find("Cube").gameObject;
        //find other components
        letter = GetComponent<TextMesh>();
        _rigidBody = GetComponent<Rigidbody>();
        _bndCheck = GetComponent<BoundsCheck>();
        _cubeRend = GetComponent<Renderer>();

        //set random velocity
        Vector3 velocity = Random.onUnitSphere;
        velocity.z = 0;
        velocity.Normalize();//making the vecotr length 1
        velocity *= Random.Range(driftMinMax.x, driftMinMax.y);
        _rigidBody.velocity = velocity;

        transform.rotation = Quaternion.identity;

        //set the rotations per second of the cube
        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y), 
            Random.Range(rotMinMax.x, rotMinMax.y), 
            Random.Range(rotMinMax.x, rotMinMax.y));
        int ndx = Random.Range(0,1); 
        type = _types[ndx];
        birthTime = Time.time;
        Debug.Log(type);
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);

        //fade the power up over time
        float u = (Time.time - (birthTime + liftime)) / fadeTime;
        //destroy power up when the liftime has surpassed
        if (u >= 1)
        {
            Destroy(gameObject);
            return;
        }

        if (u > 0)
        {
            //fade the cube
            Color c = _cubeRend.material.color;
            c.a = 1f - u;
            _cubeRend.material.color = c;
            //fade the letter too but by less
            c = letter.color;
            c.a = 1f - (u * 0.5f);
            letter.color = c;
        }

        if (!_bndCheck.isOnScreen){
            //destroy power up is drifted off screen
            Destroy(gameObject);
        }
    }
    
    public string GetType()
    {
        return type;
    }

    public void Absorbedby(GameObject target)
    {
        //called by the hero class when an object is collected
       // Debug.Log("PowerUp: " + type);
        Destroy(gameObject);
    }
}
