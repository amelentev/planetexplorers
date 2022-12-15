using PETools;
using System.Collections.Generic;
using UnityEngine;

public class HumanPhyCtrl : MonoBehaviour
{
	private const float MaxSpeed = 100f;

	public float mSpeedTimes = 1f;

	private float m_ImpactTime;

	public LayerMask m_GroundLayer;

	public LayerMask m_WaterLayer;

	public float m_CheckGroundRadius = 0.3f;

	public float m_CheckGroundOffsetHeight = 0.25f;

	public float m_HuamnHeight = 1.75f;

	public float m_SpineHeight = 1.3f;

	public float m_FeetHeight = 1f;

	public float m_SpineFixedHeight = 0.05f;

	public float m_StopSpeed = 0.1f;

	private float m_MoveSpeed = 5f;

	public float m_FlotageAcc = 12f;

	public float m_FlotageMaxSpeed = 1f;

	public float m_GroundDrag = 0.5f;

	public float m_WaterDrag = 0.16f;

	public float m_AirDrag = 0.006f;

	private float m_Drag;

	private float m_WaterToRoot;

	private float m_WaterHeight;

	private bool phyGround;

	[HideInInspector]
	public Vector3 m_WaterAcc;

	[HideInInspector]
	public Vector3 m_SubAcc;

	[HideInInspector]
	public bool m_IsContrler = true;

	private float MaxSubSpeed = 10f;

	private Vector3 m_RequestSubVelocity;

	private Vector3 m_ApplySubVelocity;

	public float m_LeaveGroundTime = 0.05f;

	private float m_LastGroundTime;

	private Rigidbody m_Rigidbody;

	public float m_MaxGroundBalanceAngle = 60f;

	private List<Vector3> m_GroundColNormals;

	private Vector3 m_GravityUpDir;

	private float m_ForwardGroundAngle;

	public float forwardAngleLerpSpeed = 3f;

	[HideInInspector]
	public float netMoveSpeedScale = 1f;

	public float maxSpeed = 50f;

	private int _cntFixedUpdate;

	public float moveSpeed
	{
		get
		{
			return m_MoveSpeed;
		}
	}

	public bool freezeUpdate
	{
		get;
		set;
	}

	public float gravity
	{
		get;
		set;
	}

	public bool useRopeGun
	{
		get;
		set;
	}

	public bool feetInWater
	{
		get;
		private set;
	}

	public bool spineInWater
	{
		get;
		private set;
	}

	public bool headInWater
	{
		get;
		private set;
	}

	public bool grounded
	{
		get;
		set;
	}

	public bool fallGround
	{
		get;
		private set;
	}

	public bool impacting
	{
		get
		{
			return Time.time - m_ImpactTime < m_LeaveGroundTime;
		}
	}

	public Vector3 velocity
	{
		get
		{
			return m_Rigidbody.velocity;
		}
		set
		{
			m_Rigidbody.AddForce(value - m_Rigidbody.velocity, ForceMode.VelocityChange);
		}
	}

	public Vector3 inertiaVelocity
	{
		get;
		private set;
	}

	public Vector3 desiredMovementDirection
	{
		get;
		set;
	}

	public Vector3 currentDesiredMovementDirection
	{
		get
		{
			return inertiaVelocity / m_MoveSpeed;
		}
	}

	private Vector3 desiredVelocity
	{
		get
		{
			if (desiredMovementDirection == Vector3.zero)
			{
				return Vector3.zero;
			}
			return desiredMovementDirection * m_MoveSpeed;
		}
	}

	public Rigidbody _rigidbody
	{
		get
		{
			return m_Rigidbody;
		}
	}

