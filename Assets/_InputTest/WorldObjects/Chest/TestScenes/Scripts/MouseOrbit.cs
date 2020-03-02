using UnityEngine;

namespace _InputTest.WorldObjects.Chest.TestScenes.Scripts
{
	public class MouseOrbit : MonoBehaviour {

		public GameObject target;
		public float distance = 5.0f;
		public float xSpeed = 120.0f;
		public float ySpeed = 120.0f;
		public float camVisible = 14f;
		public float yMinLimit = -20f;
		public float yMaxLimit = 80f;
		public float scrollSpeed = 0.1f;
		public float distanceMin = .5f;
		public float distanceMax = 15f;
		public float inertia = 5f;
		public float ScrollMouseSpeed = 1f;
 
		private bool firstClick = false;
		private bool camZ = false;
		private float AxisX = 0f;
		private float AxisY = 0f;
		private float inertiaVar = 0f;
		private Vector3 mp;
		private bool MouseD = false;
		private bool open = false;
		float x = 0.0f;
		float y = 0.0f;
	
 
		// Use this for initialization
		void Start () {
		
			//QualitySettings.antiAliasing = 8;
			Vector3 angles = transform.eulerAngles;
			x = angles.y;
			y = angles.x;
 
			// Make the rigid body not change rotation
			if (GetComponent<Rigidbody>())
				GetComponent<Rigidbody>().freezeRotation = true;
		}
	
		void LateUpdate () {
			if (target) {
				if(inertiaVar>0) inertiaVar--;
				if(Input.GetMouseButtonDown(0)) {
					firstClick = true;
					MouseD = true;
				}
				if(Input.GetMouseButtonUp(0)) {MouseD = false;
					AxisX =	Input.GetAxis("Mouse X");
					AxisY =	Input.GetAxis("Mouse Y");
				}
			
				if(firstClick){
					if(MouseD){	
						inertiaVar = inertia;
						x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f*inertiaVar;
						y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f*inertiaVar;
					}
					else{
						x += AxisX*xSpeed * distance * 0.02f*inertiaVar;
						y -= AxisY*ySpeed * 0.02f*inertiaVar;
					}
				}
				else{
					x += xSpeed * distance * 0.08f; //скорость вращения
				
				}
			
				y = ClampAngle(y, yMinLimit, yMaxLimit);
 
				Quaternion rotation = Quaternion.Euler(y, x, 0);
 
				distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*ScrollMouseSpeed, distanceMin, distanceMax);
				if(camZ){
					if(distance<=camVisible){
						distance = distance+scrollSpeed;
					}
					else camZ =  false;
		
				}
				/* RaycastHit hit;
        if (Physics.Linecast (target.transform.position, transform.position, out hit)) {
            distance -=  hit.distance;
        }*/
			
				Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
				Vector3 position = rotation * negDistance + target.transform.position;
 
				transform.rotation = rotation;
				transform.position = position;
			
				if(Input.GetMouseButtonDown(0)){
					mp = Input.mousePosition;
				}
			
			
			
				if(Input.GetMouseButtonUp(0)){
					if(!target.GetComponent<Animation>().isPlaying && mp.x == Input.mousePosition.x && mp.y == Input.mousePosition.y){
						if(distance<14)
							camZ = true;
						Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
						RaycastHit hit;
						if (Physics.Raycast(ray, out hit)){
							if(!open){ 
								target.GetComponent<Animation>()["ChestAnim"].speed = 1.0f;
								open = true;

						
							}
							else {
								target.GetComponent<Animation>()["ChestAnim"].time = target.GetComponent<Animation>()["ChestAnim"].length;	
								target.GetComponent<Animation>()["ChestAnim"].speed = -1.0f;
								open = false;
							}
							target.GetComponent<Animation>().Play("ChestAnim");

						}
					}		
				}
			
			}
 
		}
 
		public static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360F)
				angle += 360F;
			if (angle > 360F)
				angle -= 360F;
			return Mathf.Clamp(angle, min, max);
		}
 
 
	}
}