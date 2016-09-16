using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class FlagRenderer : MonoBehaviour
{
    [SerializeField] 
    private MeshFilter _meshFilter;

    [SerializeField] 
    private Transform _bottomLeftPoint;

    [SerializeField, Range(0,12)]
    private float _width;

    [SerializeField,Range(0,8)]
    private float _height;

    [SerializeField] private ColorController _colorController;

    [SerializeField] private Toggle _isVerticaleToggle;

    private Color[] _colors;

    public void DrowFlag(bool isVertical)
    {
        if (!_bottomLeftPoint || !_meshFilter) return;

        Mesh mesh = new Mesh();

        _colors = _colorController.GetColors();

        if (isVertical) SetVerticalVertices(mesh);
        else SetHorizontalVertices(mesh);

        SetTriangles(mesh);

        SetColors(_colors, mesh);

        if (_meshFilter) _meshFilter.mesh = mesh;
    }

    private void SetVerticalVertices(Mesh mesh)
    {
        Vector3[] vertices = new Vector3[_colors.Length*4];

        float stepWidth = _width/_colors.Length;

        Vector3 rightVector3 = Vector3.right * stepWidth;
        Vector3 upVector3 = Vector3.up * _height;

        vertices[0] = _bottomLeftPoint.position;
        vertices[1] = vertices[0] + upVector3;
        vertices[2] = vertices[0] + rightVector3;
        vertices[3] = vertices[1] + rightVector3;

        for (int i = 4; i < _colors.Length*4; i+=4)
        {
            vertices[i] = vertices[i - 2];
            vertices[i + 1] = vertices[i - 1];
            vertices[i + 2] = vertices[i] + rightVector3;
            vertices[i + 3] = vertices[i + 1] + rightVector3;
        }

        mesh.vertices = vertices;
    }

    private void SetHorizontalVertices(Mesh mesh)
    {
        Vector3[] vertices = new Vector3[_colors.Length * 4];

        float stepHeight = _height / _colors.Length;

        Vector3 rightVector3 = Vector3.right * _width;
        Vector3 upVector3 = Vector3.up * stepHeight;

        vertices[0] = _bottomLeftPoint.position;
        vertices[1] = vertices[0] + rightVector3;
        vertices[2] = vertices[0] + upVector3;
        vertices[3] = vertices[1] + upVector3;

        for (int i = 4; i < _colors.Length * 4; i += 4)
        {
            vertices[i] = vertices[i - 2];
            vertices[i + 1] = vertices[i - 1];
            vertices[i + 2] = vertices[i] + upVector3;
            vertices[i + 3] = vertices[i + 1] + upVector3;
        }

        mesh.vertices = vertices;
    }

    private void SetTriangles(Mesh mesh)
    {
        int[] triangles = new int[_colors.Length * 6];

        int counter = 0;
        int num = 0;
        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] = num;
            counter++;
            num++;
            if (counter == 3) num -= 2;
            if (counter == 6) counter = 0;

        }

        mesh.triangles = triangles;
    }

    private void SetColors(Color[] colors, Mesh mesh)
    {
        int index = 0;
        Color[] colorsToMesh = new Color[mesh.vertexCount];
        for (int i = 0; i < colors.Length; i++)
        {
            for (int j = index; j < index + mesh.vertexCount/colors.Length; j++)
            {
                colorsToMesh[j] = colors[i];
            }
            index += mesh.vertexCount/colors.Length;
        }

        mesh.colors = colorsToMesh;
    }

    public void OnDrawClick()
    {
        DrowFlag(_isVerticaleToggle.isOn);
    }
}