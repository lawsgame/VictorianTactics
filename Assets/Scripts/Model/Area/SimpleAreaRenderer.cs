using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SimpleAreaRenderer : IAreaRenderer
{

    [SerializeField] private GameObject areaTile;

    private static readonly Dictionary<AreaType, Color> tileColor;

    static SimpleAreaRenderer() {
        tileColor = new Dictionary<AreaType, Color>();
        tileColor.Add(AreaType.ATTACK, Color.red);
        tileColor.Add(AreaType.MOVE, Color.magenta);
        tileColor.Add(AreaType.DEPLOYEMENT, Color.yellow);
    }


    public override void Draw(Tilemap battlefield)
    {
        List<Vector3Int> cellposs = Model.GetCells();
        foreach(Vector3Int pos in cellposs)
        {
            Vector3 worldPos = battlefield.CellToWorld(pos) + new Vector3(0f, 0.25f, 0f);
            GameObject cloneTile = GameObject.Instantiate(areaTile, worldPos, Quaternion.identity, transform);
            SpriteRenderer renderer = cloneTile.GetComponent<SpriteRenderer>();
            if (tileColor.ContainsKey(Type))
                renderer.color = tileColor[Type];
        }
    }


}
