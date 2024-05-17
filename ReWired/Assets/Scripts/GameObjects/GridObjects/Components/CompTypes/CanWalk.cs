using UnityEngine;

namespace GridObjects.Components.CompTypes
{
    public class CanWalk : MonoBehaviour
    {
        
        private bool walkable;
        private bool isWall = false;

        private void Awake()
        {
            if (gameObject.CompareTag("Floor"))
            {
                walkable = true;
            }
        }

        public bool isWalkable()
        {
            return walkable;
        }

        public void setWalkable(bool walk)
        {
            if (!isWall)
            {
                //Debug.Log(walk);
                walkable = walk;
            }
        }

        public void setWall(bool wall)
        {
            isWall = wall;
        }
    }
}