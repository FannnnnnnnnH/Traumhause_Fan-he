using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
	// Start is called before the first frame update
	
	public Material[] mats;
	int index;
	private Skybox sky;
	private void Start()
	{
		index = 0;
		sky = transform.GetComponent<Skybox>();
	}

	void Update()
	{
		//当运行游戏时，按下键盘的空格键就执行if语句的下列代码
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ChangeSkyBox();
		}
	}
	void ChangeSkyBox()
	{
		//下面的这一行代码 生效，必须 删除 主摄像机Main Camera，的Skybox组件
		RenderSettings.skybox = mats[index];


		//下面的这一行代码 生效，必须 使得 主摄像机Main Camera，的Skybox组件 生效
		//sky.material = mats[index];


		index++;
		index %= mats.Length;
	}

}
