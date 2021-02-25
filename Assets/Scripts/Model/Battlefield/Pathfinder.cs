﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pathfinder
{

    public class Node: IComparable
    {
        public Node Parent { get; } // previous node
        public Vector3Int Pos { get; } // position of the tile on the tilemap
        public int FCost => Gcost + Hcost;
        public int Gcost { get; }  // distance from initial position
        public int Hcost { get; } // distance from the target

        public Node(Node parent, Vector3Int gridPosInput, int gcostInput, int hcostInput)
        {
            this.Parent = parent;
            this.Pos = gridPosInput;
            this.Gcost = gcostInput;
            this.Hcost = hcostInput;
        }

        public bool IsRoot => Parent == null;

        public List<Node> GetPath() =>  GetPath(new List<Node>());

        private List<Node> GetPath(List<Node> endOf)
        {
            endOf.Add(this);
            if (IsRoot)
            {
                return endOf;
            }
            else
            {
                return Parent.GetPath(endOf);
            }
        }

        public int CompareTo(object o)
        {
            int returnedValue = 0;
            if(o.GetType() == typeof(Node))
            {
                Node other = (Node)o;
                if (FCost > other.FCost)
                    returnedValue = 1;
                else if (FCost < other.FCost)
                    returnedValue = -1;
                else
                {
                    if (Hcost > other.Hcost)
                        returnedValue = 1;
                    else if (Hcost < other.Hcost)
                        returnedValue = -1;
                }
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

        public override string ToString() => string.Format("N:{0} ", Pos);

        public string ToLongString() => string.Format("Node of cost {0} + {1} = {2} at pos {3} and {4} ",Gcost, Hcost, FCost, Pos, (IsRoot) ? " is root." : " parent is at"+Parent.Pos);
    }




    public static class Algorithm
    {

        // return path
        public static List<Node> GetShortestPath(Tilemap map, Vector3Int initPos, Vector3Int targetPos)
        {

            // check if targetPos and initPos within the tilemap and accessible

            if (!map.HasTile(initPos) || !map.HasTile(targetPos))
                return new List<Node>();

            WorldTile targetTile = map.GetTile<WorldTile>(targetPos);
            if (!targetTile.Model.Traversable)
                return new List<Node>();

            // ---***$$$ find the shortest path $$$***---

            Node chosenNode = new Node(null, initPos, 0, 0);
            Node targetNode = new Node(null, targetPos, 0, 0);
            List<Node> elligibleNeighborNodes = new List<Node>();
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            openList.Add(chosenNode);

            while (openList.Count > 0)
            {

                // find new chosen least costly node, remove it from open list to place it inside the closed list

                openList.Sort();
                chosenNode = openList[0];
                closedList.Add(chosenNode);
                openList.RemoveAt(0);

                Debug.Log(string.Format("\nChosen {0}\nOpen : {1}\nClosed: {2} ", chosenNode.ToLongString(), string.Join("|", openList), string.Join("/", closedList)));

                // quit the loop if the chosen node is the target one

                if (chosenNode.Equals(targetNode))
                    break;

                // the algo compute the neighbor cost of the current node which is the last chosen node as well

                elligibleNeighborNodes.Clear();

                WorldTile neighborTile;
                Node neighborNode;
                Vector3Int neighborPos = new Vector3Int(chosenNode.Pos.x + 1, chosenNode.Pos.y, 0);
                if (map.HasTile(neighborPos))
                {
                    neighborTile = map.GetTile<WorldTile>(neighborPos);
                    neighborNode = new Node(chosenNode, neighborPos, chosenNode.Gcost + 1, Dist(neighborPos, targetPos));
                    if (neighborTile.Model.Traversable && !closedList.Contains(neighborNode))
                        elligibleNeighborNodes.Add(neighborNode);
                }

                neighborPos = new Vector3Int(chosenNode.Pos.x, chosenNode.Pos.y + 1, 0);
                if (map.HasTile(neighborPos))
                {
                    neighborTile = map.GetTile<WorldTile>(neighborPos);
                    neighborNode = new Node(chosenNode, neighborPos, chosenNode.Gcost + 1, Dist(neighborPos, targetPos));
                    if (neighborTile.Model.Traversable && !closedList.Contains(neighborNode))
                        elligibleNeighborNodes.Add(neighborNode);
                }

                neighborPos = new Vector3Int(chosenNode.Pos.x - 1, chosenNode.Pos.y, 0);
                if (map.HasTile(neighborPos))
                {
                    neighborTile = map.GetTile<WorldTile>(neighborPos);
                    neighborNode = new Node(chosenNode, neighborPos, chosenNode.Gcost + 1, Dist(neighborPos, targetPos));
                    if (neighborTile.Model.Traversable && !closedList.Contains(neighborNode))
                        elligibleNeighborNodes.Add(neighborNode);
                }

                neighborPos = new Vector3Int(chosenNode.Pos.x, chosenNode.Pos.y - 1, 0);
                if (map.HasTile(neighborPos))
                {
                    neighborTile = map.GetTile<WorldTile>(neighborPos);
                    neighborNode = new Node(chosenNode, neighborPos, chosenNode.Gcost + 1, Dist(neighborPos, targetPos));
                    if (neighborTile.Model.Traversable && !closedList.Contains(neighborNode))
                        elligibleNeighborNodes.Add(neighborNode);
                }

                // update the open list, replacing old nodes with newly Computed one if less costly 

                Node formerOpenNode;
                foreach (Node newOpenNode in elligibleNeighborNodes)
                {
                    if (openList.Contains(newOpenNode))
                    {
                        formerOpenNode = openList.Find(n => n.Equals(newOpenNode));
                        if (newOpenNode.BetterThan(formerOpenNode))
                        {
                            openList.Remove(formerOpenNode);
                            openList.Add(newOpenNode);
                        }
                    }
                    else
                    {
                        openList.Add(newOpenNode);
                    }
                }

                

            }

            return chosenNode.GetPath();
        }

        public static int Dist(Vector3Int gridPos1, Vector3Int gridPos2) => Math.Abs(gridPos1.x - gridPos2.x) + Math.Abs(gridPos1.y - gridPos2.y);

    }

}
