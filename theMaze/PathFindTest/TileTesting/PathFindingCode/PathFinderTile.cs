using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileTesting.PathFindingCode
{
    public class PathFinderTile
    {
        private int width;
        private int height;
        private NodeTile[,] nodes;
        private NodeTile startNode;
        private NodeTile endNode;


        public Vector2 StartLocation { get; set; }

        public Vector2 EndLocation { get; set; }

        public Tile[,] Map { get; set; }


        public PathFinderTile(Vector2 startLocation, Vector2 endLocation, Tile[,] map)
        {
            this.StartLocation = startLocation;
            this.EndLocation = endLocation;
            this.Map = map;

            InitializeNodes(Map);
            this.startNode = this.nodes[(int)StartLocation.X / ConstantValues.TILE_WIDTH, (int)StartLocation.Y / ConstantValues.TILE_HEIGHT];
            this.startNode.State = NodeState.Open;
            this.endNode = this.nodes[(int)EndLocation.X / ConstantValues.TILE_WIDTH, (int)EndLocation.Y / ConstantValues.TILE_HEIGHT];
        }
        
        public List<Vector2> FindPath()
        {
            // The start node is the first entry in the 'open' list
            List<Vector2> path = new List<Vector2>();
            bool success = Search(startNode);
            if (success)
            {
                // If a path was found, follow the parents from the end node to build a list of locations
                NodeTile node = this.endNode;
                while (node.ParentNode != null)
                {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }

                // Reverse the list so it's in the correct order when returned
                path.Reverse();
            }

            return path;
        }

        private void InitializeNodes(Tile[,] map)
        {
            this.width = map.GetLength(0);
            this.height = map.GetLength(1);

            this.nodes = new NodeTile[this.width, this.height];

            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    this.nodes[x, y] = new NodeTile(x * ConstantValues.TILE_WIDTH, y * ConstantValues.TILE_HEIGHT, map[x, y].IsNotWall, this.EndLocation);
                }
            }
        }

        private bool Search(NodeTile currentNode)
        {
            // Set the current node to Closed since it cannot be traversed more than once
            currentNode.State = NodeState.Closed;
            List<NodeTile> nextNodes = GetAdjacentWalkableNodes(currentNode);

            // Sort by F-value so that the shortest possible routes are considered first
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach (var nextNode in nextNodes)
            {
                // Check whether the end node has been reached
                if (nextNode.Location == this.endNode.Location)
                {
                    return true;
                }
                else
                {
                    // If not, check the next set of nodes
                    if (Search(nextNode))  // Note: Recurses back into Search(Node)
                    {
                        return true;
                    }
                }
            }

            // The method returns false if this path leads to be a dead end
            return false;
        }

        private List<NodeTile> GetAdjacentWalkableNodes(NodeTile fromNode)
        {
            List<NodeTile> walkableNodes = new List<NodeTile>();
            IEnumerable<Vector2> nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (Vector2 location in nextLocations)
            {
                int x = (int)location.X;
                int y = (int)location.Y;

                // Stay within the grid's boundaries
                if (x < 0 || x >= this.width * ConstantValues.TILE_WIDTH || y < 0 || y >= this.height * ConstantValues.TILE_HEIGHT)
                {
                    continue;
                }

                NodeTile node = this.nodes[x / ConstantValues.TILE_WIDTH, y / ConstantValues.TILE_HEIGHT];

                // Ignore non-walkable nodes
                if (!node.IsWalkable)
                {
                    continue;
                }

                // Ignore already-closed nodes
                if (node.State == NodeState.Closed)
                {
                    continue;
                }

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == NodeState.Open)
                {
                    float traversalCost = NodeTile.GetTraversalCost(node.Location, node.ParentNode.Location);
                    float gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
                }
            }

            return walkableNodes;
        }

        private static IEnumerable<Vector2> GetAdjacentLocations(Vector2 fromLocation)
        {
            return new Vector2[]
            {
                new Vector2(fromLocation.X-ConstantValues.TILE_WIDTH, fromLocation.Y),
                new Vector2(fromLocation.X, fromLocation.Y+ConstantValues.TILE_HEIGHT),
                new Vector2(fromLocation.X+ConstantValues.TILE_WIDTH, fromLocation.Y),
                new Vector2(fromLocation.X, fromLocation.Y-ConstantValues.TILE_HEIGHT)
            };
        }
    }
}
