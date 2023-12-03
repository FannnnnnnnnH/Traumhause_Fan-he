using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Licht : MonoBehaviour
{
	// Start is called before the first frame update
	private Transform m_transform;

	// Use this for initialization
	void Start()
	{
		m_transform = GameObject.Find("Directional Light").GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		//旋转
		m_transform.Rotate(Vector3.down * 5 * Time.deltaTime);
	}

}
