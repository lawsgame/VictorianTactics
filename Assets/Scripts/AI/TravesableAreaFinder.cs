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
        public int Children { get; set; }
        public Node Parent { get; set; }

        public Node(Node parent, Vector3Int gridPosInput, int cost)
        {
            this.Parent = parent;
            this.Pos = gridPosInput;
            this.Cost = cost;
            this.Children = 0;
        }

        public bool IsRoot() => Parent == null;

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
            if (o != null && o.GetType() == typeof(Node))
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

        public static List<Vector3Int> GetTravesableArea(Battle battlefield, Unit unit) => GetTravesableArea(battlefield, unit.GetMapPos(), unit.Model.Mobility(), unit.Model.Party());
        

        public static List<Vector3Int> GetTravesableArea(Battle battlefield, Vector3Int initPos, int range, int partyNumber)
        {
            
            if (!battlefield.Battlefield.HasTile(initPos))
                return new List<Vector3Int>();

            Dictionary<Vector3Int, Node> selectedNodes = new Dictionary<Vector3Int, Node>();
            Node rootNode = new Node(null, initPos, 0);
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


        private static void ExpandArea(Battle battle, Dictionary<Vector3Int, Node> selectedTiles, Node previousNode, Vector3Int currentPos, int rangeMax, int partyNumber)
        {
            // filters unworthy nodes

            Tilemap map = battle.Battlefield; 
            if (!map.HasTile(currentPos))
                return;

            WorldTile tile = map.GetTile<WorldTile>(currentPos);
            Node currentNode = new Node(previousNode, currentPos, previousNode.Cost + tile.MovementCost());
            if (!tile.Traversable || currentNode.Cost > rangeMax)
                return;

            if(battle.IsTileOccupiedByFoe(currentPos, partyNumber))
                return;
             
            if (selectedTiles.ContainsKey(currentNode.Pos))
            {
                Node oldNode = selectedTiles[currentNode.Pos];
                if (oldNode.Cost <= currentNode.Cost)
                    return;
                else
                    selectedTiles.Remove(currentNode.Pos);
            }

            // add the current node to the selected ones

            selectedTiles.Add(currentNode.Pos, currentNode);
            previousNode.Children += 1;
            
            Debug.Log("node chosen " + currentPos+ " for "+currentNode.Cost);

            // check neighbouring nodes and expand the area

            ExpandArea(battle, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(1,0,0), rangeMax, partyNumber);
            ExpandArea(battle, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(-1,0,0), rangeMax, partyNumber);
            ExpandArea(battle, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(0,1,0), rangeMax, partyNumber);
            ExpandArea(battle, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(0,-1,0), rangeMax, partyNumber);

            // if the furthest tile of one of paths is occupied by an ally, remove it 

            RemoveChildlessOccupiedNodes(battle, selectedTiles, currentNode);
        }

        private static void RemoveChildlessOccupiedNodes(Battle battle, Dictionary<Vector3Int, Node> selectedNodes, Node currentNode)
        {
            if (currentNode.Children == 0 && battle.IsTileOccupied(currentNode.Pos) && selectedNodes.ContainsKey(currentNode.Pos))
            {
                selectedNodes.Remove(currentNode.Pos);
                Debug.Log("Remove childless occupied tile : " + currentNode+" => is root? "+currentNode.IsRoot());
                if (!currentNode.IsRoot())
                {
                    currentNode.Parent.Children--;
                    RemoveChildlessOccupiedNodes(battle, selectedNodes, currentNode.Parent);
                }
            }
            
        }

    }

}
