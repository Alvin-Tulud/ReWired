using UnityEngine;

namespace GridObjects.Components.CompTypes
{
    public class CanWalk : MonoBehaviour
    {
        [SerializeField]
        private bool walkable;

        
        public bool isWalkable()
        {
            return walkable;
        }

        public void setWalkable(bool walk)
        {
            walkable = walk;
        }
    }
}