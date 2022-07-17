using UnityEngine;

[ExecuteInEditMode]
public class CurveOffset : SingletonBehaviour<CurveOffset>
{
	[SerializeField] bool m_curveEnabled = true;
	public bool CurveEnabled { get { return m_curveEnabled; } set { m_curveEnabled = value; } }


	[SerializeField] Vector2 m_curveValues;
	public float BendX { get; private set; }
	public float BendZ { get; private set; }
	[SerializeField] float m_curveOffset;

	public Vector2 OrigValues { get; set; }

	void Start()
	{
		SetBendParams(m_curveValues.x, m_curveValues.y);

		if (Application.isPlaying)
			m_curveEnabled = true;
	}

	public void Update()
	{
		if (!Application.isPlaying)
		{
			SetBendParams(m_curveValues.x, m_curveValues.y);
		}

		if (m_curveEnabled)
		{
			Vector3 offset = transform.position + new Vector3(0f, 0f, m_curveOffset);
			offset.y = 0f;

			Shader.SetGlobalFloat("_CurvedX", BendX);
			Shader.SetGlobalFloat("_CurvedZ", BendZ);
			Shader.SetGlobalVector("_CurvedPivot", offset);
		}
		else
		{
			Shader.SetGlobalFloat("_CurvedX", 0f);
			Shader.SetGlobalFloat("_CurvedZ", 0f);
			Shader.SetGlobalVector("_CurvedPivot", Vector3.zero);
		}
	}

	public void SetBendParams(float x, float z)
	{
		BendX = x;
		BendZ = z;
	}

	void OnDrawGizmosSelected()
	{
		if (m_curveEnabled)
		{
			Gizmos.color = Color.red;

			Vector3 offset = transform.position + new Vector3(0f, 0f, m_curveOffset);
			offset.y = 0f;

			Gizmos.DrawSphere(offset, 1f);
		}
	}
}