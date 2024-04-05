using UnityEngine;

namespace GridObjects.Components.CompTypes
{
    public class changeFloor : MonoBehaviour
    {
        void Start()
        {
            CanWalk canwalk = this.gameObject.transform.parent.transform.gameObject.GetComponent<CanWalk>();
            canwalk.setWalkable(false);
        }
    }
}