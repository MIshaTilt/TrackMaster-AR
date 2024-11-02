using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMeshCustom : MonoBehaviour {

    [SerializeField] private BezierSpline spline;
    [SerializeField] private float meshWidth = 1.5f;

    private Mesh mesh;
    private MeshFilter meshFilter;

    [SerializeField] private Vector3 normal = new Vector3(0, 0, -1);

    private void Awake() {
        if (spline == null) spline = GetComponent<BezierSpline>();
        meshFilter = GetComponent<MeshFilter>();

        transform.position = Vector3.zero;
    }

    private void Start() {
        transform.position = spline.transform.position;

        UpdateMesh();

        spline.OnDirty += Spline_OnDirty;
    }

    private void Spline_OnDirty(object sender, EventArgs e) {
        UpdateMesh();
    }

    private void Update()
    {
        UpdateMesh();
    }

    private void UpdateMesh() {
        if (mesh != null) {
            mesh.Clear();
            Destroy(mesh);
            mesh = null;
        }

        List<Transform> pointList = spline.GetInterpolatedTransforms();
        if (pointList.Count > 2) {
            Transform point = pointList[0];
            Transform secondPoint = pointList[1];
            mesh = MeshUtils.CreateLineMesh(point.position - transform.position, secondPoint.position - transform.position, normal, meshWidth);

            for (int i = 2; i < pointList.Count; i++) {
                Transform thisPoint = pointList[i];
                MeshUtils.AddLinePoint(mesh, thisPoint.position - transform.position, thisPoint.forward, normal, meshWidth);
            }

            meshFilter.mesh = mesh;
        }
    }

    [Serializable]
    public class Point
    {
        public float t;
        public Vector3 position;
        public Vector3 forward;
        public Vector3 normal;
    }

}
