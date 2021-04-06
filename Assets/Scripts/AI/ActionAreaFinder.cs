using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ActionAreaFinder
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

        public static List<Vector3Int> FindMoveArea(Battlefield battlefield, Unit unit) => FindMoveArea(battlefield, unit.GetMapPos(), unit.Model.Mobility(), unit.Model.Party());
        

        public static List<Vector3Int> FindMoveArea(Battlefield battlefield, Vector3Int initPos, int range, int partyNumber)
        {
            
            if (!battlefield.Map.HasTile(initPos))
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


        private static void ExpandArea(Battlefield battlefield, Dictionary<Vector3Int, Node> selectedTiles, Node previousNode, Vector3Int currentPos, int rangeMax, int partyNumber)
        {
            // filters unworthy nodes

            Tilemap map = battlefield.Map; 
            if (!map.HasTile(currentPos))
                return;

            WorldTile tile = map.GetTile<WorldTile>(currentPos);
            Node currentNode = new Node(previousNode, currentPos, previousNode.Cost + tile.MovementCost());
            if (!tile.Traversable || currentNode.Cost > rangeMax)
                return;

            if(battlefield.IsTileOccupiedByFoe(currentPos, partyNumber))
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

            ExpandArea(battlefield, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(1,0,0), rangeMax, partyNumber);
            ExpandArea(battlefield, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(-1,0,0), rangeMax, partyNumber);
            ExpandArea(battlefield, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(0,1,0), rangeMax, partyNumber);
            ExpandArea(battlefield, selectedTiles, currentNode, currentNode.Pos + new Vector3Int(0,-1,0), rangeMax, partyNumber);

            // if the furthest tile of one of paths is occupied by an ally, remove it 

            RemoveChildlessOccupiedNodes(battlefield, selectedTiles, currentNode);
        }

        private static void RemoveChildlessOccupiedNodes(Battlefield battlefield, Dictionary<Vector3Int, Node> selectedNodes, Node currentNode)
        {
            if (currentNode.Children == 0 && battlefield.IsTileOccupied(currentNode.Pos) && selectedNodes.ContainsKey(currentNode.Pos))
            {
                selectedNodes.Remove(currentNode.Pos);
                Debug.Log("Remove childless occupied tile : " + currentNode+" => is root? "+currentNode.IsRoot());
                if (!currentNode.IsRoot())
                {
                    currentNode.Parent.Children--;
                    RemoveChildlessOccupiedNodes(battlefield, selectedNodes, currentNode.Parent);
                }
            }
            
        }

        public static List<Vector3Int> FindAttackArea(List<Vector3Int> standingPointList, Battlefield battle, Unit actor)
        {
            int rangeMin = actor.Model.CurrentWeapon().Template().RangeMin();
            int rangeMax = actor.Model.CurrentWeapon().Template().RangeMax();
            Vector3Int actorPos = actor.GetMapPos();
            return FindAttackArea(standingPointList, battle, rangeMin, rangeMax, actorPos);
        }

        public static List<Vector3Int> FindAttackArea(List<Vector3Int> standingPointList, Battlefield battlefield, int rangeMin, int rangeMax, Vector3Int actorPos)
        {
            standingPointList.Add(actorPos);

            ConcentricArea area = new ConcentricArea(battlefield.Map);
            area.RangeMin = rangeMin;
            area.RangeMax = rangeMax;
            List<Vector3Int> areaPosList;
            List<Vector3Int> attackArea = new List<Vector3Int>();
            foreach(Vector3Int standingPoint in standingPointList)
            {
                area.Center = standingPoint;
                areaPosList = area.GetCells();
                foreach(Vector3Int target in areaPosList)
                {
                    if (!standingPointList.Contains(target) && !attackArea.Contains(target))
                        attackArea.Add(target);
                }
            }

            attackArea.Remove(actorPos);
            standingPointList.Remove(actorPos);

            return attackArea;
        }
    }

}
