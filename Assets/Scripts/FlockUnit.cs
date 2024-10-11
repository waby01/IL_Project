using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    [SerializeField] private float FOVAngle;
    [SerializeField] private float smoothDamp;

    private List<FlockUnit> cohesionNeighbours = new List<FlockUnit>();
	private List<FlockUnit> avoidanceNeighbours = new List<FlockUnit>();
	private List<FlockUnit> aligementNeighbours = new List<FlockUnit>();
    private Flock assignedFlock;
    private Vector3 currentVelocity;

    private float speed;

    public Transform myTransform { get; set; }

	private void Awake()
	{
		myTransform = transform;
	}

    

    public void AssignFlock(Flock flock)
	{
		assignedFlock = flock;
	}

    public void InitializeSpeed(float speed)
	{
		this.speed = speed;
	}

    public void MoveUnit()
	{
		FindNeighbours();
		CalculateSpeed();

		
		var cohesionVector = CalculateCohesionVector() * assignedFlock.cohesionWeight;
		var avoidanceVector = CalculateAvoidanceVector() * assignedFlock.avoidanceWeight;
		var aligementVector = CalculateAligementVector() * assignedFlock.aligementWeight;
        var moveVector = cohesionVector + avoidanceVector + aligementVector;
        moveVector = Vector3.SmoothDamp(myTransform.forward, moveVector, ref currentVelocity, smoothDamp);
		moveVector = moveVector.normalized * speed;
        myTransform.forward = moveVector;
		myTransform.position += moveVector * Time.deltaTime;
	}

    private void FindNeighbours()
	{
		cohesionNeighbours.Clear();
		avoidanceNeighbours.Clear();
		aligementNeighbours.Clear();
		var allUnits = assignedFlock.allUnits;
		for (int i = 0; i < allUnits.Length; i++)
		{
			var currentUnit = allUnits[i];
			if (currentUnit != this)
			{
				float currentNeighbourDistanceSqr = Vector3.SqrMagnitude(currentUnit.myTransform.position - myTransform.position);
				if(currentNeighbourDistanceSqr <= assignedFlock.cohesionDistance * assignedFlock.cohesionDistance)
				{
					cohesionNeighbours.Add(currentUnit);
				}
				if (currentNeighbourDistanceSqr <= assignedFlock.avoidanceDistance * assignedFlock.avoidanceDistance)
				{
					avoidanceNeighbours.Add(currentUnit);
				}
				if (currentNeighbourDistanceSqr <= assignedFlock.aligementDistance * assignedFlock.aligementDistance)
				{
					aligementNeighbours.Add(currentUnit);
				}
			}
		}
	}

    private void CalculateSpeed()
	{
		if (cohesionNeighbours.Count == 0)
			return;
		speed = 0;
		for (int i = 0; i < cohesionNeighbours.Count; i++)
		{
			speed += cohesionNeighbours[i].speed;
		}

		speed /= cohesionNeighbours.Count;
		speed = Mathf.Clamp(speed, assignedFlock.minSpeed, assignedFlock.maxSpeed);
	}

    private Vector3 CalculateCohesionVector()
	{
        var cohesionVector = Vector3.zero;
		if (cohesionNeighbours.Count == 0)
			return Vector3.zero;
		int neighboursInFOV = 0;
        for (int i = 0; i < cohesionNeighbours.Count; i++)
		{
			if (IsInFOV(cohesionNeighbours[i].myTransform.position))
			{
				neighboursInFOV++;
				cohesionVector += cohesionNeighbours[i].myTransform.position;
			}
		}
        cohesionVector /= neighboursInFOV;
		cohesionVector -= myTransform.position;
		cohesionVector = cohesionVector.normalized;
		return cohesionVector;
    }
     private Vector3 CalculateAvoidanceVector()
     {
        var avoidanceVector = Vector3.zero;
        if (aligementNeighbours.Count == 0)
			return Vector3.zero;
        int neighboursInFOV = 0;
        for (int i = 0; i < avoidanceNeighbours.Count; i++)
		{
			if (IsInFOV(avoidanceNeighbours[i].myTransform.position))
			{
				neighboursInFOV++;
				avoidanceVector += (myTransform.position - avoidanceNeighbours[i].myTransform.position);
			}
		}
        
        avoidanceVector /= neighboursInFOV;
		avoidanceVector = avoidanceVector.normalized;
        return avoidanceVector;
		

     }

      private Vector3 CalculateAligementVector()
     {
        var aligementVector = myTransform.forward;
        if (aligementNeighbours.Count == 0)
			return myTransform.forward;
        int neighboursInFOV = 0;
        for (int i = 0; i < aligementNeighbours.Count; i++)
		{
			if (IsInFOV(aligementNeighbours[i].myTransform.position))
			{
				neighboursInFOV++;
				aligementVector += aligementNeighbours[i].myTransform.forward;
			}
		}
       
        aligementVector /= neighboursInFOV;
		aligementVector = aligementVector.normalized;
        return aligementVector;
     }
    
    

    private bool IsInFOV(Vector3 position)
	{
		return Vector3.Angle(myTransform.forward, position - myTransform.position) <= FOVAngle;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
