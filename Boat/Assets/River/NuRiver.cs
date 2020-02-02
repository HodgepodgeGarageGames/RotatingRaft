using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuRiver : MonoBehaviour
{
    [SerializeField] private float widthMax = 2.0f;
    [SerializeField] private float widthMin = 1.0f;
    [SerializeField] private float bendRange = 1.0f;
    [SerializeField] private float zetaRange = 0.1f;
    [SerializeField] private float segmentLength = 1.0f;
    [SerializeField] private int numberOfSegments = 20;
    [SerializeField] private int numberOfZetaSegments = 2;
    [SerializeField] private Material[] waterTexture = new Material[3];
    [SerializeField] private GameObject[] treesNcrap = new GameObject[0];

    private LineRenderer middle = null;
    private LineRenderer top = null;
    private LineRenderer bottom = null;

    private EdgeCollider2D topEdge = null;
    private EdgeCollider2D bottomEdge = null;

    private float widthMod = 0.0f;

    private MeshFilter waterFilter = null;
    private MeshRenderer waterRenderer = null;

    private List<GameObject> landscape = new List<GameObject>();

    private Vector3 vel = Vector3.zero;

    private Rigidbody2D rb2d = null;

    private Vector2 riverOldPos;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        widthMod = widthMax;

        riverOldPos = rb2d.position;

        //Line renderer
        GameObject middleGO = new GameObject("Middle Line");
        middleGO.transform.SetParent(transform);
        middleGO.transform.localPosition = Vector3.zero;
        middleGO.transform.localRotation = Quaternion.identity;
        middleGO.transform.localScale = Vector3.one;
        middle = middleGO.AddComponent<LineRenderer>() as LineRenderer;
        middleGO.SetActive(false);

        GameObject topGO = new GameObject("Top Line");
        topGO.transform.SetParent(transform);
        topGO.transform.localPosition = Vector3.zero;
        topGO.transform.localRotation = Quaternion.identity;
        topGO.transform.localScale = Vector3.one;
        top = topGO.AddComponent<LineRenderer>() as LineRenderer;
        topGO.GetComponent<LineRenderer>().enabled = false;

        GameObject bottomGO = new GameObject("Bottom Line");
        bottomGO.transform.SetParent(transform);
        bottomGO.transform.localPosition = Vector3.zero;
        bottomGO.transform.localRotation = Quaternion.identity;
        bottomGO.transform.localScale = Vector3.one;
        bottom = bottomGO.AddComponent<LineRenderer>() as LineRenderer;
        bottomGO.GetComponent<LineRenderer>().enabled = false;

        middle.startWidth = top.startWidth = bottom.startWidth = 0.1f;
        middle.endWidth = top.endWidth = bottom.endWidth = 0.1f;

        middle.useWorldSpace = top.useWorldSpace = bottom.useWorldSpace = false;

        middle.positionCount = top.positionCount = bottom.positionCount = numberOfSegments;

        for (int i = 0; i < numberOfSegments; ++i)
        {
            float y = 0.0f;

            middle.SetPosition(i, new Vector3(i * segmentLength, y, 0.0f));
        }

        copy(ref top, middle, 1.0f);
        copy(ref bottom, middle, -1.0f);

        //Edges
        topEdge = topGO.AddComponent<EdgeCollider2D>();
        bottomEdge = bottomGO.AddComponent<EdgeCollider2D>();

        copy(ref topEdge, top, false);
        copy(ref bottomEdge, bottom, false);

        //Stuff
        rb2d.MovePosition(Vector3.left * ((float)numberOfSegments) / 2.0f * segmentLength);
        //moveRiver(Vector3.left * ((float)numberOfSegments) / 2.0f * segmentLength);

        //Water Mesh
        waterFilter = gameObject.AddComponent<MeshFilter>();
        waterRenderer = gameObject.AddComponent<MeshRenderer>();
        waterRenderer.material = waterTexture[0];
        makeMesh(ref waterFilter, topEdge, bottomEdge);
    }

    public void moveRiver(Vector3 vec)
    {
        moveLandscape(rb2d.position - riverOldPos);
        riverOldPos = rb2d.position;

        rb2d.AddForce(vec);

        while (transform.position.x <= -((segmentLength * numberOfSegments) / 2.0f) - segmentLength)
        {
            //Line renderer
            transform.position += Vector3.right * segmentLength;

            for (int i = 0; i < numberOfSegments - 1; ++i)
            {
                middle.SetPosition(i, new Vector3(middle.GetPosition(i).x, middle.GetPosition(i + 1).y));
            }
            middle.SetPosition(numberOfSegments - 1, new Vector3(middle.GetPosition(numberOfSegments - 1).x, middle.GetPosition(numberOfSegments - 2).y + Random.Range(-bendRange, bendRange)));

            copy(ref top, middle, 1.0f);
            copy(ref bottom, middle, -1.0f);

            copy(ref topEdge, top, true);
            copy(ref bottomEdge, bottom, true);

            makeMesh(ref waterFilter, topEdge, bottomEdge);

            produceLandscape();
            killLandscape();
        }
    }

    private void copy(ref LineRenderer r, LineRenderer c, float dist)
    {
        /*for (int i = 0; i < numberOfSegments; ++i)
        {
            r.SetPosition(i, c.GetPosition(i) + vec);
        }*/

        for (int i = 0; i < numberOfSegments; ++i)
        {
            widthMod += Random.Range(-(widthMax - widthMin) * 0.1f, (widthMax - widthMin) * 0.1f);
            if (widthMod > widthMax) widthMod = widthMax;
            if (widthMod < widthMin) widthMod = widthMin;

            if (i == 0)
            {
                Vector3 cross = Vector3.Cross(Vector3.forward, c.GetPosition(i + 1) - c.GetPosition(i)).normalized * dist * widthMod;
                r.SetPosition(i, c.GetPosition(i) + cross);
            }
            else if (i == numberOfSegments - 1)
            {
                Vector3 cross = Vector3.Cross(Vector3.forward, c.GetPosition(i) - c.GetPosition(i - 1)).normalized * dist * widthMod;
                r.SetPosition(i, c.GetPosition(i) + cross);
            }
            else
            {
                Vector3 cross0 = Vector3.Cross(Vector3.forward, c.GetPosition(i) - c.GetPosition(i - 1)).normalized * dist * widthMod;
                Vector3 p00 = c.GetPosition(i - 1) + cross0;
                Vector3 p01 = c.GetPosition(i) + cross0;

                Vector3 cross1 = Vector3.Cross(Vector3.forward, c.GetPosition(i + 1) - c.GetPosition(i)).normalized * dist * widthMod;
                Vector3 p10 = c.GetPosition(i) + cross1;
                Vector3 p11 = c.GetPosition(i + 1) + cross1;

                Vector3 l0, l1;
                if (ClosestPointsOnTwoLines(out l0, out l1, p00, p01 - p00, p10, p11 - p10))
                    r.SetPosition(i, l0);
                else
                    r.SetPosition(i, c.GetPosition(i) + cross0);
            }
        }
    }

    //Two non-parallel lines which may or may not touch each other have a point on each line which are closest
    //to each other. This function finds those two points. If the lines are not parallel, the function 
    //outputs true, otherwise false.
    private bool ClosestPointsOnTwoLines(out Vector3 closestPointLine1, out Vector3 closestPointLine2, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {

        closestPointLine1 = Vector3.zero;
        closestPointLine2 = Vector3.zero;

        float a = Vector3.Dot(lineVec1, lineVec1);
        float b = Vector3.Dot(lineVec1, lineVec2);
        float e = Vector3.Dot(lineVec2, lineVec2);

        float d = a * e - b * b;

        //lines are not parallel
        if (d != 0.0f)
        {

            Vector3 r = linePoint1 - linePoint2;
            float c = Vector3.Dot(lineVec1, r);
            float f = Vector3.Dot(lineVec2, r);

            float s = (b * f - c * e) / d;
            float t = (a * f - c * b) / d;

            closestPointLine1 = linePoint1 + lineVec1 * s;
            closestPointLine2 = linePoint2 + lineVec2 * t;

            return true;
        }

        else
        {
            return false;
        }
    }

    private void copy(ref EdgeCollider2D e, LineRenderer lr, bool end)
    {
        int numOfEdgeSegments = ((numberOfSegments - 1) * numberOfZetaSegments) + 1;
        Vector2[] points = new Vector2[numOfEdgeSegments];

        if (end)
        {
            for (int i = 0; i < numberOfSegments - 2; ++i)
            {
                for (int j = 0; j < numberOfZetaSegments; ++j)
                {
                    points[(i * numberOfZetaSegments) + j] = e.points[((i + 1) * numberOfZetaSegments) + j];
                    points[(i * numberOfZetaSegments) + j].x -= segmentLength;
                }
            }

            points[(numberOfSegments - 2) * numberOfZetaSegments] = lr.GetPosition(numberOfSegments - 2);
            for (int j = 1; j < numberOfZetaSegments; ++j)
            {
                points[((numberOfSegments - 2) * numberOfZetaSegments) + j] = (((lr.GetPosition((numberOfSegments - 2) + 1) - lr.GetPosition(numberOfSegments - 2)) / ((float)numberOfZetaSegments)) * j) + lr.GetPosition(numberOfSegments - 2);
                Vector2 cross = Vector3.Cross((lr.GetPosition((numberOfSegments - 2) + 1) - lr.GetPosition(numberOfSegments - 2)).normalized, Vector3.forward);
                points[((numberOfSegments - 2) * numberOfZetaSegments) + j] += cross * Random.Range(-zetaRange, zetaRange);
            }
            points[numOfEdgeSegments - 1] = lr.GetPosition(numberOfSegments - 1);
        }
        else
        {
            for (int i = 0; i < numberOfSegments - 1; ++i)
            {
                points[i * numberOfZetaSegments] = lr.GetPosition(i);

                for (int j = 1; j < numberOfZetaSegments; ++j)
                {
                    points[(i * numberOfZetaSegments) + j] = (((lr.GetPosition(i + 1) - lr.GetPosition(i)) / ((float)numberOfZetaSegments)) * j) + lr.GetPosition(i);
                    Vector2 cross = Vector3.Cross((lr.GetPosition(i + 1) - lr.GetPosition(i)).normalized, Vector3.forward);
                    points[(i * numberOfZetaSegments) + j] += cross * Random.Range(-zetaRange, zetaRange);
                }
            }
            points[numOfEdgeSegments - 1] = lr.GetPosition(numberOfSegments - 1);
        }

        e.points = points;
    }

    private void makeMesh(ref MeshFilter segmentMesh, EdgeCollider2D eTop, EdgeCollider2D eBottom)
    {
        int numOfEdgeSegments = ((numberOfSegments - 1) * numberOfZetaSegments) + 1;

        Mesh mesh = new Mesh();

        Vector3[] Vertex = new Vector3[numOfEdgeSegments * 2];
        for (int i = 0; i < numOfEdgeSegments; ++i)
        {
            Vertex[i] = eTop.points[i];
            Vertex[i + numOfEdgeSegments] = eBottom.points[i];
            
        }

        Vector2[] UV_MaterialDisplay = new Vector2[numOfEdgeSegments * 2];
        for (int i = 0; i < numberOfSegments-1; ++i)
        {
            for (int j = 0; j < numberOfZetaSegments; ++j)
            {
                UV_MaterialDisplay[(i * numberOfZetaSegments) + j] = new Vector2(((float)j)/((float)numberOfZetaSegments), 1);
                UV_MaterialDisplay[(i * numberOfZetaSegments) + j + numOfEdgeSegments] = new Vector2(((float)j) / ((float)numberOfZetaSegments), 0);
            }
            
        }
        /*for (int i = 1; i < numberOfSegments - 1; i += 2)
        {
            for (int j = 0; j < numberOfZetaSegments; ++j)
            {
                UV_MaterialDisplay[(i * numberOfZetaSegments) + j] = new Vector2(1.0f - (((float)j) / ((float)numberOfZetaSegments)), 1);
                UV_MaterialDisplay[(i * numberOfZetaSegments) + j + numOfEdgeSegments] = new Vector2(1.0f - (((float)j) / ((float)numberOfZetaSegments)), 0);
            }
        }*/

            int[] Triangles = new int[((numOfEdgeSegments - 1) * 2) * 3];
        for (int i = 0; i < numOfEdgeSegments - 1; ++i)
        {
            Triangles[(i * 6) + 0] = i + 0;
            Triangles[(i * 6) + 1] = i + 1;
            Triangles[(i * 6) + 2] = i + 0 + numOfEdgeSegments;
            Triangles[(i * 6) + 3] = i + 1;
            Triangles[(i * 6) + 4] = i + 1 + numOfEdgeSegments;
            Triangles[(i * 6) + 5] = i + 0 + numOfEdgeSegments;
        }

        mesh.name = "Water Mesh";
        mesh.vertices = Vertex;
        mesh.triangles = Triangles;
        mesh.uv = UV_MaterialDisplay;

        mesh.RecalculateNormals();
        mesh.Optimize();
        waterFilter.mesh = mesh;
    }

    public Vector3 getCurrent(LineRenderer lr)
    {
        float closestPoint = ProjectPointOnLineSegment(lr.GetPosition(0), lr.GetPosition(1), Vector3.zero).magnitude;
        Vector3 current = (lr.GetPosition(1) - lr.GetPosition(0)).normalized;

        for (int i = 1; i < numberOfSegments; ++i)
        {
            float contender = ProjectPointOnLineSegment(lr.GetPosition(i+0), lr.GetPosition(i+1), Vector3.zero).magnitude;
            if (contender <= closestPoint)
            {
                closestPoint = contender;
                current = (lr.GetPosition(i+1) - lr.GetPosition(i+0)).normalized;
            }
        }

        return current;
    }

    private Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point)
    {

        //get vector from point on line to point in space
        Vector3 linePointToPoint = point - linePoint;

        float t = Vector3.Dot(linePointToPoint, lineVec);

        return linePoint + lineVec * t;
    }

    private Vector3 ProjectPointOnLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {
        Vector3 vector = linePoint2 - linePoint1;

        Vector3 projectedPoint = ProjectPointOnLine(linePoint1, vector.normalized, point);

        int side = PointOnWhichSideOfLineSegment(linePoint1, linePoint2, projectedPoint);

        //The projected point is on the line segment
        if (side == 0)
        {

            return projectedPoint;
        }

        if (side == 1)
        {

            return linePoint1;
        }

        if (side == 2)
        {

            return linePoint2;
        }

        //output is invalid
        return Vector3.zero;
    }

    private int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {

        Vector3 lineVec = linePoint2 - linePoint1;
        Vector3 pointVec = point - linePoint1;

        float dot = Vector3.Dot(pointVec, lineVec);

        //point is on side of linePoint2, compared to linePoint1
        if (dot > 0)
        {

            //point is on the line segment
            if (pointVec.magnitude <= lineVec.magnitude)
            {

                return 0;
            }

            //point is not on the line segment and it is on the side of linePoint2
            else
            {

                return 2;
            }
        }

        //Point is not on side of linePoint2, compared to linePoint1.
        //Point is not on the line segment and it is on the side of linePoint1.
        else
        {

            return 1;
        }
    }

    private void Update()
    {
        //Water animation
        float maxT = 1.0f;
        float t = Mathf.Repeat(Time.time, maxT);

        if (t < maxT / 3.0f)
            waterRenderer.material = waterTexture[0];
        else if (t < (2.0f * maxT) / 3.0f)
            waterRenderer.material = waterTexture[1];
        else
            waterRenderer.material = waterTexture[2];
    }

    private void FixedUpdate()
    {
        //Raft movement
        vel -= Vector3.right * Time.fixedDeltaTime * 0.1f;
        //vel *= 0.98f;

        moveRiver(vel);
    }

    private void moveLandscape(Vector3 vec)
    {
        foreach (GameObject g in landscape)
        {
            g.transform.position += vec;
        }
    }

    private void produceLandscape()
    {
        int numToCreate = Random.Range(5, 21);

        for (int i = 0; i < numToCreate; ++i)
        {
            int whichZeta = Random.Range(0, numberOfZetaSegments);

            if (Random.Range(0, 2) == 0)
            {
                landscape.Add(Instantiate(
                        treesNcrap[Random.Range(0, treesNcrap.Length)],
                        bottomEdge.points[((numberOfSegments - 2) * numberOfZetaSegments) + whichZeta] +
                        new Vector2(transform.position.x, transform.position.y) -
                        (((Vector2)(Vector3.Cross(Vector3.forward, middle.GetPosition(numberOfSegments - 1) - middle.GetPosition(numberOfSegments - 2)).normalized) * Random.Range(0.1f, 3.0f))),
                        Quaternion.identity,
                        null));

                Vector3 whereWeAt = landscape[landscape.Count - 1].transform.position;
                landscape[landscape.Count - 1].transform.position = new Vector3(whereWeAt.x, whereWeAt.y, whereWeAt.y);
            }
            else
            {
                landscape.Add(Instantiate(
                    treesNcrap[Random.Range(0, treesNcrap.Length)],
                    topEdge.points[((numberOfSegments - 2) * numberOfZetaSegments) + whichZeta] +
                    new Vector2(transform.position.x, transform.position.y) +
                    (((Vector2)(Vector3.Cross(Vector3.forward, middle.GetPosition(numberOfSegments - 1) - middle.GetPosition(numberOfSegments - 2)).normalized) * Random.Range(0.1f, 3.0f))),
                    Quaternion.identity,
                    null));

                Vector3 whereWeAt = landscape[landscape.Count - 1].transform.position;
                landscape[landscape.Count - 1].transform.position = new Vector3(whereWeAt.x, whereWeAt.y, whereWeAt.y);
            }
        }
    }

    private void killLandscape()
    {
        List<GameObject> killList = new List<GameObject>();
        foreach (GameObject g in landscape)
        {
            if (g.transform.position.x < -15.0f)
            {
                killList.Add(g);
            }
        }

        foreach (GameObject g in killList)
        {
            landscape.Remove(g);
            Destroy(g);
        }
    }
}
