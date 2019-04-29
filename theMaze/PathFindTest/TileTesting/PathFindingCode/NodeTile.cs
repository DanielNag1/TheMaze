using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileTesting.PathFindingCode
{
    public class NodeTile
    {
        private NodeTile parentNode;

        public Vector2 Location { get; private set; }
        public bool IsWalkable { get; set; }
        public NodeState State { get; set; }

        public float G { get; private set; }
        public float H { get; private set; }
        public float F
        {
            get { return this.G + this.H; }
        }

        public NodeTile ParentNode
        {
            get { return this.parentNode; }
            set
            {
                // When setting the parent, also calculate the traversal cost from the start node to here (the 'G' value)
                this.parentNode = value;
                this.G = this.parentNode.G + GetTraversalCost(this.Location, this.parentNode.Location);
            }
        }


        public NodeTile(int x, int y, bool isWalkable, Vector2 endLocation)
        {
            this.Location = new Vector2(x, y);
            this.State = NodeState.Untested;
            this.IsWalkable = isWalkable;
            this.H = GetTraversalCost(this.Location, endLocation);
            this.G = 0;
        }

        internal static float GetTraversalCost(Vector2 location, Vector2 otherLocation)
        {
            float deltaX = Math.Abs(location.X - otherLocation.X);
            float deltaY = Math.Abs(location.Y - otherLocation.Y);
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
