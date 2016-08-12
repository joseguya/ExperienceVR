using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Menu
{
	// This class 'pops' each of the menu items out
	// when the user looks at them.
	public class ObjectPopOut : MonoBehaviour
	{
		[SerializeField] private Transform m_Transform;         // Used to control the movement whatever needs to pop out.
		[SerializeField] private VRInteractiveItem m_Item;      // The VRInteractiveItem of whatever should pop out.
		[SerializeField] private float m_PopSpeed = 8f;         // The speed at which the item should pop out.
		[SerializeField] private float m_PopDistance = 0.5f;    // The distance the item should pop out.
		[SerializeField] private int popAxis = 1;
		[SerializeField] private GameObject targetObject;

		private Vector3 m_StartPosition;                        // The position aimed for when the item should not be popped out.
		private Vector3 m_PoppedPosition;                       // The position aimed for when the item should be popped out.
		private Vector3 m_TargetPosition;                       // The current position being aimed for.
		private Vector3 axisUnitaryVector;
		private Vector3 direction;

		private void Start ()
		{
			// Store the original position as the one that is not popped out.
			m_StartPosition = m_Transform.position;
			direction = m_StartPosition - targetObject.transform.position;
			direction = direction.normalized;
//			switch(popAxis){
//			case 1:
//				axisUnitaryVector = new Vector3 (1, 0, 0);
//				break;
//			case 2:
//				axisUnitaryVector = new Vector3 (0, 1, 0);
//				break;
//			case 3:
//				axisUnitaryVector = new Vector3 (0, 0, 1);
//				break;
//				default:
//					axisUnitaryVector = new Vector3 (1, 0, 0);
//					break;
//			}
			// Calculate the position the item should be when it's popped out.
			m_PoppedPosition = m_Transform.position - direction * m_PopDistance;

		}


		private void Update ()
		{
			// Set the target position based on whether the item is being looked at or not.
			m_TargetPosition = m_Item.IsOver ? m_PoppedPosition : m_StartPosition;

			// Move towards the target position.
			m_Transform.position = Vector3.MoveTowards(m_Transform.position, m_TargetPosition, m_PopSpeed * Time.deltaTime);
		}
	}
}