	public float forwardGroundAngle
	{
		get
		{
			return (!m_IsContrler) ? 0f : m_ForwardGroundAngle;
		}
		private set
		{
			m_ForwardGroundAngle = value;
		}
	}

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Rigidbody.freezeRotation = true;
		m_Rigidbody.useGravity = false;
		m_GroundColNormals = new List<Vector3>();
		m_SubAcc = Vector3.zero;
		Vector3 gravity = Physics.gravity;
		this.gravity = gravity.y;
		inertiaVelocity = Vector3.zero;
		forwardGroundAngle = 0f;
		m_GravityUpDir = Vector3.up;
	}

	private void FixedUpdate()
	{
		if (freezeUpdate)
		{
			return;
		}
		Vector3 force = Vector3.zero;
		Vector3 vector = desiredVelocity;
		vector += m_ApplySubVelocity;
		vector *= netMoveSpeedScale;
		if (_rigidbody.isKinematic)
		{
			if (!m_IsContrler)
			{
				_rigidbody.MovePosition(base.transform.position + vector * Time.fixedDeltaTime);
			}
			_rigidbody.velocity = Vector3.zero;
		}
		else
		{
			if (_cntFixedUpdate++ >= 2)
			{
				return;
			}
			if (!grounded && feetInWater && m_SubAcc.y <= 0f && vector.y > 0f && m_WaterToRoot > m_SpineHeight - m_SpineFixedHeight && m_WaterToRoot + float.Epsilon < m_HuamnHeight)
			{
				vector.y = 0f;
				Vector3 position = base.transform.position;
				position.y = m_WaterHeight - m_SpineHeight - m_SpineFixedHeight;
				Vector3 position2 = base.transform.position;
				if (position2.y - position.y < m_SpineHeight)
				{
					base.transform.position = position;
				}
			}
			if (vector != Vector3.zero)
			{
				force = ((!phyGround && !spineInWater && !useRopeGun) ? (Util.ProjectOntoPlane(vector, base.transform.up) - Util.ProjectOntoPlane(_rigidbody.velocity, base.transform.up)) : (vector - _rigidbody.velocity));
			}
			if (velocity.sqrMagnitude > 10000f)
			{
				force = -velocity;
			}
			_rigidbody.AddForce(force, ForceMode.VelocityChange);
			_rigidbody.AddForce(gravity * m_GravityUpDir, ForceMode.Acceleration);
			_rigidbody.AddForce(m_WaterAcc, ForceMode.Acceleration);
			_rigidbody.AddForce(m_SubAcc, ForceMode.Acceleration);
			if (vector == Vector3.zero)
			{
				_rigidbody.AddForce((0f - m_Drag) * _rigidbody.velocity.sqrMagnitude * _rigidbody.velocity.normalized, ForceMode.Acceleration);
			}
			if (desiredVelocity == Vector3.zero && m_SubAcc != Vector3.zero && velocity.sqrMagnitude < m_StopSpeed * m_StopSpeed)
			{
				velocity = Vector3.zero;
			}
			Vector3 position3 = base.transform.position;
			if (position3.y < -10000f)
			{
				Transform transform = base.transform;
				Vector3 position4 = base.transform.position;
				float x = position4.x;
				Vector3 position5 = base.transform.position;
				transform.position = new Vector3(x, -10f, position5.z);
				_rigidbody.AddForce(-velocity, ForceMode.VelocityChange);
			}
			if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
			{
				Vector3 normalized = velocity.normalized;
				float magnitude = velocity.magnitude;
				_rigidbody.AddForce(-normalized * (magnitude - maxSpeed), ForceMode.VelocityChange);
			}
		}
	}

	private void Update()
	{
		_cntFixedUpdate = 0;
		UpdateGroundState();
		UpdateWaterState();
		UpdateDrag();
	}

	private void LateUpdate()
	{
		inertiaVelocity = velocity;
		m_ApplySubVelocity = m_RequestSubVelocity;
		m_RequestSubVelocity = Vector3.zero;
	}

	private void OnCollisionEnter(Collision collision)
	{
		AddCollision(collision);
	}

	private void OnCollisionStay(Collision collision)
	{
		AddCollision(collision);
	}

	private void AddCollision(Collision collision)
	{
		for (int i = 0; i < collision.contacts.Length; i++)
		{
			if (((1 << collision.gameObject.layer) & m_GroundLayer.value) != 0)
			{
				m_GroundColNormals.Add(collision.contacts[i].normal);
			}
		}
	}

	private void UpdateGroundState()
	{
		bool grounded = this.grounded;
		if (!impacting)
		{
			phyGround = CheckGround();
			if (phyGround)
			{
				m_LastGroundTime = Time.time;
			}
			this.grounded = (Time.time - m_LastGroundTime < m_LeaveGroundTime);
		}
		else
		{
			bool flag2 = this.grounded = false;
			phyGround = flag2;
		}
		fallGround = (!grounded && this.grounded);
		if (!spineInWater && m_GroundColNormals.Count > 0)
		{
			Vector3 vector = Vector3.zero;
			m_GravityUpDir = Vector3.zero;
			if (!_rigidbody.isKinematic)
			{
				for (int i = 0; i < m_GroundColNormals.Count; i++)
				{
					Vector3 vector2 = m_GroundColNormals[i];
					float num = Vector3.Angle(vector2, Vector3.up);
					if (num < m_MaxGroundBalanceAngle)
					{
						m_GravityUpDir += vector2;
					}
					if (num < 90f)
					{
						vector += vector2;
					}
				}
			}
			m_GroundColNormals.Clear();
			if (Vector3.zero == vector)
			{
				vector = Vector3.up;
			}
			else
			{
				vector.Normalize();
				Vector3 vector3 = Vector3.ProjectOnPlane(base.transform.forward, Vector3.up);
				Vector3 from = Vector3.ProjectOnPlane(vector, Vector3.Cross(Vector3.up, vector3));
				forwardGroundAngle = Vector3.Angle(from, vector3) - 90f;
			}
			if (Vector3.zero == m_GravityUpDir)
			{
				m_GravityUpDir = Vector3.up;
			}
			else
			{
				m_GravityUpDir.Normalize();
			}
		}
		else
		{
			m_GravityUpDir = Vector3.up;
			forwardGroundAngle = Mathf.Lerp(forwardGroundAngle, 0f, forwardAngleLerpSpeed * Time.deltaTime);
			m_GroundColNormals.Clear();
		}
	}

	private void UpdateWaterState()
	{
		spineInWater = false;
		feetInWater = false;
		headInWater = false;
		if (PE.PointInWater(base.transform.position + ((!grounded) ? 0f : 0.5f) * Vector3.up) > 0.5f)
		{
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position + m_HuamnHeight * Vector3.up, Vector3.down, out hitInfo, 2f, m_WaterLayer.value))
			{
				Vector3 point = hitInfo.point;
				m_WaterHeight = point.y;
			}
			else
			{
				Vector3 position = base.transform.position;
				m_WaterHeight = position.y + m_HuamnHeight + 0.5f;
			}
			float waterHeight = m_WaterHeight;
			Vector3 position2 = base.transform.position;
			m_WaterToRoot = Mathf.Clamp(waterHeight - position2.y, 0f, m_HuamnHeight);
			if (m_WaterToRoot > m_FeetHeight)
			{
				feetInWater = true;
			}
			if (m_WaterToRoot > m_SpineHeight)
			{
				spineInWater = true;
			}
			if (m_WaterToRoot + float.Epsilon >= m_HuamnHeight)
			{
				headInWater = true;
			}
		}
		m_WaterAcc = ((!spineInWater) ? 0f : (0f - gravity)) * Vector3.up;
	}

	private void UpdateDrag()
	{
		if (grounded)
		{
			m_Drag = m_GroundDrag;
		}
		else if (spineInWater)
		{
			m_Drag = m_WaterDrag;
		}
		else
		{
			m_Drag = m_AirDrag;
		}
	}

	public void ResetSpeed(float speed)
	{
		if (speed < 0.1f)
		{
			speed = 0.1f;
		}
		m_MoveSpeed = speed;
		m_MoveSpeed *= mSpeedTimes;
	}

	public void ResetInertiaVelocity()
	{
		inertiaVelocity = Vector3.zero;
	}

	public void ApplyImpact(Vector3 speedImpact)
	{
		m_Rigidbody.AddForce(speedImpact, ForceMode.VelocityChange);
		m_ImpactTime = Time.time;
		bool flag2 = grounded = false;
		phyGround = flag2;
	}

	public void ApplyMoveRequest(Vector3 velocity)
	{
		m_RequestSubVelocity += velocity;
		if (m_RequestSubVelocity.magnitude > MaxSubSpeed)
		{
			m_RequestSubVelocity = MaxSubSpeed * m_RequestSubVelocity.normalized;
		}
	}

	public void CancelMoveRequest()
	{
		m_RequestSubVelocity = Vector3.zero;
		m_ApplySubVelocity = Vector3.zero;
	}

	public bool CheckGround()
	{
		if (m_IsContrler)
		{
			return m_GroundColNormals.Count > 0 || m_Rigidbody.isKinematic;
		}
		Collider[] array = Physics.OverlapSphere(base.transform.position + m_CheckGroundOffsetHeight * Vector3.up, m_CheckGroundRadius, m_GroundLayer.value);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].transform.IsChildOf(base.transform.parent))
			{
				return true;
			}
		}
		return false;
	}
}
