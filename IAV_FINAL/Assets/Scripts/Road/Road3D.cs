using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Road3D : MonoBehaviour {

    public int numSegments;
    public float distanceBetweenAnchors;
    public float smooth;
    public Transform terrain;
    public Transform car;
    RoadMesh road;
    float angleOffset;

    // Start is called before the first frame update
    void Start() {
        angleOffset = car.localEulerAngles.y * Mathf.Deg2Rad - PerlinOrientation(car.position);
        Vector3 pos = car.position;
        //pos.y = Terrain.activeTerrain.SampleHeight(pos)+3;
        pos.y = terrain.position.y + 3;
        //pos.y += 3;
        car.position = pos;
        road = GetComponent<RoadMesh>();
        BuildRoadAhead(car.position - car.forward * 4);
       
    }

    void BuildRoadAhead(Vector3 startPosition) {
        List<Vector3> anchors = new List<Vector3>();
        anchors.Add(startPosition);

        for (int i = 0; i < numSegments; i++) {
            Vector3 newAnchor = NextAnchor(anchors[i]);
            anchors.Add(newAnchor);
        }
        VertexPath vertexPath = GeneratePath(anchors, 0.2f, false, PathSpace.xyz);
        road.BuildRoad(vertexPath);
    }
    Vector3 NextAnchor(Vector3 anchor) {
        float angle = PerlinOrientation(anchor);
        angle += angleOffset;
        Vector2 dir = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
        Vector2 anchorxz = new Vector2(anchor.x, anchor.z);
        anchorxz += dir * distanceBetweenAnchors;
        //float h = Terrain.activeTerrain.SampleHeight(new Vector3(anchorxz[0], 0, anchorxz[1]));
        return new Vector3(anchorxz[0], terrain.position.y, anchorxz[1]);
    }

    float PerlinOrientation(Vector3 pos) {
        float offset = 24000f;
        return 2 * Mathf.PI * Mathf.PerlinNoise((pos.x + offset) * smooth, pos.z + (offset) * smooth);
    }

    VertexPath GeneratePath(List<Vector3> points, float vertexSpacing, bool closedPath, PathSpace space) {
        BezierPath bezierPath = new BezierPath(points, closedPath, space);
        return new VertexPath(bezierPath, this.transform, vertexSpacing);
    }


}
