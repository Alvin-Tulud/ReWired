using UnityEngine;

namespace GridObjects.Components.CompTypes
{
    public class CanWalk : MonoBehaviour
    {
        [SerializeField]
        private bool walkable;
        private bool isWall = false;

        
        public bool isWalkable()
        {
            return walkable;
        }

        public void setWalkable(bool walk)
        {
            if (!isWall)
            {
                walkable = walk;
            }
        }

        public void setWall(bool wall)
        {
            isWall = wall;
        }
    }
}