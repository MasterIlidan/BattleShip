using UnityEngine;

public class TileInit : MonoBehaviour
{
    public GameObject tilePrefab;

    public GameObject rootTile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameObject tile = rootTile;
        Vector3 firstTilVector3 = tile.transform.position;
        char letter = 'A';
        for (int j = 1; j <= 10; j++)
        {
            Vector3 origin = firstTilVector3;
            if (j != 0)
            {
                tile = Instantiate(tilePrefab, origin, tile.transform.rotation);
                //tile.SendMessage("SetName", letter.ToString() + 1);
                tile.transform.SetParent(rootTile.transform);
                tile.name = "" + letter + 1;
            }
            for (int i = 2; i <= 10; i++)
            {
                
                origin.y -= 0.535f;
                tile = Instantiate(tilePrefab, origin, tile.transform.rotation);
                //tile.SendMessage("SetName",  letter.ToString() + 1);
                tile.transform.SetParent(rootTile.transform);
                tile.name = "" + letter + i;
            }
            letter = (char)(letter + 1);
            firstTilVector3.x += 0.53f;
        }
    }
}
