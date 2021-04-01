using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TraversableAreaFinder
{
    public class Node : IComparable
    {
        public Vector3Int Pos { get; } // position of the tile on the tilemap
        public int Cost { get; set;  }  // distance from initial position

        public Node(Vector3Int gridPosInput, int cost)
        {
            this.Pos = gridPosInput;
            this.Cost = cost;
        }

        public int CompareTo(object o)
        {
            int returnedValue = 0;
            if (o.GetType() == typeof(Node))
            {
                Node other = (Node)o;
                return (Cost > other.Cost) ? 1 : -1;
            }
            return returnedValue;
        }

        public bool BetterThan(Node n) => CompareTo(n) < 0;

        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object o)
        {
            if (o.GetType() == typeof(Node))
            {
                Node other = (Node)o;
                return other.Pos.x == Pos.x && other.Pos.y == Pos.y;
            }
            return false;
        }

        public override string ToString() => string.Format("Node of cost {0}  at pos {1} ", Cost, Pos);
    }

    public static class Algorithm
    {

        public static List<Vector3Int> GetTravesableArea(Battle battlefield, Unit unit)
        {
            return GetTravesableArea(battlefield, unit.GetMapPos(), unit.Model.Mobility(), unit.Model.Party());
        }

        public static List<Vector3Int> GetTravesableArea(Battle battlefield, Vector3Int initPos, int range, int partyNumber)
        {
            
            if (!battlefield.Groundmap.HasTile(initPos))
                return new List<Vector3Int>();

            Dictionary<Vector3Int, Node> selectedNodes = new Dictionary<Vector3Int, Node>();
            Node rootNode = new Node(initPos, 0);
            selectedNodes.Add(rootNode.Pos, rootNode);
            ExpandArea(battlefield, selectedNodes, rootNode, initPos + new Vector3Int(1, 0, 0), range, partyNumber);
            ExpandArea(battlefield, selectedNodes, rootNode, initPos + new Vector3Int(-1, 0, 0), range, partyNumber);
            ExpandArea(battlefield, selectedNodes, rootNode, initPos + new Vector3Int(0, 1, 0), range, partyNumber);
            ExpandArea(battlefield, selectedNodes, rootNode, initPos + new Vector3Int(0, -1, 0), range, partyNumber);

            List<Vector3Int> selectedPos = new List<Vector3Int>();
            foreach (Node node in selectedNodes.Values)
            {
                if(!node.Equals(rootNode))
                    selectedPos.Add(node.Pos);
            }
            
            return selectedPos;
        }


        private static void ExpandArea(Battle battlefield, Dictionary<Vector3Int, Node> selectedTiles, Node previousNode, Vector3Int currentPos, int rangeMax, int partyNumber)
        {
            //Debug.Log("node candidate: " + currentNode);

            Tilemap map = battlefield.Groundmap;
            if (!map.HasTile(currentPos))
                return;

            WorldTile tile = map.GetTile<WorldTile>(currentPos);
            Node currentNode = new Node(currentPos, previousNode.Cost + tile.Model.MovementCost());

            if (!tile.Model.Traversable || currentNode.Cost > rangeMax)
                return;

            if (selectedTiles.ContainsKey(currentNode.Pos))
            {
                Node oldNode = selectedTiles[currentNode.Pos];
                if (oldNode.Cost <= currentNode.Cost)
                    return;
                else
                    selectedTiles.Remove(currentNode.Pos);
            }

            // current node is a selected one
            selectedTiles.Add(currentNode.Pos, currentNode);
            
            Debug.Log("node chosen " + currentPos+ " for "+currentNode.Cost);

            ExpandArea(battlefield, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(1,0,0), rangeMax, partyNumber);
            ExpandArea(battlefield, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(-1,0,0), rangeMax, partyNumber);
            ExpandArea(battlefield, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(0,1,0), rangeMax, partyNumber);
            ExpandArea(battlefield, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(0,-1,0), rangeMax, partyNumber);
        }
    }

}
