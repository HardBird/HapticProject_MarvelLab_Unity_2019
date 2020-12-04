using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent( typeof(LineRenderer) )]
public class CurvedLineRenderer : MonoBehaviour 
{
	//PUBLIC
	public float lineSegmentSize = 0.15f;
	public float lineWidth = 0.01f;
	[Header("Gizmos")]
	public bool showGizmos = true;
	public float gizmoSize = 0.1f;
	public Color gizmoColor = new Color(1,0,0,0.5f);
	//PRIVATE
	public CurvedLinePoint[] linePoints = new CurvedLinePoint[0];
	public Vector3[] linePositions = new Vector3[0];
	private Vector3[] linePositionsOld = new Vector3[0];

    public EdgeCollider2D edgeColl;
    public List<Vector2> newVector;

    // Update is called once per frame

    void Start()
    {
        edgeColl.Reset();
    }
    public void Update () 
	{
		GetPoints();
		SetPointsToLine();

        int thisGameObjectCount = this.gameObject.transform.childCount;

        edgeColl.points = new Vector2[thisGameObjectCount];
        newVector.Clear();
        for (int i = 0; i < thisGameObjectCount; i++)
        {
            Vector2 tmp = new Vector2(this.gameObject.transform.GetChild(i).transform.localPosition.x, this.gameObject.transform.GetChild(i).transform.localPosition.y);
            newVector.Add(tmp);
            edgeColl.points = newVector.ToArray();
        }
        
    }

	void GetPoints()
	{
		//find curved points in children
		linePoints = this.GetComponentsInChildren<CurvedLinePoint>();

		//add positions
		linePositions = new Vector3[linePoints.Length];
		for( int i = 0; i < linePoints.Length; i++ )
		{
			linePositions[i] = linePoints[i].transform.position;    
		}
	}

	void SetPointsToLine()
	{
		//create old positions if they dont match
		if( linePositionsOld.Length != linePositions.Length )
		{
			linePositionsOld = new Vector3[linePositions.Length];
		}

		//check if line points have moved
		bool moved = false;
		for( int i = 0; i < linePositions.Length; i++ )
		{
			//compare
			if( linePositions[i] != linePositionsOld[i] )
			{
				moved = true;
			}
		}

		//update if moved
		if( moved == true )
		{
			LineRenderer line = this.GetComponent<LineRenderer>();

			//get smoothed values
			Vector3[] smoothedPoints = LineSmoother.SmoothLine( linePositions, lineSegmentSize );

			//set line settings
			line.SetVertexCount( smoothedPoints.Length );
			line.SetPositions( smoothedPoints );
			line.SetWidth( lineWidth, lineWidth );
		}


        
        
	}

	void OnDrawGizmosSelected()
	{
		Update();
	}

	void OnDrawGizmos()
	{
		if( linePoints.Length == 0 )
		{
			GetPoints();
		}

		//settings for gizmos
		foreach( CurvedLinePoint linePoint in linePoints )
		{
			linePoint.showGizmo = showGizmos;
			linePoint.gizmoSize = gizmoSize;
			linePoint.gizmoColor = gizmoColor;
		}
	}

    
}
